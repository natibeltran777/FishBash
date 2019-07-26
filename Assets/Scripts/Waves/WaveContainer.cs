using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FishBash
{
    namespace Waves
    {
        [CreateAssetMenu(fileName = "Wave", menuName = "Waves/New Wave Container", order = 1)]
        public class WaveContainer : ScriptableObject
        {
            public WaveScriptable[] subwaves;
            public float timeBetweenSubwaves;
            public int totalPoints;
        }
    }
}


