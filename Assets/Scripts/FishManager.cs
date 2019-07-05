using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace FishBash
{
    public class FishManager : MonoBehaviour
    {
        public static FishManager instance = null;
        public int CurrWave { get; private set; } = 0;

        /// <summary>
        /// Returns total waves
        /// </summary>
        public int TotalWaves {
            get
            {
                return waveList.Length;
            }
        }
        /// <summary>
        /// Returns fish remaining in current wave
        /// </summary>
        public int FishRemaining
        {
            get
            {
                return fishList.Count;
            }
        }

        [Range(1,10)]
        public float innerRadius;

        public GameObject platform;

        [SerializeField]
        private WaveScriptable[] waveList;
        private IList<IFish> fishList;

        #region UNITY_METHODS
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
           
        }
        #endregion //UNITY_METHODS

        #region PUBLIC_METHODS
        /// <summary>
        /// Destroys the given fish after a set time
        /// </summary>
        /// <param name="toDestroy">Fish object to destroy</param>
        /// <param name="t">Time in seconds to wait before destroying</param>
        public void DestroyFish(IFish toDestroy, float t)
        {
            fishList.Remove(toDestroy);
            toDestroy.Destroy(t);
        }

        /// <summary>
        /// Initializes fish list for new game
        /// </summary>
        public void InitializeWaves()
        {
            if (fishList != null)
            {
                fishList.Clear();
            }
            else
            {
                fishList = new List<IFish>();
            }
            CurrWave = 0;
        }
        #endregion //PUBLIC_METHODS

        #region PRIVATE_METHODS
        /// <summary>
        /// Creates a fish of the specified object at the given position with the given speed
        /// </summary>
        /// <param name="fishToSpawn">Fish Object to use</param>
        /// <param name="position">Vector2 specifying x and z position of the fish</param>
        /// <param name="speed">Speed of the newly created fish</param>
        /// <returns>IFish component created</returns>
        private IFish SpawnFish(GameObject fishToSpawn, Vector2 position, float speed)
        {
            
            GameObject fish = Instantiate(fishToSpawn, new Vector3(position.x, 0, position.y), new Quaternion(), transform);
            IFish toReturn = fish.GetComponent<IFish>();
            toReturn.SetSpeed(speed);
            return toReturn;
        }

        /// <summary>
        /// Creates a fish from a external scriptable asset
        /// </summary>
        /// <param name="fishToSpawn">Scriptable fish to spawn in</param>
        /// <param name="currentWave">Scriptable wave setting external parameters</param>
        /// <returns></returns>
        private IFish SpawnFish(FishScriptable fishToSpawn, WaveScriptable currentWave)
        {
            Vector2 position;
            float speed;

            if (fishToSpawn.randomPosition) {
                position = Utility.RandomPointOnUnitCircle(currentWave.radius.rangeEnd, currentWave.radius.rangeStart);
            }
            else
            {
                position = fishToSpawn.spawnPosition;
            }

            if (fishToSpawn.randomSpeed)
            {
                speed = currentWave.speed.GetRandomValue();
            }
            else
            {
                speed = fishToSpawn.speed;
            }

            return SpawnFish(fishToSpawn.fishPrefab, position, speed);

        }
        #endregion PRIVATE_METHODS

        #region COROUTINES
        /// <summary>
        /// Central game loop - runs each wave until all waves have been executed
        /// </summary>
        /// <returns></returns>
        public IEnumerator HandleWaves()
        {
            while (CurrWave < waveList.Length)
            {
                yield return Break(CurrWave);
                yield return BeginWave(waveList[CurrWave]);
                Debug.Log("Wave Over");
                CurrWave++;
            }
            yield return null;
        }

        /// <summary>
        /// Handles internal wave logic
        /// </summary>
        /// <param name="wave">Current wave</param>
        /// <returns></returns>
        private IEnumerator BeginWave(WaveScriptable wave)
        {
            if (wave.randomFish)
            {
                //Spawns fish in random order
                for(int i = 0; i < wave.fishCount; i++)
                {
                    IFish f = SpawnFish(wave.fishInWave[UnityEngine.Random.Range(0, wave.fishInWave.Length)], wave);
                    fishList.Add(f);
                    yield return new WaitForSeconds(wave.timeBetweenFish);
                }
                
            }
            else
            {
                //Spawns fish in specified order
                foreach (int i in ProcessString(wave.order)){
                    IFish f = SpawnFish(wave.fishInWave[i], wave);
                    fishList.Add(f);
                    yield return new WaitForSeconds(wave.timeBetweenFish);
                }
            
            }
            while(FishRemaining > 0)
            {
                yield return null;
            }
            yield return null;
        }



        /// <summary>
        /// Given a string outlining the order of fish, breaks string up into enumerable. Uses '.' as a seperator character
        /// </summary>
        /// <param name="order">String to process</param>
        /// <returns>Enumerable list providing order of fish</returns>
        IEnumerable<int> ProcessString(string order)
        {
            string[] toReturn = order.Split('.');
            int[] t = new int[toReturn.Length];
            for (int i = 0; i < toReturn.Length; i++)
            {
                t[i] = int.Parse(toReturn[i]);
            }
            return t;
        }

        /// <summary>
        /// Filler coroutine to run before each wave
        /// </summary>
        /// <param name="nextWave">Name of next wave</param>
        /// <returns></returns>
        private IEnumerator Break(int nextWave)
        {
            yield return GameManager.instance.DisplayText("Beginning wave " + (nextWave+1) + "...", 3);
        }
        #endregion //COROUTINES
    }
}
