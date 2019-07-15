using FishBash.Waves;
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

        [SerializeField]
        private GameObject menu;
        [SerializeField]
        private GameObject pointers;

        public int CurrWave { get; private set; } = 0;

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

        private bool hasGameStarted = false;

        public static GameManager instance = null;

        public TextMeshProUGUI uiText;

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
            hasGameStarted = true;
            menu.SetActive(false);
            pointers.SetActive(false);
            FishManager.instance.InitializeFishList();
            CurrWave = 0;
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
        #endregion //PUBLIC_METHODS

        #region COROUTINES
        /// <summary>
        /// Central game loop - runs each wave until all waves have been executed
        /// </summary>
        /// <returns></returns>
        IEnumerator BeginGame()
        {
            yield return HandleWaves();
            yield return EndGame();
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
            yield return DisplayText("Beginning wave " + (nextWave + 1) + "...", 3);
        }

        /// <summary>
        /// Handles game ending behavior - for when all waves are over \todo - add losing & winning behavior
        /// </summary>
        /// <returns></returns>
        IEnumerator EndGame()
        {
            while (FishManager.instance.FishRemaining > 0)
            {
                yield return null;
            }
            yield return DisplayText("Game Over!", 3);
            hasGameStarted = true;
            menu.SetActive(true);
            pointers.SetActive(true);
        }

        /// <summary>
        /// Displays given text on screen for a set period of time
        /// </summary>
        /// <param name="text">Text to display</param>
        /// <param name="timeToDisplay">Time to display text</param>
        /// <returns></returns>
        public IEnumerator DisplayText(string text, float timeToDisplay)
        {
            uiText.text = text;
            uiText.gameObject.SetActive(true);
            yield return new WaitForSeconds(timeToDisplay);
            uiText.gameObject.SetActive(false);
            yield return null;
        }
        #endregion //COROUTINES

    }
}