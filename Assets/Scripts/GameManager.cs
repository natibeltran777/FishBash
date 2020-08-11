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
        public static GameManager instance = null;

        [SerializeField]
        private bool test = false;
        [SerializeField]
        private DisplayTextInViewField textField;
        [SerializeField]
        private int initLives = 3;
        [SerializeField, Tooltip("After getting hit, the player will be invulnerable for this long")]
        protected float invulnerableTime = 0.5f;
        [SerializeField]
        protected int playerScore = 0;
        [SerializeField]
        private WaveContainer[] waveList;


        protected int lives;
        protected bool isPlayerInvulnerable = false;
        
        private List<int> fishIDsHitPlayer = new List<int>();
        private IEnumerator currentExecutingWave;
        private IEnumerator currentExecutingGame;
        private IEnumerator waveHandler;

        [SerializeField] private Transform m_homingTarget;

        //TODO : Do to different manifest files, i think its best to configure a seperate quest/go build rather than trying to have one build that works for both. 
        // However, in that case we should have a global variable to choose between the quest and go builds
        [SerializeField, Tooltip("Do to different manifest files, i think its best to configure a seperate quest/go build rather than trying to have one build that works for both. However, in that case we should have a global variable to choose between the quest and go builds")]
        private bool _isOculusGo;

        public bool IsOculusGo { get => _isOculusGo; }
        public int CurrWave { get; private set; } = 0;
        public int GetScore { get => playerScore; }
        public int CurrLives { get => lives; }

        public delegate void GameManagerDelegate();
        public event GameManagerDelegate OnTargetChanged;

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

        /// <summary>
        /// When homing target has been changed we fire an event to let the homing functionality know what the new target is
        /// </summary>
        public Transform Target
        {
            set
            {
                if (OnTargetChanged != null) OnTargetChanged();
                m_homingTarget = value;
            }
            get
            {
                return m_homingTarget;
            }
        }



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
            //Debug.Log("start game");
            FishManager.instance.InitializeFishList();
            lives = initLives;
            playerScore = 0;
            CurrWave = 0;
            EventManager.TriggerEvent("GAMESTART");
            currentExecutingGame = BeginGame();
            StartCoroutine(currentExecutingGame);
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
                if (!isPlayerInvulnerable)
                {
                    StartCoroutine(DetractLives());
                }
            }
         }

        public void IncrementScore(int toAdd)
        {
            playerScore += toAdd;
            //Debug.Log("Fish Hit! Score is: " + playerScore);
            //StartCoroutine(DisplayText(scoreText, 3));
        }
        #endregion //PUBLIC_METHODS

        /// <summary>
        /// Handles game ending behavior - for when all waves are over \todo - add losing & winning behavior
        /// </summary>
        private void EndGame(bool isWin)
        {
            if (isWin)
            {
                //Win behaviour

            }
            else
            {
                //Lose behaviour
                FishManager.instance.DestroyAllFish();
                StopAllCoroutines();
            }

            StartCoroutine(EndGameDisplay());
            
        }

        #region COROUTINES
        /// <summary>
        /// Central game loop - runs each wave until all waves have been executed
        /// </summary>
        /// <returns></returns>
        IEnumerator BeginGame()
        {
            waveHandler = HandleWaves();
            yield return StartCoroutine(waveHandler);
            EndGame(true);
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
                currentExecutingWave = wave.BeginWave();
                yield return StartCoroutine(currentExecutingWave);
                CurrWave++;
            }
            yield return null;
        }

        /// <summary>
        /// Given a string outlining the order of fish, breaks string up into enumerable. Uses '.' as a seperator character
        /// </summary>
        /// <param name="order">String to process</param>
        /// <returns>Enumerable list providing order of fish</returns>
        [Obsolete] IEnumerable<int> ProcessString(string order)
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
        
        IEnumerator EndGameDisplay()
        {
            Debug.Log("End game");
            while (FishManager.instance.FishRemaining > 0)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1f);
            EventManager.TriggerEvent("GAMEEND");
            yield return textField.DisplayText("Game Over!", 3);
        }

        protected virtual IEnumerator DetractLives()
        {
            isPlayerInvulnerable = true;
            lives--;
            EventManager.TriggerEvent("PLAYERHIT");
            if (lives > 0)
            {
                yield return new WaitForSeconds(invulnerableTime);
                isPlayerInvulnerable = false;
            }
            else
            {
                isPlayerInvulnerable = false;
                EndGame(false);
            }
        }
        #endregion //COROUTINES

    }
}
