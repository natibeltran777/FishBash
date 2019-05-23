using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FishBash
{
    public class FishManager : MonoBehaviour
    {
        public int fishToSpawn;
        public int radius;
        public GameObject fishPrefab;
        public GameObject platform;

        private IList<IFish> fishList;
        
        // Start is called before the first frame update
        void Start()
        {
            fishList = new List<IFish>();
            for(int i = 0; i < fishToSpawn; i++)
            {
                IFish fish = SpawnFish(fishPrefab);
                fishList.Add(fish);
                fish.SetGoal(platform);
            }
        }

        IFish SpawnFish(GameObject fishToSpawn)
        {
            Vector2 position = Utility.RandomPointOnUnitCircle(radius);
            GameObject fish = Instantiate(fishToSpawn, new Vector3(position.x, 0, position.y), new Quaternion(), transform);
            IFish toReturn = fish.GetComponent<IFish>();
            return toReturn;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
