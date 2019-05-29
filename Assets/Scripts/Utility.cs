using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    public static class Utility
    {
        /// <summary>
        /// Returns a random point on the unit circle inside maxRadius but outside of minRadius
        /// </summary>
        /// <param name="maxRadius">Maximum distance</param>
        /// <param name="minRadius">Minimum distance</param>
        /// <returns>Vector2 Point in 2D space</returns>
        public static Vector2 RandomPointOnUnitCircle(float maxRadius, float minRadius)
        {
            float radius = Random.Range(minRadius, maxRadius);
            float angle = Random.Range(0f, Mathf.PI * 2);
            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;

            return new Vector2(x, y);

        }

        /// <summary>
        /// Returns a random point on the unit circle at a fixed distance
        /// </summary>
        /// <param name="radius">Distance from origin</param>
        /// <returns>Vector2 point in 2D space</returns>
        public static Vector2 RandomPointOnUnitCircle(float radius)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;

            return new Vector2(x, y);

        }
    }
}
