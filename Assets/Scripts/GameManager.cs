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
        private List<int> fishIDsHitPlayer = new List<int>();

        public static GameManager instance = null;

        //TODO : Do to different manifest files, i think its best to configure a seperate quest/go build rather than trying to have one build that works for both. 
        // However, in that case we should have a global variable to choose between the quest and go builds
        [SerializeField, Tooltip("Do to different manifest files, i think its best to configure a seperate quest/go build rather than trying to have one build that works for both. However, in that case we should have a global variable to choose between the quest and go builds")]
        private bool _isOculusGo;
        public bool IsOculusGo { get => _isOculusGo; }

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
         // methods to keep track of the fishes run into player
         public void HandleFishHitPlayer(int itemID, string itemName)
         {
            if (! fishIDsHitPlayer.Contains(itemID) && itemName.Contains("Fish")) {
                fishIDsHitPlayer.Add(itemID);
                print("=== in game manager === HIT " + fishIDsHitPlayer.Count);
            }
         }

         public void RelocateMenuOnTurn(float angle)
         {
            float radian_angle = angle * Mathf.Deg2Rad;
            float side = menu.transform.position.z;
            float area = side * side * Mathf.Sin(radian_angle) / 2.0f;
            float new_x = 2.0f * area / side;

            float radian_another_angle = (180 - 90 - Mathf.Abs(angle)) * Mathf.Deg2Rad;
            float radian_right_angle = 90 * Mathf.Deg2Rad;
            float new_z = side * Mathf.Sin(radian_another_angle) /  Mathf.Sin(radian_right_angle);
            print("=== on turn === " + new_x + " " + new_z);
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
