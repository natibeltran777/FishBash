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
            private readonly float _speedMultiplier;

            public DeterministicSubWave(WaveScriptable toBuild)
            {
                _speedMultiplier = toBuild.speedMultiplier;
                _delay = toBuild.timeBetweenFish;
                data = toBuild.fishInWave;
            }

            public IEnumerator BeginWave()
            {
                foreach(FishContainer f in data)
                {
                    FishManager.instance.SpawnFish(f, _speedMultiplier);
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
