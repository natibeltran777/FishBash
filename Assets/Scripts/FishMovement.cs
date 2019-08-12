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
        [Range(0,5)]
        private float magnitude;


        /// <summary>
        /// Frequency of the sine wave
        /// </summary>
        [SerializeField]
        [Range(0, 5)]
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
        public delegate Vector3 FishPattern(Vector3 pos, Vector3 dir, float t);

        /// <summary>
        /// Adjusts pos based on a sine wave pattern
        /// </summary>
        /// <param name="pos">Input position</param>
        /// <param name="dir">Direction of the sin wave (cross product of object direction)</param>
        /// <param name="t">Time</param>
        /// <returns>Modified position with sin wave in direction dir at time t</returns>
        public static Vector3 SinWave(Vector3 pos, Vector3 dir, float t)
        {
            return SinWave(instance.magnitude, instance.freq, pos, dir, t);
        }


        /// <summary>
        /// Adjusts position based on a sine wave - controllable
        /// </summary>
        /// <param name="mag">Magnitude</param>
        /// <param name="freq">Frequency</param>
        /// <param name="pos">Input position</param>
        /// <param name="dir">Direction of the sin wave (cross product of object direction)</param>
        /// <param name="t">Time</param>
        /// <returns>Modified position with sin wave in direction dir at time t</returns>
        public static Vector3 SinWave(float mag, float freq, Vector3 pos, Vector3 dir, float t)
        {
            return pos + (dir * Mathf.Sin(t * freq) * mag);
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

        

        /// <summary>
        /// Static array of all simple fish patterns - should be the same as the FishPatterns enum
        /// </summary>
        public static FishPattern[] patterns = { Line, SinWave };

    }


    /// <summary>
    /// Labels for all simple fish patterns - should be the same as the array patterns
    /// </summary>
    internal enum FishPatterns
    {
        Line, SineWave
    }
}