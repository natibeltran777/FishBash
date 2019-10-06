using HTC.UnityPlugin.Vive;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash
{
    public class BattingRaycast : MonoBehaviour
    {

        private VivePoseTracker parentTracker;
        // Start is called before the first frame update
        void Start()
        {
            parentTracker = GetComponent<VivePoseTracker>();

            if (BatHandler.instance.BatPose.IsRole(HandRole.RightHand))
            {
                parentTracker.viveRole.SetEx(HandRole.LeftHand);
            }
            else
            {
                parentTracker.viveRole.SetEx(HandRole.RightHand);
            }
            BatHandler.instance.BatPose.onRoleChanged += ChangeHand;
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
    }
}
