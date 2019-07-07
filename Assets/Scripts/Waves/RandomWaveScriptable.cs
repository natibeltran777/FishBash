using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {
        [CreateAssetMenu(fileName = "RandomWave", menuName = "Waves/New Random Sub Wave", order = 3)]
        [System.Serializable]
        public class RandomWaveScriptable : WaveScriptable
        {
            public int fishCount;

            [MinMaxRange(5, 100)]
            public MinMaxRange radius;

            [MinMaxRange(0, (2 * (float)System.Math.PI))]
            public MinMaxRange radians;

        }
    }
}
