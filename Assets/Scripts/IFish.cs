using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace FishBash
{
    
    public interface IFish
    {

        /// <summary>
        /// Sets the destination object for the fish
        /// </summary>
        /// <param name="goal">Destination</param>
        void SetGoal(GameObject goal);

    }
}
