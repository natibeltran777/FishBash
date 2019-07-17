using System;
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
    private List<int> fishesCatched = new List<int>();

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
    public void OnTriggerEnter(Collider col)
    {
        int itemID = col.gameObject.GetInstanceID();
        string itemName = col.gameObject.name;

        if (! fishesCatched.Contains(itemID) && itemName.Contains("Fish")) {
            fishesCatched.Add(itemID);
        }
        print("=== on trigger === " + gameObject.name +" dynamically meets " + itemName + " - " + itemID + " - length: " + fishesCatched.Count);
    }
}
