using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {
        [CreateAssetMenu(fileName = "Wave", menuName = "Waves/New Sub Wave", order = 2)]
        public class WaveScriptable : SerializedScriptableObject
        {
            public string waveName;
            [NonSerialized, OdinSerialize]
            public FishContainer[] fishInWave;

            public float defaultSpeed;

            public float timeBetweenFish;

        }
    }
}
