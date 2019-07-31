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

        /*
        public void OnTriggerEnter(Collider col)
        {
            int itemID = col.gameObject.GetInstanceID();
            string itemName = col.gameObject.name;

            if (! fishesCatched.Contains(itemID) && itemName.Contains("Fish")) {
                fishesCatched.Add(itemID);
            }
            print("=== on trigger === " + gameObject.name +" dynamically meets " + itemName + " - " + itemID + " - length: " + fishesCatched.Count);
        }
        */
    }
}
