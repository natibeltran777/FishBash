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
        protected float startSpeed;

        public float Speed { get; set; }

        [SerializeField]
        protected FishPatterns currentPattern;

        [SerializeField]
        protected GameObject rippleGenerator;

        [SerializeField]
        protected AudioClip[] fishJumpSounds;
        protected AudioSource fishAudio;

        protected TrailRenderer trail;
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

        public int ScoreVal { get => scoreValue; }

        public bool HasBeenHit { get; set; } = false;

        public float Distance
        {
            get
            {
                return Vector3.Distance(transform.position, platform.transform.position);
            }
        }

        private FishPool _pool = null;

        [SerializeField] int fishId = 0;

        public int FishId { get => fishId; }

        public GameObject GameObject { get => gameObject; }

        public FishPool Pool
        {
            set
            {
                if (_pool == null) _pool = value;
                else
                {
                    Debug.LogError("Can't reassign pool");
                }
            }
            get => _pool;
        }

        public bool HasLeaped { get => hasLeapt;}

        protected void UpdateMovement()
        {
            //Debug.Log("move");
            pos += unitDirection * Speed * Time.deltaTime;
            transform.position = pattern(pos, crossDirection, Time.time);
            transform.rotation = Quaternion.LookRotation(prevPos- transform.position, Vector3.up);
            prevPos = transform.position;
        }

        public void SetSpeed(float s)
        {
            Speed = s;
        }

        public bool CheckRadius(float radius)
        {
            return Distance < radius;
        }


        public void HitFish()
        {
            if (!HasBeenHit)
            {
                HasBeenHit = true;
                trail.emitting = true;
                GameManager.instance.IncrementScore(scoreValue);
                EventManager.TriggerEvent("FISHHIT");
            }
        }

        protected void LeapBehavior()
        {
            //fishAudio.Stop();
            fishAudio.loop = false;
            //trail.emitting = true;
            SoundManager.instance.RandomizeSfxOnObject(fishAudio, fishJumpSounds);
            Vector3 force = GetUnitDirection() + Vector3.up;
            Debug.DrawLine(Vector3.zero,force, Color.red);
            rb.AddForce(force*7.2f, ForceMode.Impulse);
            rippleGenerator.SetActive(false);
            hasLeapt = true;
        }

        protected Vector3 GetUnitDirection()
        {
            return (platform.transform.position - transform.position).normalized;
        }

        public void Reclaim()
        {
            if(this.gameObject.activeSelf) //ensure we can't reclaim twice
                Pool.Recycle(this);
        }

        public void OnEnable()
        {
            pos = transform.position;
            platform = FishManager.instance.platform;
            unitDirection = GetUnitDirection();
            crossDirection = Vector3.Cross(unitDirection, Vector3.up);
            hasLeapt = false;
            HasBeenHit = false;
            rippleGenerator.SetActive(true);
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            Speed = startSpeed;
            fishAudio.Play();
            fishAudio.loop = true;
            trail.emitting = false;
        }

        #region UNITY_MONOBEHAVIOUR_METHODS
        protected void Awake()
        {
            rb = GetComponent<Rigidbody>();
            //this.platform = FishManager.instance.platform;
            pattern = FishMovement.patterns[(int)currentPattern];
            fishAudio = GetComponent<AudioSource>();
            trail = GetComponent<TrailRenderer>();
            gameObject.layer = 10;
            //Initialize();
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
                if(this.transform.position.y <= -20)
                    FishManager.instance.DestroyFish(this, 0f);

            }
            
        }
        #endregion //UNITY_MONOBEHAVIOUR_METHODS
    }
}