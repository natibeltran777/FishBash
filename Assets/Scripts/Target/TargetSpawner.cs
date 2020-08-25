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
            [SerializeField, Range(0, 5)] float targetLifetime;

            SimpleTarget testTarget = null;

            // \todo: replace once target break behaviour is implemented
            private IEnumerator Start()
            {
                while (true)
                {
                    if (!testTarget)
                    {
                        SpawnTargetRandom();
                        yield return new WaitForSeconds(targetLifetime);
                        testTarget.Recycle();
                        testTarget = null;
                    }
                    yield return null;
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
