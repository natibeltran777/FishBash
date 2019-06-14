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

        [Range(1, 10)]
        public float speedMax;
        [Range(1, 10)]
        public float speedMin;
        [Range(1, 75)]
        public int maxRadius;
        [Range(1, 75)]
        public int minRadius;

    }

    [CreateAssetMenu(fileName = "Enemy", menuName = "Waves/New Enemy", order = 3)]
    [System.Serializable]
    public class FishScriptable : ScriptableObject
    {
        public GameObject fishPrefab;

        public bool randomSpeed = true;
        public bool randomPosition = true;

        public Vector2 spawnPosition;
        public float speed;
    }
}
