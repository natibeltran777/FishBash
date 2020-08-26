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
                Debug.Log("hit target");
                target.Recycle();
            }
        }
    }
}
