using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{

    [System.Serializable]
    public class FishContainer 
    {
        public GameObject fishPrefab;

        public int pointVal;

        public Vector2? spawnPositionOverride;
    }
}
