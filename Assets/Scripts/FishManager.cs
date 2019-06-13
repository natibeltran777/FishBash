using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FishBash
{
    public class FishManager : MonoBehaviour
    {
        public int fishToSpawn;
        public int maxRadius;
        public int minRadius;

        public static FishManager instance = null;

        [Range(1,10)]
        public float innerRadius;


        // \todo - Custom editor window to clean this up
        [Range(1, 10)]
        public float speedMax;
        [Range(1, 10)]
        public float speedMin;

        public GameObject[] fishPrefab;
        public GameObject platform;

        private IList<IFish> fishList;

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
            fishList = new List<IFish>();
            for(int i = 0; i < fishToSpawn; i++)
            {
                Vector2 position = Utility.RandomPointOnUnitCircle(maxRadius, minRadius);
                IFish fish = SpawnFish(fishPrefab[UnityEngine.Random.Range(0,fishPrefab.Length)], position, UnityEngine.Random.Range(speedMin, speedMax));
                fishList.Add(fish);
            }
        }


        /// <summary>
        /// Creates a fish of the specified object at the given position with the given speed
        /// </summary>
        /// <param name="fishToSpawn">Fish Object to use</param>
        /// <param name="position">Vector2 specifying x and z position of the fish</param>
        /// <param name="speed">Speed of the newly created fish</param>
        /// <returns>IFish component created</returns>
        IFish SpawnFish(GameObject fishToSpawn, Vector2 position, float speed)
        {
            
            GameObject fish = Instantiate(fishToSpawn, new Vector3(position.x, 0, position.y), new Quaternion(), transform);
            IFish toReturn = fish.GetComponent<IFish>();
            toReturn.SetSpeed(speed);
            return toReturn;
        }

        public void DestroyFish(IFish toDestroy, float t)
        {
            fishList.Remove(toDestroy);
            toDestroy.Destroy(t);
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
