using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    public class RandomSineFish : SimpleFish
    {
        private SinWaveGenerator gen;

        //Sets pattern to a sine wave
        new void Start()
        {
            base.Start();
            gen = new SinWaveGenerator(Random.Range(0, 5), Random.Range(0, 5));
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
