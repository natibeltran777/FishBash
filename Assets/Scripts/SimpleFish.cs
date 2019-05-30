using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FishBash
{

    /// <summary>
    /// Basic fish behavior. Fish moves toward goal and is modified according to some base pattern
    /// \todo : handle reaching the goal
    /// </summary>
    public abstract class SimpleFish : MonoBehaviour, IFish
    {

        private GameObject platform;

        /// <summary>
        /// Speed for fish to move in
        /// </summary>
        [SerializeField]
        [Range(0,10)]
        protected float speed;

        /// <summary>
        /// Direction for fish to move in
        /// </summary>
        protected Vector3 unitDirection;

        /// <summary>
        /// Direction orthagonal to unitDirection, on the surface of the plane
        /// </summary>
        protected Vector3 crossDirection;

        /// <summary>
        /// Pattern for fish to move in
        /// </summary>
        protected FishMovement.fishPattern pattern;

        protected Vector3 pos;

        /// <summary>
        /// Sets goal as the fish's destination
        /// </summary>
        /// <param name="goal">Object for the fish to move towards</param>
        public void SetGoal(GameObject goal)
        {
            platform = goal;
            unitDirection = (platform.transform.position - transform.position).normalized;
            crossDirection = Vector3.Cross(unitDirection, Vector3.up);
        }

        protected void UpdateMovement()
        {
            pos += unitDirection * speed * Time.deltaTime;
            transform.position = pattern(pos, crossDirection, Time.time);
        }

        #region UNITY_MONOBEHAVIOUR_METHODS
        protected void Start()
        {
            pos = transform.position;
        }

        void Update()
        {
            UpdateMovement();
        }
        #endregion //UNITY_MONOBEHAVIOUR_METHODS
    }
}