using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{

    /// <summary>
    /// Delegate function manager for controlling fish movement 
    /// </summary>
    public class FishMovement : MonoBehaviour
    {
        public static FishMovement instance = null;

        /// <summary>
        /// Magnitude of the sine wave
        /// </summary>
        [SerializeField]
        [Range(0,1)]
        private float magnitude;


        /// <summary>
        /// Frequency of the sine wave
        /// </summary>
        [SerializeField]
        [Range(0, 1)]
        private float freq;

        private void Awake()
        {
            if (instance == null){
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        
        /// <summary>
        /// Returns a position modified according ot a set pattern
        /// </summary>
        /// <param name="pos">Input position</param>
        /// <param name="dir">Direction of modification</param>
        /// <param name="t">Time</param>
        /// <returns>pos modified according to pattern</returns>
        public delegate Vector3 fishPattern(Vector3 pos, Vector3 dir, float t);

        /// <summary>
        /// Adjusts pos based on a sine wave pattern
        /// </summary>
        /// <param name="pos">Input position</param>
        /// <param name="dir">Direction of the sin wave (cross product of object direction)</param>
        /// <param name="t">Time</param>
        /// <returns>Modified position with sin wave in direction dir at time t</returns>
        public static Vector3 SinWave(Vector3 pos, Vector3 dir, float t)
        {
            return pos + (dir * Mathf.Sin(t * instance.freq) * instance.magnitude);
        }

        /// <summary>
        /// Returns straight line. This is used for fish implementing SimpleFish but moving in a base line
        /// </summary>
        /// <param name="pos">Input ot be returned</param>
        /// <param name="dir">Does nothing</param>
        /// <param name="t">Does nothing</param>
        /// <returns>pos</returns>
        public static Vector3 Line(Vector3 pos, Vector3 dir, float t)
        {
            return pos;
        }

    }
}