using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {
        [CreateAssetMenu(fileName = "Wave", menuName = "Waves/New Sub Wave", order = 2)]
        [System.Serializable]
        public class WaveScriptable : ScriptableObject
        {
            public string waveName;
            public FishContainer[] fishInWave;

            public float defaultSpeed;

            public float timeBetweenFish;

        }
    }
}
