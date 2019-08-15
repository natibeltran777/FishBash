using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.Serialization;
using Sirenix.OdinInspector;

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
