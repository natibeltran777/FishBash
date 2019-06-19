using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FishBash
{
    
    public interface IFish
    {

        /// <summary>
        /// Checks if the fish is within a set radius of the fish's goal
        /// </summary>
        /// <returns>True if it is close</returns>
        bool CheckRadius(float radius);


        /// <summary>
        /// Sets the fish speed
        /// </summary>
        /// <param name="s">New speed</param>
        void SetSpeed(float s);

        void Destroy(float t);

    }
}
