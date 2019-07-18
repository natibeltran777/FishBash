using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using HTC.UnityPlugin.Vive;


namespace FishBash
{
    public class BatCollision : MonoBehaviour
    {
        [SerializeField]
        private HandRole rightController;
        private Rigidbody rigidbody;
        private int score = 0;
        private void Start()
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }
        private void OnCollisionEnter(Collision collision)
        {

            Rigidbody rg = collision.collider.attachedRigidbody;
            rg.AddForce(rigidbody.velocity);
            ViveInput.TriggerHapticPulse(rightController, 500);
            fishHasBeenHit(collision.gameObject.GetComponent<IFish>());
            

        }
        private void fishHasBeenHit(IFish fish)
        {
            fish.HasBeenHit = true;
        }
    }

}
