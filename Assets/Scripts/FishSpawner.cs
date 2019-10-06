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
        //[SerializeField]
        //private float cooldown;
        [SerializeField]
        private Slider speedOverride;

        [SerializeField]
        private TextMeshProUGUI currentDistanceTxt;
        [SerializeField]
        private TextMeshProUGUI maxDistanceTxt;

        private bool CanSpawn {
            get => FishManager.instance.FishRemaining < 1;
        }
        public float SpeedOverride => speedOverride.value;
        // Start is called before the first frame update
        void Start()
        {
            Invoke("Setup", 0.5f);
            FishManager.instance.InitializeFishList();
            ViveInput.AddPressDown(HandRole.LeftHand, ControllerButton.Grip, SpawnFish);
            ViveInput.AddPressDown(HandRole.RightHand, ControllerButton.Grip, SpawnFish);
        }

        private void Update()
        {
            currentDistanceTxt.text = HitDistance;

            maxDistanceTxt.text = maxDistance.ToString();
        }

        private GameObject activeFish;
        private IFish fishComponent; 

        public float Distance
        {
            get
            {
                if (activeFish != null && fishComponent != null && fishComponent.HasBeenHit)
                {
                    float d = Vector3.Distance(Vector3.zero, activeFish.transform.position);
                    if (d > maxDistance){
                        maxDistance = d;
                    }
                    return d;

                }
                else
                {
                    return -1f;
                }
            }
        }

        public float maxDistance = 0;

        public string HitDistance
        {
            get
            {
                if(Distance == -1f)
                {
                    return "-";
                }
                else
                {
                    return Distance.ToString();
                }
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
            if (CanSpawn)
            {
                FishContainer f = new FishContainer();
                f.spawnPositionOverride = new Vector2(transform.position.x, transform.position.z);
                f.fishPrefab = fishObj[Random.Range(0, fishObj.Length)];
                activeFish = FishManager.instance.SpawnFish(f, SpeedOverride);
                fishComponent = activeFish.GetComponent<IFish>();

            }
            yield return null;
        }
    }
}
