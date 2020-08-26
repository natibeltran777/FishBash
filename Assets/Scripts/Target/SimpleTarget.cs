using UnityEngine;
using DG.Tweening;

namespace FishBash
{
    namespace Target
    {
        public class SimpleTarget : TargetBehaviour, IPoolable
        {
            [SerializeField] private Transform m_target;
            private float m_glowTransitionDuration = 0.5f;
            private float m_glowOnVal = 0.5f;
            private float m_glowOffVal = 20f;
            private bool m_isOn = true;
            private Material m_glowMaterial;
            private static int glowId = Shader.PropertyToID("_GlowPower");
            private GenericPool<SimpleTarget> _pool = null;

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

            private void Start()
            {
                m_glowMaterial = transform.GetComponentInChildren<Renderer>().material;
                OnTargetUngazed();
            }

            public override void OnTargetGazed()
            {
                if (!m_isOn)
                {
                    GameManager.instance.SetNewTarget(m_target);
                    m_glowMaterial.DOFloat(m_glowOnVal, glowId, m_glowTransitionDuration);
                    m_isOn = true;
                }
            }

            public override void OnTargetUngazed()
            {
                if (m_isOn)
                {
                    GameManager.instance.SetNewTarget(null);
                    m_glowMaterial.DOFloat(m_glowOffVal, glowId, m_glowTransitionDuration);
                    m_isOn = false;
                }
            }

            public void Reset()
            {
                OnTargetUngazed();
            }

            public void Recycle()
            {
                _pool.Recycle(this);
            }
        }
    }
}