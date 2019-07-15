using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatHandler : MonoBehaviour
{
    [SerializeField]
    private bool rightHandIsOn = true;
    [SerializeField]
    private GameObject leftHand;
    [SerializeField]
    private GameObject rightHand;
    [SerializeField]
    private GameObject leftHandObj;
    [SerializeField]
    private GameObject rightHandObj;
    // Start is called before the first frame update
    void Start()
    {
        if (rightHandIsOn)
        {
            SetRightHand();
        }
        else
        {
            SetLeftHand();
        }
    }

    private void SetRightHand()
    {
        transform.SetParent(rightHand.transform);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        rightHandObj.SetActive(false);
        leftHandObj.SetActive(true);
    }
    private void SetLeftHand()
    {
        transform.SetParent(leftHand.transform);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        rightHandObj.SetActive(true);
        leftHandObj.SetActive(false);
    }

    public void RightTrigger()
    {
        if (!rightHandIsOn)
        {
            SetRightHand();
            rightHandIsOn = true;
        }
    }

    public void LeftTrigger()
    {
        if (rightHandIsOn)
        {
            SetLeftHand();
            rightHandIsOn = false;
        }
    }
}
