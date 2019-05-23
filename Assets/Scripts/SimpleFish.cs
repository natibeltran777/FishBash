using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FishBash
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SimpleFish : MonoBehaviour, IFish
    {
        NavMeshAgent agent;
        private GameObject platform;

        public void SetGoal(GameObject goal)
        {
            platform = goal;
        }

        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.destination = platform.transform.position;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}