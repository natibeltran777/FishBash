using UnityEngine;
using DG.Tweening;

namespace FishBash
{
    namespace Target
    {
        public class SimpleTarget : TargetBehaviour, IPoolable
        {
            [SerializeField] private Transform m_target;
            private ParticleSystem particles;
            private float m_glowTransitionDuration = 0.2f;
            private float m_glowOnVal = 0.2f;
            private float m_glowOffVal = 20f;
            private float m_targetPickCoolDown = 2.5f;
            private bool m_isOn = true;
            private bool m_targetIsSelected = false;
            private Material[] m_glowMaterials;
            private static int glowId = Shader.PropertyToID("_GlowPower");
            private GenericPool<SimpleTarget> _pool = null;

            private bool isBeingDestroyed = false;

            public GenericPool<SimpleTarget> Pool
            {
                set
                {
                    if (_pool == null) _pool = value;
                    else
                    {
                        Debug.LogError("Can't reassign pool");
                    }
                }
            }

            public override Transform GetTargetMesh
            {
                get
                {
                    return m_target;
                }
            }

            private void Start()
            {
                particles = GetComponent<ParticleSystem>();
                m_glowMaterials = m_target.GetComponent<Renderer>().materials;
                OnTargetUngazed();
            }

            public override void OnTargetGazed()
            {
                if (!m_isOn)
                {
                    GameManager.instance.SetNewTarget(m_target);
                    foreach(Material material in m_glowMaterials)
                    {
                        material.DOFloat(m_glowOnVal, glowId, m_glowTransitionDuration);
                    }
                    m_isOn = true;
                }
            }

            public override void OnTargetUngazed()
            {
                if (m_isOn)
                {
                    GameManager.instance.SetNewTarget(null);
                    foreach (Material material in m_glowMaterials)
                    {
                        material.DOFloat(m_glowOffVal, glowId, m_glowTransitionDuration);
                    }
                    m_isOn = false;
                }
            }

            public void Reset()
            {
                m_target.gameObject.SetActive(true);
                OnTargetUngazed();
                isBeingDestroyed = false;
            }

            private void OnParticleSystemStopped()
            {
                _pool.Recycle(this);
            }

            private void DestroyedByFish()
            {
                m_target.gameObject.SetActive(false);
                particles.Play();
            }

            public void Recycle(bool instant)
            {
                if (instant && !isBeingDestroyed)
                {
                    isBeingDestroyed = true;
                    _pool.Recycle(this);
                }
                else if (!isBeingDestroyed)
                {
                    isBeingDestroyed = true;
                    DestroyedByFish();
                }
            }
        }
    }
}
