using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COllider : MonoBehaviour {

   
    public string WhatCollider;             //碰撞的东西的tag
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay(Collider mCollider)
    {
        EventManager.getInstance().TriggerEvent("Stay" + mCollider.gameObject.tag);
    }

    void OnTriggerExit(Collider mCollider)
    {
        EventManager.getInstance().TriggerEvent("Leave" + mCollider.gameObject.tag);
    }
}
