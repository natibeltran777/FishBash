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
    private int score = 0;
    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
    
        Rigidbody rg = collision.collider.attachedRigidbody;
        rg.AddForce(rigidbody.velocity);
        ViveInput.TriggerHapticPulse(rightController,500);
        incrementScore(1);
        Debug.Log("Score is now " + score);
        
    }

    private void incrementScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
