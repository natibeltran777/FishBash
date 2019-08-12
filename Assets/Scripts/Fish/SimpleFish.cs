using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FishBash
{

    /// <summary>
    /// Basic fish behavior. Fish moves toward goal and is modified according to some base pattern
    /// </summary>
    public class SimpleFish : MonoBehaviour, IFish
    {

        private GameObject platform;

        /// <summary>
        /// Speed for fish to move in
        /// </summary>
        [SerializeField]
        [Range(0,10)]
        protected float speed;
        public float Speed { get => speed; set => speed = value; }

        [SerializeField]
        private FishPatterns currentPattern;

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
        protected FishMovement.FishPattern pattern;
        
        protected Vector3 pos;

        protected Vector3 prevPos = new Vector3();

        protected Rigidbody rb;

        private bool hasLeapt = false;

        public int scoreValue;

        public bool HasBeenHit { get; set; } = false;

        protected void UpdateMovement()
        {
            //Debug.Log("move");
            pos += unitDirection * speed * Time.deltaTime;
            transform.position = pattern(pos, crossDirection, Time.time);
            transform.rotation = Quaternion.LookRotation(prevPos- transform.position, Vector3.up);
            prevPos = transform.position;
        }

        public void SetSpeed(float s)
        {
            speed = s;
        }

        public bool CheckRadius(float radius)
        {
            return Vector3.Distance(transform.position, platform.transform.position) < radius;
        }


        public void HitFish()
        {
            if (!HasBeenHit)
            {
                HasBeenHit = true;
                GameManager.instance.IncrementScore(scoreValue);
                EventManager.TriggerEvent("FISHHIT");
            }
        }

        protected void LeapBehavior()
        {

            Vector3 force = GetUnitDirection() + Vector3.up;
            Debug.DrawLine(Vector3.zero,force, Color.red);
            rb.AddForce(force*7.2f, ForceMode.Impulse);
            hasLeapt = true;
        }

        protected Vector3 GetUnitDirection()
        {
            return (platform.transform.position - transform.position).normalized;
        }

        public void Destroy(float t)
        {
            Destroy(this.gameObject, t);
        }

        #region UNITY_MONOBEHAVIOUR_METHODS
        protected void Start()
        {
            rb = GetComponent<Rigidbody>();
            pos = transform.position;
            this.platform = FishManager.instance.platform;
            unitDirection = GetUnitDirection();
            pattern = FishMovement.patterns[(int)currentPattern];

            crossDirection = Vector3.Cross(unitDirection, Vector3.up);
        }

        void Update()
        {
            if (!hasLeapt)
            {
                if (!CheckRadius(FishManager.instance.innerRadius))
                {
                    UpdateMovement();
                }
                else
                {
                    LeapBehavior();
                }
            }
            if (hasLeapt)
            {
                //Probably need to make this more complicated, and implement some kind of score system here
                rb.AddForce(Vector3.down * 9.8f, ForceMode.Acceleration);
                FishManager.instance.DestroyFish(this, 3f);

            }
            
        }
        #endregion //UNITY_MONOBEHAVIOUR_METHODS
    }
}