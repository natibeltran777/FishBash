using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    public static class Utility
    {
        public static Vector2 RandomPointOnUnitCircle(float radius)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);
            float x = Mathf.Sin(angle) * radius;
            float y = Mathf.Cos(angle) * radius;

            return new Vector2(x, y);

        }
    }
}
