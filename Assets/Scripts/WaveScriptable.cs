using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    
    [CreateAssetMenu(fileName = "Wave", menuName = "Waves/New Wave", order = 2)]
    [System.Serializable]
    public class WaveScriptable : ScriptableObject
    {
        public string waveName;
        public FishScriptable[] fishInWave;

        public float timeBetweenFish;
        
        public bool randomFish = true;
        public int fishCount; //Only matters if randomFish is true, otherwise fishCount is the length of order

        //order is a string dictating the indices of fish to spawn in fishInWave list;
        public string order;

        [MinMaxRange(1f, 10f)]
        public MinMaxRange speed;
        [MinMaxRange(1f, 75f)]
        public MinMaxRange radius;

    }

}
