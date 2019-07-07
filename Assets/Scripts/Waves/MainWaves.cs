using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {
        public class MainWaves : CustomYieldInstruction, IWaves<WaveScriptable>
        {
            private float _timeBetweenWaves = 0;
            private WaveScriptable[] _subWaves;

            private bool allSubWavesOver = false;

            public override bool keepWaiting => !allSubWavesOver;

            public MainWaves(WaveScriptable[] subWaves, float timeBetweenWaves)
            {
                _subWaves = subWaves;
                _timeBetweenWaves = timeBetweenWaves;
                Debug.Log("Constructor called");
                BeginWave();
            }

            public IEnumerator BeginWave()
            {
                foreach (WaveScriptable s in _subWaves)
                {
                    Debug.Log("Begin wave");
                    if (s.GetType() == typeof(RandomWaveScriptable))
                    {
                        Debug.Log("Type identified");
                        yield return new RandomSubWave((RandomWaveScriptable) s);
                    }
                    else
                    {
                        yield return new DeterministicSubWave(s);
                    }
                    yield return new WaitForSeconds(_timeBetweenWaves);
                }

                while(FishManager.instance.FishRemaining > 0)
                {
                    yield return null;
                }

                allSubWavesOver = true;

            }


            public WaveScriptable[] GetData()
            {
                return _subWaves;
            }

            public int GetLength()
            {
                return _subWaves.Length;
            }

        }
    }

}


