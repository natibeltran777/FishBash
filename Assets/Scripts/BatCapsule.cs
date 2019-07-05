using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCapsule : MonoBehaviour
{
    [SerializeField]
    public BatCapsuleFollower batCapsuleFollower;
    // Start is called before the first frame update
    void Start()
    {
        SpawnBatCapsuleFollower();
    }

    private void SpawnBatCapsuleFollower()
    {
        var follower = Instantiate(batCapsuleFollower);
        follower.transform.position = transform.position;
        follower.SetFollowTarget(this);
    }
}
