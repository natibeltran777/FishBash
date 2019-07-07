using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{

    [System.Serializable]
    public struct FishContainer 
    {
        public GameObject fishPrefab;

        public int pointVal;

        public Vector2 spawnPosition;
        public float? speedOverride;
    }
}
