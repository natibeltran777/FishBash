using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {

        public class RandomSubWave : CustomYieldInstruction, IWaves<FishContainer>
        {
            private bool isOver = false;
            public override bool keepWaiting => !isOver;
            private int _fishCount;
            private FishContainer[] data;
            private float _delay;
            private float _speed;

            private MinMaxRange _radiusRange;
            private MinMaxRange _radiansRange;

            public RandomSubWave(RandomWaveScriptable toBuild)
            {
                _speed = toBuild.defaultSpeed;
                _delay = toBuild.timeBetweenFish;
                data = toBuild.fishInWave;
                _fishCount = toBuild.fishCount;
                _radiusRange = toBuild.radius;
                _radiansRange = toBuild.radians;
                BeginWave();
            }

            public IEnumerator BeginWave()
            {
                for (int i = 0; i < _fishCount; i++)
                {
                    FishContainer toSpawn = data[Random.Range(0, data.Length)];
                    FishManager.instance.RandomSpawnFish(toSpawn, _speed, _radiusRange, _radiansRange);
                    yield return new WaitForSeconds(_delay);
                }
                isOver = true;
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

