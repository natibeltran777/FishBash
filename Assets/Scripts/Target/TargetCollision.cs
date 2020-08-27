using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Target
    {
        public class TargetCollision : MonoBehaviour
        {
            [SerializeField] private SimpleTarget target;

            public void OnCollisionEnter(Collision collision)
            {
                if (collision.gameObject.TryGetComponent(out IFish fish)){
                    target.Recycle(false);
                    GameManager.instance.IncrementScore(3 * fish.ScoreVal);
                    EventManager.TriggerEvent("TARGETHIT");
                }
            }
        }
    }
}
