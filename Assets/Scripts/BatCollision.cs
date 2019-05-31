using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;



public class BatCollision : MonoBehaviour
{
    private Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
            Rigidbody rg = collision.collider.attachedRigidbody;
            rg.AddForce(2 * rigidbody.velocity);

    }
}
