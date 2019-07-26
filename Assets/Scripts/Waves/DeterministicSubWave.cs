using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {
        public class DeterministicSubWave : IWaves<FishContainer>
        {
            private readonly WaveScriptable toBuild;
            private FishContainer[] data;
            private readonly float _delay;
            private readonly float _speed;

            public DeterministicSubWave(WaveScriptable toBuild)
            {
                _speed = toBuild.defaultSpeed;
                _delay = toBuild.timeBetweenFish;
                data = toBuild.fishInWave;
            }

            public IEnumerator BeginWave()
            {
                foreach(FishContainer f in data)
                {
                    FishManager.instance.SpawnFish(f, _speed);
                    yield return new WaitForSeconds(_delay);

                }
            }

            public FishContainer[] GetData()
            {
                return data;
            }

            public int GetLength()
            {
                return data.Length;
            }

        }
    }

}
