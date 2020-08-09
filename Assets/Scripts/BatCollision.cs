using System;
using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;
using HTC.UnityPlugin.Vive;


namespace FishBash {
    public class BatCollision : MonoBehaviour
    {
        private Rigidbody batCollisionRigidBody;
        private List<int> fishesCatched = new List<int>();
        private BattingManager battingManager;

        private float m_speedToActivateHoming = 10.0f;
        private float m_angleToactivateBezier = 180f;

        private void Start()
        {
            batCollisionRigidBody = gameObject.GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Transform target = BattingManager.instance.Target;
            Rigidbody fishRigidBody = collision.collider.attachedRigidbody;
            //collision.contacts[0].normal;
            if (BattingManager.instance.Target != null)
            {
                if (batCollisionRigidBody.velocity.magnitude > m_speedToActivateHoming && fishRigidBody.velocity.magnitude != 0)
                {
                    Vector3 finalPosition = target.position;
                   // Vector3 directionOfImpact = this.GetComponent<Rigidbody>().velocity.normalized;
                    Vector3 directionOfImpact = -collision.GetContact(0).normal;
                    Vector3 initialPosition = collision.transform.position;
                    Vector3 finalPosDir = finalPosition - initialPosition;
                    if (Vector3.Angle(finalPosDir.normalized, directionOfImpact.normalized) < m_angleToactivateBezier)
                    {
                        collision.gameObject.GetComponent<IFish>().HomingHit(initialPosition, finalPosition, directionOfImpact, batCollisionRigidBody.velocity);
                        ViveInput.TriggerHapticPulse(BatHandler.instance.BatPose, 500);
                        return;
                    }
                }
                collision.gameObject.GetComponent<IFish>().HitFish();
                fishRigidBody.AddForce(batCollisionRigidBody.velocity);
                ViveInput.TriggerHapticPulse(BatHandler.instance.BatPose, 500);

            }
        }
    }
}
