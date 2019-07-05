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
            FishManager.instance.InitializeWaves();
            StartCoroutine(BeginGame());
        }
        #endregion //PUBLIC_METHODS

        #region COROUTINES
        /// <summary>
        /// Central game loop - runs each wave until all waves have been executed
        /// </summary>
        /// <returns></returns>
        IEnumerator BeginGame()
        {
            yield return FishManager.instance.HandleWaves();
            yield return EndGame();
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