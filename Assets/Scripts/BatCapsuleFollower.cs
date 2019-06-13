using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCapsuleFollower : MonoBehaviour
{
    private BatCapsule batCapsuleToFollow;
    private Rigidbody rigidbody;
    private Vector3 velocity;

    [SerializeField]
    private float sensitivity = 100f;

    /// <summary>
    /// Initialize rigid body component of the BatFollower
    /// </summary>
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    /// <summary>
    /// Destination is the capsule on the bat. 
    /// We get a velocity vector by subtracting the destination by the current position of the batfollower
    /// So when we swing the bat, the follower follows right behind it at a certain velocity. 
    /// This is what gives our swings power. 
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 destination = batCapsuleToFollow.transform.position;
        rigidbody.transform.rotation = transform.rotation;
        velocity = (destination - rigidbody.transform.position) * sensitivity;

        rigidbody.velocity = velocity;
        transform.rotation = batCapsuleToFollow.transform.rotation;
    }

    public void SetFollowTarget(BatCapsule batFollower)
    {
        this.batCapsuleToFollow = batFollower;
    }
}
