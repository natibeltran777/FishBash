using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishBash {
	public class PlayerCapsule : MonoBehaviour
	{
	    public void OnTriggerEnter(Collider col)
	    {
	        int itemID = col.gameObject.GetInstanceID();
	        string itemName = col.gameObject.name;

	        GameManager.instance.handleFishHitPlayer(itemID, itemName);
	    }
	}
}

