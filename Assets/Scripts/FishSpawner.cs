using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FishBash {


    public class FishSpawner : MonoBehaviour
    {

        [SerializeField]
        private GameObject[] fishObj;
        [SerializeField]
        private float cooldown;

        private bool canSpawn = false;

        // Start is called before the first frame update
        void Start()
        {
            Invoke("Setup", 0.5f);
            FishManager.instance.InitializeFishList();
            ViveInput.AddPressDown(HandRole.LeftHand, ControllerButton.Grip, SpawnFish);
            ViveInput.AddPressDown(HandRole.RightHand, ControllerButton.Grip, SpawnFish);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDestroy()
        {
            EventManager.TriggerEvent("GAMEEND");
        }

        private void Setup()
        {
            EventManager.TriggerEvent("GAMESTART");
            canSpawn = true;
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
                Vector2 spawnPoint = new Vector2(transform.position.x, transform.position.z);
                int index = Random.Range(0, fishObj.Length - 1);
                FishManager.instance.SpawnFish(fishObj[index], spawnPoint);
                yield return new WaitForSeconds(cooldown);
                canSpawn = true;
            }
            yield return null;
        }
    }
}
