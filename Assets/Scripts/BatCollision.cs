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
		private int score = 0;

        private void Start()
        {
            rb = gameObject.GetComponent<Rigidbody>();
        }
        private void OnCollisionEnter(Collision collision)
        {

            Rigidbody rg = collision.collider.attachedRigidbody;
            fishHasBeenHit(collision.gameObject.GetComponent<IFish>());
            rg.AddForce(rb.velocity);
            ViveInput.TriggerHapticPulse(BatHandler.instance.BatPose, 500);

        }

        private void fishHasBeenHit(IFish fish)
        {
            fish.HasBeenHit = true;
        }
    }

}
