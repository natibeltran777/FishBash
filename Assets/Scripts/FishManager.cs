using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FishBash
{
    public class FishManager : MonoBehaviour
    {
        public static FishManager instance = null;

        [SerializeField] FishPool pool;

        /// <summary>
        /// Returns fish remaining in current wave
        /// </summary>
        public int FishRemaining
        {
            get
            {
                Debug.Log("Fish Remaining: " + fishList.Count);
                return fishList.Count;
            }
        }

        [SerializeField] AudioClip[] fishSounds;

        [Range(1,10)]
        public float innerRadius;

        public GameObject platform;

        private IList<IFish> fishList;

        #region UNITY_METHODS
        // \todo: need to restructure this. Should be a singleton, but either needs to be dontdestroyonload or destroy itself on scene change or smth like that
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
        #endregion //UNITY_METHODS

        #region PUBLIC_METHODS
        /// <summary>
        /// Destroys the given fish after a set time
        /// </summary>
        /// <param name="toDestroy">Fish object to destroy</param>
        /// <param name="t">Time in seconds to wait before destroying</param>
        public void DestroyFish(IFish toDestroy, float t)
        {
            if (fishList.Contains(toDestroy))
            {
                fishList.Remove(toDestroy);
                if (t == 0) toDestroy.Reclaim();
                else
                {
                    toDestroy.MarkDestroyed();
                    StartCoroutine(DestroyFishAfterTime(toDestroy, t));
                }
            }
            else
            {
                Debug.Log("This fish shouldn't exist!");
            }
        }

        /// <summary>
        /// Destroys all active fish
        /// </summary>
        public void DestroyAllFish()
        {
            while (fishList.Count > 0)
            {
                DestroyFish(fishList[0], 0);
            }
                
        }

        /// <summary>
        /// Initializes fish list for new game
        /// </summary>
        public void InitializeFishList()
        {
            if (fishList != null)
            {
                fishList.Clear();
            }
            else
            {
                fishList = new List<IFish>();
            }
        }

        /// <summary>
        /// Spawns a fish in a fixed position
        /// </summary>
        /// <param name="f">Fish object</param>
        /// <param name="speedMultiplier">Speed multiplier for this wave</param>
        public GameObject SpawnFish(FishContainer f, float speedMultipler)
        {
            IFish fish;
            GameObject obj;
            (fish, obj) = SpawnFish(f.fishId, f.spawnPositionOverride.Value, speedMultipler);
            fishList.Add(fish);
            return obj;
        }

        /// <summary>
        /// Spawns a fish in a random position
        /// </summary>
        /// <param name="f">Fish object</param>
        /// <param name="speedMultiplier">Speed multiplier for this wave</param>
        /// <param name="distance">Range of distance for this wave</param>
        /// <param name="distance">Range of angles (in radians) for this wave</param>
        public GameObject RandomSpawnFish(FishContainer f, float speedMultiplier, Vector2 distance, Vector2 angle)
        {
            Vector2 position = Utility.RandomPointOnUnitCircle(distance, angle);
            IFish fish;
            GameObject obj;
            (fish, obj) = SpawnFish(f.fishId, f.spawnPositionOverride.GetValueOrDefault(position), speedMultiplier);
            fishList.Add(fish);
            return obj;
        }

        /// <summary>
        /// Creates a fish of the specified object at the given position with the given speed
        /// </summary>
        /// <param name="fishToSpawn">Fish Object to use</param>
        /// <param name="position">Vector2 specifying x and z position of the fish</param>
        /// <param name="speedMultiplier">Optional multiplier for the fish speed</param>
        /// <returns>IFish component created</returns>
        private (IFish,GameObject) SpawnFish(int fishId, Vector2 position, float speedMultiplier = 1)
        {
            IFish toReturn = pool.GetFish(fishId);
            GameObject fish = toReturn.GameObject;
            fish.transform.position = new Vector3(position.x, 0, position.y);
            SoundManager.instance.RandomizeSfxOnObject(fish, out _, fishSounds);
            fish.SetActive(true); //speed is initialized in OnEnable, so this has to go before the speed multiplier.
            toReturn.Speed *= speedMultiplier;
            return (toReturn,fish);
        }

        #endregion //PUBLIC_METHODS

        #region PRIVATE_METHODS
        /// <summary>
        /// Reclaim the fish after waiting for t seconds
        /// </summary>
        /// <param name="fish">Fish to reclaim</param>
        /// <param name="t">Time to wait</param>
        /// <returns></returns>
        IEnumerator DestroyFishAfterTime(IFish fish, float t)
        {
            yield return new WaitForSeconds(t);
            fish.Reclaim();
        }
        #endregion //PRIVATE_METHODS

    }
}
