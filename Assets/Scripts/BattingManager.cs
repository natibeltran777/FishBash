using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{

    public class BattingManager : GameManager
    {

        #region COROUTINES
        protected override IEnumerator DetractLives()
        {
            isPlayerInvulnerable = true;
            EventManager.TriggerEvent("PLAYERHIT");
            yield return new WaitForSeconds(invulnerableTime);
            isPlayerInvulnerable = false;

        }
        #endregion //COROUTINES


    }
}
