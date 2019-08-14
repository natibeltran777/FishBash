using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    public class RandomSineFish : SimpleFish
    {
        private SinWaveGenerator gen;
        [SerializeField, Sirenix.OdinInspector.MinMaxSlider(0, 5, showFields: true)]
        private Vector2 magnitudeRange;
        [SerializeField, Sirenix.OdinInspector.MinMaxSlider(0, 5, showFields: true)]
        private Vector2 frequencyRange;

        //Sets pattern to a sine wave
        new void Start()
        {
            base.Start();
            gen = new SinWaveGenerator(Random.Range(magnitudeRange.x, magnitudeRange.y), Random.Range(frequencyRange.x, frequencyRange.y));
            pattern = gen.SineWave;
        }

        internal class SinWaveGenerator
        {
            private float mag;
            private float freq;

            public SinWaveGenerator(float m, float f)
            {
                mag = m;
                freq = f;
            }
            public Vector3 SineWave(Vector3 pos, Vector3 dir, float t)
            {
                return FishMovement.SinWave(mag, freq, pos, dir, t);
            }
        }

    }

    
}
