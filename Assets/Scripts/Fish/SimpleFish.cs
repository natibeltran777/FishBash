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

        private bool activateBezier;

        private float timeVal = 0;
        private float lengthOfCurve = 0;
        private float timeToTraverseCurve = 0; 

        private Vector3 initialBezierPosition, middleBezierPosition, finalBezierPosition = Vector3.zero;

        static readonly int highlightEnabled = Shader.PropertyToID("_HighlightEnabled");

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

        protected List<Material> fishMats;

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

        protected Rigidbody fishRigidBody;

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
            Debug.Log("Hit: " + HasBeenHit);
            if (!HasBeenHit)
            {
                HasBeenHit = true;
                trail.emitting = true;
                GameManager.instance.IncrementScore(scoreValue);
                EventManager.TriggerEvent("FISHHIT");
            }
        }

        public void HomingHit(Vector3 initialPos, Vector3 finalPos, Vector3 dirOfImpact, Vector3 velocity)
        {
            Debug.Log("Hit: " + HasBeenHit);
            if (!HasBeenHit)
            {
                HasBeenHit = true;
                initialBezierPosition = initialPos;
                float midMagnitude = GetMidDistance(finalPos);
                middleBezierPosition = initialPos + dirOfImpact * midMagnitude;
                finalBezierPosition = finalPos;

                // We get an estimated length of the curve we're using to home on an object
                //lengthOfCurve = (finalBezierPosition - middleBezierPosition).magnitude + (initialBezierPosition - middleBezierPosition).magnitude;

                lengthOfCurve = GetLengthOfBezier();
                // Divide that length by the initial velocity of impact to get an estimated time it would take to traverse the curve at a constant velocity
                timeToTraverseCurve = lengthOfCurve / velocity.magnitude;
                
                //m_fruit = other.attachedRigidbody;
                activateBezier = true;
                trail.emitting = true;
                GameManager.instance.IncrementScore(scoreValue);
                EventManager.TriggerEvent("FISHHIT");
            }
        }

        public void MarkDestroyed()
        {
            foreach(Material m in fishMats)
            {
                m.SetFloat(highlightEnabled, 1.0f);
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
            fishRigidBody.AddForce(force*6f, ForceMode.Impulse);
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

        

        /// <summary>
        /// Get's the distance between the fruit and the target naval enemy
        /// </summary>
        /// <returns> Magnitude of vector to mid point </returns>
        private float GetMidDistance(Vector3 finalPos)
        {
            Vector3 midpoint = (finalPos - this.transform.position) / 2;
            return midpoint.magnitude;
        }

        private float GetLengthOfBezier()
        {
            // Simpsons rule to get length of curve
            return ((0.125f)*(BezierPositionGivenTime(0) + 3 * BezierPositionGivenTime(0.33f) + 3 * BezierPositionGivenTime(0.66f) + BezierPositionGivenTime(1))).magnitude;
        }

        private Vector3 BezierPositionGivenTime(float t)
        {
            return initialBezierPosition * 1 * Mathf.Pow(t, 0) * Mathf.Pow((1 - t), 2)
                        + middleBezierPosition * 2 * Mathf.Pow(t, 1) * Mathf.Pow((1 - t), 1)
                        + finalBezierPosition * 1 * Mathf.Pow(t, 2) * Mathf.Pow((1 - t), 0);
        }

        #region UNITY_MONOBEHAVIOUR_METHODS
        protected void Awake()
        {
            fishRigidBody = GetComponent<Rigidbody>();
            Renderer[] renderers = GetComponentsInChildren<Renderer>(includeInactive: true);
            fishMats = new List<Material>();
            foreach(Renderer r in renderers)
            {
                fishMats.AddRange(r.materials);
            }
            //this.platform = FishManager.instance.platform;
            pattern = FishMovement.patterns[(int)currentPattern];
            fishAudio = GetComponent<AudioSource>();
            trail = GetComponent<TrailRenderer>();
            gameObject.layer = 10;
            //Initialize();
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
            fishRigidBody.velocity = Vector3.zero;
            fishRigidBody.angularVelocity = Vector3.zero;
            Speed = startSpeed;
            fishAudio.Play();
            fishAudio.loop = true;
            trail.emitting = false;
            foreach (Material m in fishMats)
            {
                m.SetFloat(highlightEnabled, 0.0f);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(initialBezierPosition, 0.2f);
            Gizmos.DrawSphere(middleBezierPosition, 0.2f);
            Gizmos.DrawSphere(finalBezierPosition, 0.2f);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(initialBezierPosition, middleBezierPosition);
            Gizmos.DrawLine(middleBezierPosition, finalBezierPosition);
        }


        void FixedUpdate()
        {
            if (activateBezier)
            {

                Vector3 newPosition = BezierPositionGivenTime(timeVal);

                this.transform.position = newPosition;
                timeVal += Time.deltaTime * (1 / timeToTraverseCurve);
                fishRigidBody.AddRelativeTorque(1, 0.8f, 0.2f);

                if (timeVal > 1)
                {
                    timeVal = 0;
                    activateBezier = false;
                    return;
                }
                return;
            }

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
                fishRigidBody.AddForce(Vector3.down * 7f, ForceMode.Acceleration);
                if(this.transform.position.y <= -20)
                    FishManager.instance.DestroyFish(this, 0f);

            }

        }
        #endregion //UNITY_MONOBEHAVIOUR_METHODS
    }
}