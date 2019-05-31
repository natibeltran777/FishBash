using System;
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

        protected Rigidbody rb;

        private bool hasLeapt = false;

        protected void UpdateMovement()
        {
            pos += unitDirection * speed * Time.deltaTime;
            transform.position = pattern(pos, crossDirection, Time.time);
        }

        public void SetSpeed(float s)
        {
            speed = s;
        }

        public bool CheckRadius(float radius)
        {
            return Vector3.Distance(transform.position, platform.transform.position) < radius;
        }

        #region UNITY_MONOBEHAVIOUR_METHODS
        protected void Start()
        {
            rb = GetComponent<Rigidbody>();
            pos = transform.position;
            this.platform = FishManager.instance.platform;
            unitDirection = (platform.transform.position - transform.position).normalized;

            crossDirection = Vector3.Cross(unitDirection, Vector3.up);
        }

        void Update()
        {
            if (!hasLeapt && !CheckRadius(FishManager.instance.innerRadius)) {
                UpdateMovement();
            }
            else
            {
                rb.AddForce(Vector3.up + unitDirection, ForceMode.Impulse);
                hasLeapt = true;
                LeapBehavior();
            }
        }

        protected void LeapBehavior()
        {
        }
        #endregion //UNITY_MONOBEHAVIOUR_METHODS
    }
}