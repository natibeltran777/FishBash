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

            [Sirenix.OdinInspector.MinMaxSlider(5, 100, showFields: true)]
            public Vector2 radius;

            [Sirenix.OdinInspector.MinMaxSlider(0, (2 * (float)System.Math.PI), showFields: true)]
            public Vector2 radians;

        }
    }
}
