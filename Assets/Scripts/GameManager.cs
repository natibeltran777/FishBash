using FishBash.Waves;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace FishBash
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private bool test = false;

        public int CurrWave { get; private set; } = 0;

        [SerializeField]
        private DisplayTextInViewField textField;

        [SerializeField]
        private readonly int initLives = 3;

        private int lives;
        private bool isPlayerInvulnerable = false;

        /// <summary>
        /// Returns total waves
        /// </summary>
        public int TotalWaves
        {
            get
            {
                return waveList.Length;
            }
        }

        [SerializeField]
        private WaveContainer[] waveList;

        private List<int> fishIDsHitPlayer = new List<int>();

        public static GameManager instance = null;

        //TODO : Do to different manifest files, i think its best to configure a seperate quest/go build rather than trying to have one build that works for both. 
        // However, in that case we should have a global variable to choose between the quest and go builds
        [SerializeField, Tooltip("Do to different manifest files, i think its best to configure a seperate quest/go build rather than trying to have one build that works for both. However, in that case we should have a global variable to choose between the quest and go builds")]
        private bool _isOculusGo;
        public bool IsOculusGo { get => _isOculusGo; }

        #region UNITY_METHODS
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            if (test)
            {
                StartGame();
            }
        }

        #endregion //UNITY_METHODS

        #region PUBLIC_METHODS
        /// <summary>
        /// Begins a new game
        /// </summary>
        public void StartGame()
        {
            FishManager.instance.InitializeFishList();
            lives = initLives;
            CurrWave = 0;
            EventManager.TriggerEvent("GAMESTART");
            StartCoroutine(BeginGame());
        }

        /// <summary>
        /// Exis the game
        /// </summary>
        public void ExitGame()
        {
        #if UNITY_EDITOR
            Debug.Log("Quit game");
        #else
            Application.Quit();
        #endif
         }
         // methods to keep track of the fishes run into player
         public void HandleFishHitPlayer(int itemID)
         {
            if (! fishIDsHitPlayer.Contains(itemID)) {
                fishIDsHitPlayer.Add(itemID);
                Debug.Log("Is hit");
                StartCoroutine(DetractLives());
            }
         }
        #endregion //PUBLIC_METHODS

        #region COROUTINES
        /// <summary>
        /// Central game loop - runs each wave until all waves have been executed
        /// </summary>
        /// <returns></returns>
        IEnumerator BeginGame()
        {
            yield return HandleWaves();
            yield return EndGame(true);
        }

        /// <summary>
        /// Central game loop - runs each wave until all waves have been executed
        /// </summary>
        /// <returns></returns>
        public IEnumerator HandleWaves()
        {
            while (CurrWave < waveList.Length)
            {
                yield return Break(CurrWave);
                IWaves<WaveScriptable> wave = new MainWaves(waveList[CurrWave].subwaves, waveList[CurrWave].timeBetweenSubwaves, instance);
                yield return StartCoroutine(wave.BeginWave());
                CurrWave++;
            }
            yield return null;
        }

        /// \deprecated
        /// <summary>
        /// Given a string outlining the order of fish, breaks string up into enumerable. Uses '.' as a seperator character
        /// </summary>
        /// <param name="order">String to process</param>
        /// <returns>Enumerable list providing order of fish</returns>
        [System.Obsolete] IEnumerable<int> ProcessString(string order)
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
            yield return textField.DisplayText("Beginning wave " + (nextWave + 1) + "...", 3);
        }

        /// <summary>
        /// Handles game ending behavior - for when all waves are over \todo - add losing & winning behavior
        /// </summary>
        /// <returns></returns>
        IEnumerator EndGame(bool isWin)
        {
            if (isWin)
            {
                while (FishManager.instance.FishRemaining > 0)
                {
                    yield return null;
                }
            }
            else
            {
                //DestroyAllFish
            }
            yield return textField.DisplayText("Game Over!", 3);
            EventManager.TriggerEvent("GAMEEND");
        }

        IEnumerator DetractLives()
        {
            if (isPlayerInvulnerable)
            {
                yield return null;
            }
            else
            {
                isPlayerInvulnerable = true;
                EventManager.TriggerEvent("PLAYERHIT");
                lives--;
                if (lives > 0)
                {
                    yield return new WaitForSeconds(1f);
                    isPlayerInvulnerable = false;
                }
                else
                {
                    StopAllCoroutines();
                    yield return EndGame(false);
                }
            }
        }
        #endregion //COROUTINES

    }
}
