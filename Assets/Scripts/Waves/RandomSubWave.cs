using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {

        public class RandomSubWave : IWaves<FishContainer>
        {
            private readonly int _fishCount;
            private FishContainer[] data;
            private readonly float _delay;
            private readonly float _speedMultiplier;

            private readonly Vector2 _radiusRange;
            private readonly Vector2 _radiansRange;

            public RandomSubWave(RandomWaveScriptable toBuild)
            {
                _speedMultiplier = toBuild.speedMultiplier;
                _delay = toBuild.timeBetweenFish;
                data = toBuild.fishInWave;
                _fishCount = toBuild.fishCount;
                _radiusRange = toBuild.radius;
                _radiansRange = toBuild.radians;
            }

            public IEnumerator BeginWave()
            {
                for (int i = 0; i < _fishCount; i++)
                {
                    FishContainer toSpawn = data[Random.Range(0, data.Length)];
                    FishManager.instance.RandomSpawnFish(toSpawn, _speedMultiplier, _radiusRange, _radiansRange);
                    yield return new WaitForSeconds(_delay);
                }
            }

            public FishContainer[] GetData()
            {
                return data;
            }

            public int GetLength()
            {
                return _fishCount;
            }
        }
    }

}

