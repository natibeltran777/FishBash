using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Splash
    {
        public class SplashObj : MonoBehaviour
        {
            new ParticleSystem particleSystem;
            private SplashPool _pool = null;
            private SphereCollider col;
            ParticleSystem.ShapeModule shape;
            ParticleSystem.MainModule main;

            ParticleSystem.MinMaxCurve speed;

            [SerializeField, Tooltip("Only does anything in the editor, use for testing purposes")]
            float strength;
            [SerializeField, Tooltip("Replaces the start speed property on the particle system")]
            Vector2 initSpeedRange;

            public SplashPool Pool
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



            // Start is called before the first frame update
            void Awake()
            {
                SetupParticleSystem();
                col = GetComponent<SphereCollider>();
            }

            void SetupParticleSystem()
            {
                particleSystem = GetComponent<ParticleSystem>();
                shape = particleSystem.shape;
                main = particleSystem.main;
                main.simulationSpeed = 3.0f;

                speed = main.startSpeed;
            }

            private void OnParticleSystemStopped()
            {
                RecycleSplash();
            }

            public void ResetSplash()
            {
                particleSystem.Clear();
                transform.position = Vector3.zero;
            }

            public void SetSplashVals()
            {
#if UNITY_EDITOR
                if (!Application.isPlaying)
                {
                    col = GetComponent<SphereCollider>();
                    SetupParticleSystem();
                }
#endif
                col.radius = strength;

                shape.radius = strength;

                ParticleSystem.MinMaxCurve speed = main.startSpeed;
                speed.constantMax = initSpeedRange.y * strength;
                speed.constantMin = initSpeedRange.x * strength;
                main.startSpeed = speed;
            }

            public void InitializeSplash(float strength, Vector3 position)
            {
                this.strength = strength;
                SetSplashVals();
                transform.position = position;
                position.y = 0;

                //\todo: Also experiment with setting emission speed here

                particleSystem.Play();
            }

            private void RecycleSplash()
            {
                _pool.RecycleSplash(this);
            }

            private void OnCollisionEnter(Collision collision)
            {
                IFish f = gameObject.GetComponent<IFish>();
                GameManager.instance.IncrementScore(f.ScoreVal);

                //\todo: Animate this, highlight shader would be useful for this actually
                FishManager.instance.DestroyFish(f, 0.1f);
            }



        }
    }
}
