using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FishBash
{
    namespace Waves
    {

        public class RandomSubWave : IWaves<FishContainer>
        {
            private readonly int _fishCount;
            private Dictionary<FishContainer, int> data;
            private FishContainer[] fishTypes;
            private readonly float _delay;
            private readonly float _speedMultiplier;

            private readonly Vector2 _radiusRange;
            private readonly Vector2 _radiansRange;

            public RandomSubWave(RandomWaveScriptable toBuild)
            {
                _speedMultiplier = toBuild.speedMultiplier;
                _delay = toBuild.timeBetweenFish;
                data = toBuild.fishInWave;
                _fishCount = toBuild.FishCount;
                _radiusRange = toBuild.radius;
                _radiansRange = toBuild.radians;
                fishTypes = data.Keys.ToArray();
            }

            public IEnumerator BeginWave()
            {
                for (int i = 0; i < _fishCount; i++)
                {
                    FishContainer toSpawn = fishTypes[Random.Range(0, fishTypes.Length)];
                    data[toSpawn]--;
                    if(data[toSpawn] < 1)
                    {
                        data.Remove(toSpawn);
                        fishTypes = data.Keys.ToArray();
                    }
                    FishManager.instance.RandomSpawnFish(toSpawn, _speedMultiplier, _radiusRange, _radiansRange);
                    yield return new WaitForSeconds(_delay);
                }
            }

            public FishContainer[] GetData()
            {
                return fishTypes;
            }

            public int GetLength()
            {
                return _fishCount;
            }
        }
    }

}

