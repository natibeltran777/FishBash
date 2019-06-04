using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using HTC.UnityPlugin.Vive;



public class BatCollision : MonoBehaviour
{
    [SerializeField]
    private HandRole rightController;
    private Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
            Rigidbody rg = collision.collider.attachedRigidbody;
            rg.AddForce(rigidbody.velocity);
            ViveInput.TriggerHapticPulse(rightController,500);

    }
}
