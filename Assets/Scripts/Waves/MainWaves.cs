using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    namespace Waves
    {
        public class MainWaves : IWaves<WaveScriptable>
        {
            private readonly float _timeBetweenWaves = 0;
            private WaveScriptable[] _subWaves;
            private GameManager _caller;

            public MainWaves(WaveScriptable[] subWaves, float timeBetweenWaves, GameManager caller)
            {
                _subWaves = subWaves;
                _timeBetweenWaves = timeBetweenWaves;
                _caller = caller;
                //Debug.Log("Constructor called");
            }
            
            public IEnumerator BeginWave()
            {
                foreach (WaveScriptable s in _subWaves)
                {
                    //Debug.Log("Begin subwave");
                    IWaves<FishContainer> subWave;
                    if (s.GetType() == typeof(RandomWaveScriptable))
                    {
                        //Debug.Log("Type identified");
                        subWave = new RandomSubWave((RandomWaveScriptable) s);
                        yield return _caller.StartCoroutine(subWave.BeginWave());
                    }
                    else
                    {
                        subWave = new DeterministicSubWave(s);
                        yield return _caller.StartCoroutine(subWave.BeginWave());
                    }
                    //Debug.Log("Finish subwave. Waiting...");
                    yield return new WaitForSeconds(_timeBetweenWaves);
                }

                while(FishManager.instance.FishRemaining > 0)
                {
                    yield return null;
                }
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


