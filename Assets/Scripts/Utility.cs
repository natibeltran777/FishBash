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
        /// <param name="minRadius">Minimum distance</param>
        /// <param name="maxRadius">Maximum distance</param>
        /// <param name="minAngle">Optional minimum angle</param>
        /// <param name="maxAngle">Optional maximum angle</param>
        /// <returns>Vector2 Point in 2D space</returns>
        public static Vector2 RandomPointOnUnitCircle(float minRadius, float maxRadius, float minAngle = 0f, float maxAngle = Mathf.PI * 2)
        {
            float radius = Random.Range(minRadius, maxRadius);
            float angle = Random.Range(minAngle, maxAngle);
            return PointOnUnitCircle(radius, angle);

        }

        /// <summary>
        /// Returns a random point on the unit circle inside the given range
        /// </summary>
        /// <param name="range">Range of distance</param>
        /// <returns>Vector2 Point in 2D space</returns>
        public static Vector2 RandomPointOnUnitCircle(Vector2 range)
        {
            return RandomPointOnUnitCircle(minRadius: range.x, maxRadius: range.y);
        }

        /// <summary>
        /// Returns a random point on the unit circle inside the given range
        /// </summary>
        /// <param name="range">Range of distance</param>
        /// <param name="angle">Range of angles</param>
        /// <returns>Vector2 Point in 2D space</returns>
        public static Vector2 RandomPointOnUnitCircle(Vector2 range, Vector2 angle)
        {
            return RandomPointOnUnitCircle(range.x, range.y, angle.x, angle.y);
        }

        /// <summary>
        /// Returns a random point on the unit circle at a fixed distance
        /// </summary>
        /// <param name="radius">Distance from origin</param>
        /// <param name="minAngle">Optional minimum angle</param>
        /// <param name="maxAngle">Optional maximum angle</param>
        /// <returns>Vector2 point in 2D space</returns>
        public static Vector2 RandomPointOnUnitCircle(float radius, float minAngle = 0f, float maxAngle = Mathf.PI * 2)
        {
            float angle = Random.Range(minAngle, maxAngle);
            return PointOnUnitCircle(radius, angle);
        }

        /// <summary>
        /// Returns a fixed point on the unit circle
        /// </summary>
        /// <param name="radius">Distance from origin</param>
        /// <param name="angle">Angle in radians of point</param>
        /// <returns>Vector2 point in 2D space</returns>
        private static Vector2 PointOnUnitCircle(float radius, float angle)
        {
            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;

            return new Vector2(x, y);
        }

    }

  

}
