using System;
using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using HTC.UnityPlugin.Vive;


namespace FishBash {
    public class BatCollision : MonoBehaviour
    {
        private Rigidbody rb;
        private List<int> fishesCatched = new List<int>();

        private void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
        private void OnCollisionEnter(Collision collision)
        {

            Rigidbody rg = collision.collider.attachedRigidbody;
            rg.AddForce(rb.velocity);
            ViveInput.TriggerHapticPulse(BatHandler.instance.BatPose, 500);

        }
    }
}
