using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash {
	public class PlayerCapsule : MonoBehaviour
	{
        private Transform cameraTransform;
        private float offset;
        private void Start()
        {
            cameraTransform = Camera.main.transform;
            offset = transform.localPosition.y;
        }

        private void Update()
        {
            Vector3 position = cameraTransform.position;
            position.y += offset;
            transform.position = position;
        }
        public void OnTriggerEnter(Collider col)
	    {
	        int itemID = col.gameObject.GetInstanceID();

	        GameManager.instance.HandleFishHitPlayer(itemID);
	    }
	}
}

