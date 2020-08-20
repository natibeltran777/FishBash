using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FishBash
{
    public class HandCanvasController : MonoBehaviour
    {
        [SerializeField]
        private VivePoseTracker parentTracker;
        [SerializeField]
        private GameObject canvasObj;
        [SerializeField]
        private TextMeshProUGUI livesLabel;
        [SerializeField]
        private TextMeshProUGUI pointsLabel;

        // Start is called before the first frame update
        void Start()
        {

            canvasObj.transform.SetParent(parentTracker.transform);

            canvasObj.SetActive(false);
            if(BatHandler.Instance.BatPose.IsRole(HandRole.RightHand))
            {
                parentTracker.viveRole.SetEx(HandRole.LeftHand);
            }
            else
            {
                parentTracker.viveRole.SetEx(HandRole.RightHand);
            }
            BatHandler.Instance.BatPose.onRoleChanged += ChangeHand;
            EventManager.StartListening("GAMESTART", GameStart);
            EventManager.StartListening("GAMEEND", GameEnd);
            EventManager.StartListening("PLAYERHIT", PlayerHit);
            EventManager.StartListening("FISHHIT", FishHit);
        }

        void ChangeHand()
        {
            if (parentTracker.viveRole.IsRole(HandRole.RightHand))
            {
                parentTracker.viveRole.SetEx(HandRole.LeftHand);
            }
            else
            {
                parentTracker.viveRole.SetEx(HandRole.RightHand);
            }
        }

        void GameStart()
        {
            livesLabel.text = GameManager.instance.CurrLives.ToString();
            canvasObj.SetActive(true);
        }

        void GameEnd()
        {
            canvasObj.SetActive(false);
        }

        void PlayerHit()
        {
            livesLabel.text = GameManager.instance.CurrLives.ToString();
        }

        void FishHit()
        {
            pointsLabel.text = GameManager.instance.GetScore.ToString();
        }
    }
}
