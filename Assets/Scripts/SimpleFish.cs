using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FishBash
{
    public class SimpleFish : MonoBehaviour, IFish
    {

        private GameObject platform;

        [SerializeField]
        [Range(0,10)]
        private float speed;

        public void SetGoal(GameObject goal)
        {
            platform = goal;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 direction = (platform.transform.position-transform.position).normalized;
            transform.position += direction * speed;
        }
    }
}