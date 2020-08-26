using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Target
    {
        public class TargetSpawner : MonoBehaviour
        {
            [SerializeField] private TargetPool targetFactory;

            [SerializeField, Range(0, 10)] float radius;
            [SerializeField, Range(0, 20)] float targetLifetime;

            SimpleTarget testTarget = null;

            float t = 0;

            private void Update()
            {
                if (!testTarget || !testTarget.gameObject.activeInHierarchy)
                {
                    t = 0;
                    SpawnTargetRandom();
                }
                else
                {
                    t += Time.deltaTime;
                    if(t > targetLifetime)
                    {
                        testTarget.Recycle();
                        testTarget = null;
                    }
                }
            }

            private void SpawnTargetRandom()
            {
                Vector3 position = (Random.insideUnitSphere * radius) + transform.position;
                testTarget = targetFactory.Get();
                testTarget.transform.position = position;
            }

            private void OnDrawGizmos()
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, radius);
            }
        }
    }
}
