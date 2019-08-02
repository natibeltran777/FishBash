using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {

        /// <summary>
        /// Interface for wave iterators. Contains a list of objects of type T. Iterates through the list and spawns a fixed number of T objects.
        /// </summary>
        /// <typeparam name="T">Object type to iterate through</typeparam>
        public interface IWaves<T>
        {

            /// <summary>
            /// Data to iterate across
            /// </summary>
            /// <returns>List of objects of type T</returns>
            T[] GetData();

            /// <summary>
            /// Handles internal wave logic
            /// </summary>
            /// <returns></returns>
            IEnumerator BeginWave();

            /// <summary>
            /// Returns size of wave
            /// </summary>
            /// <returns>Length of wave, number of objects of type T</returns>
            int GetLength();
        }
    }
    
}
