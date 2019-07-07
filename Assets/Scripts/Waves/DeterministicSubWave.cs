using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {
        public class DeterministicSubWave : CustomYieldInstruction, IWaves<FishContainer>
        {
            private WaveScriptable toBuild;
            private bool isOver = false;
            private FishContainer[] data;
            private float _delay;
            private float _speed;

            public DeterministicSubWave(WaveScriptable toBuild)
            {
                _speed = toBuild.defaultSpeed;
                _delay = toBuild.timeBetweenFish;
                data = toBuild.fishInWave;
                BeginWave();
            }

            public override bool keepWaiting => !isOver;

            public IEnumerator BeginWave()
            {
                foreach(FishContainer f in data)
                {
                    FishManager.instance.SpawnFish(f, _speed);
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
                return data.Length;
            }

        }
    }

}
