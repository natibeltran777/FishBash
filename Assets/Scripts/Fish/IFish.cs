﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FishBash
{
    
    public interface IFish
    {
        float Speed { get; set; }
        bool HasBeenHit { get; set; }

        /// <summary>
        /// Checks if the fish is within a set radius of the fish's goal
        /// </summary>
        /// <returns>True if it is close</returns>
        bool CheckRadius(float radius);

        void Destroy(float t);

        void HitFish();
    }
}
