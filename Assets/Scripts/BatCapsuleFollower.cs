using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCapsuleFollower : MonoBehaviour
{
    private BatCapsule batFollower;
    private Rigidbody rigidbody;
    private Vector3 velocity;

    [SerializeField]
    private float sensitivity = 100f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        Vector3 destination = batFollower.transform.position;
        rigidbody.transform.rotation = transform.rotation;

        velocity = (destination - rigidbody.transform.position) * sensitivity;

        rigidbody.velocity = velocity;
        transform.rotation = batFollower.transform.rotation;
    }

    public void SetFollowTarget(BatCapsule batFollower)
    {
        this.batFollower = batFollower;
    }
}
