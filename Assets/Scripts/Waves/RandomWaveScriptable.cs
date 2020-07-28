using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FishBash
{
    namespace Waves
    {
        [CreateAssetMenu(fileName = "RandomWave", menuName = "Waves/New Random Sub Wave", order = 3)]
        [System.Serializable]
        public class RandomWaveScriptable : WaveScriptable
        {
            [Tooltip("List of all fish that can be spawned. Keys are the fish type themselves, value is the number of this type to spawn")]
            public new Dictionary<FishContainer, int> fishInWave;
            [Sirenix.OdinInspector.ShowInInspector]
            public int FishCount { get => fishInWave.Values.Sum(); }

            [Sirenix.OdinInspector.MinMaxSlider(5, 100, showFields: true)]
            public Vector2 radius;

            [Sirenix.OdinInspector.MinMaxSlider(-(2 * (float)System.Math.PI), (2 * (float)System.Math.PI), showFields: true)]
            public Vector2 radians;

        }
    }
}
