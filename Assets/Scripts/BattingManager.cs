using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{

    public class BattingManager : GameManager
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        #region COROUTINES
        private new IEnumerator DetractLives()
        {
            isPlayerInvulnerable = true;
            EventManager.TriggerEvent("PLAYERHIT");
            yield return new WaitForSeconds(invulnerableTime);
            isPlayerInvulnerable = false;

        }
        #endregion //COROUTINES
    }
}
