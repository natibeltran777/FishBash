using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FishBash {


    public class FishSpawner : MonoBehaviour
    {

        [SerializeField]
        private GameObject[] fishObj;
        private int[] fishIds;
        //[SerializeField]
        //private float cooldown;
        [SerializeField]
        private Slider speedOverride;

        [SerializeField]
        private TextMeshProUGUI currentDistanceTxt;
        [SerializeField]
        private TextMeshProUGUI maxDistanceTxt;

        private bool canSpawn = true;
        public float SpeedOverride => speedOverride.value;
        // Start is called before the first frame update
        void Start()
        {
            fishIds = new int[fishObj.Length];
            for(int i = 0; i < fishIds.Length; i++)
            {
                fishIds[i] = fishObj[i].GetComponent<IFish>().FishId;
            }
            Invoke("Setup", 0.5f);
            FishManager.instance.InitializeFishList();
            ViveInput.AddPressDown(HandRole.LeftHand, ControllerButton.Grip, SpawnFish);
            ViveInput.AddPressDown(HandRole.RightHand, ControllerButton.Grip, SpawnFish);
            activeFishes = new List<GameObject>();
            fishComponents = new List<IFish>();
            distance = new List<float>();
        }

        private void Update()
        {
            UpdateDistance();
            currentDistanceTxt.text = HitDistance;

            maxDistanceTxt.text = maxDistance.ToString();
        }

        private List<GameObject> activeFishes;
        private List<IFish> fishComponents;

        public List<float> distance;

        public float maxDistance = 0;

        public string HitDistance
        {
            get
            {
                string val = "-";
                foreach (float f in distance)
                {
                    if (f > 0)
                    {
                        val = f.ToString();
                        if (f > maxDistance) maxDistance = f;
                    }
                }
                return val;
            }
        }

        private void UpdateDistance()
        {
            List<int> deadIndexes = new List<int>();
            for(int i = 0; i < activeFishes.Count; i++)
            {
                if (activeFishes[i] == null || !activeFishes[i].activeSelf) deadIndexes.Add(i);
                else
                {
                    if(fishComponents[i].HasBeenHit)
                        distance[i] = fishComponents[i].Distance;
                }
            }

            foreach(int i in deadIndexes)
            {
                activeFishes.RemoveAt(i);
                fishComponents.RemoveAt(i);
                distance.RemoveAt(i);
            }
        }

        private void OnDestroy()
        {
            //EventManager.TriggerEvent("GAMEEND");
        }

        private void Setup()
        {
            EventManager.TriggerEvent("GAMESTART");
        }

        public void SpawnFish()
        {
            StartCoroutine(SpawnFish_());
        }

        private IEnumerator SpawnFish_()
        {
            if (canSpawn)
            {
                canSpawn = false;
                FishContainer f = new FishContainer();
                f.spawnPositionOverride = new Vector2(transform.position.x, transform.position.z);
                f.fishId = fishIds[Random.Range(0, fishIds.Length)];
                GameObject spawned = FishManager.instance.SpawnFish(f, SpeedOverride);
                fishComponents.Add(spawned.GetComponent<IFish>());
                activeFishes.Add(spawned);
                distance.Add(-1f);

                yield return new WaitForSeconds(0.2f);

                canSpawn = true;
            }
            yield return null;
        }
    }
}
