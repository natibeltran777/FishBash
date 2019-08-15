using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FishBash {
    public class ActiveDuringGame : MonoBehaviour
    {

        [SerializeField, Tooltip("If true, this object will be active during gameplay, and off otherwise. False for the opposite")]
        private bool activeDuringGame;
        [SerializeField, Tooltip("If true, this object will be active when it is first created")]
        private bool activeOnStart;


        void Start()
        {

            if (activeDuringGame)
            {
                EventManager.StartListening("GAMESTART", Activate);
                EventManager.StartListening("GAMEEND", Deactivate);
            }
            else
            {
                EventManager.StartListening("GAMESTART", Deactivate);
                EventManager.StartListening("GAMEEND", Activate);
            }
            if (activeOnStart)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }

        }

        private void Activate()
        {
            this.gameObject.SetActive(true);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}