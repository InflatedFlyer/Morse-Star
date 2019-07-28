using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leavesfade : MonoBehaviour {

    bool start = false;
    GameObject layer3;
	void Start () {
        layer3 = GameObject.FindGameObjectWithTag("treelayer1");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        start = layer3.GetComponent<fade1>().end;
        if(start)
            GetComponent<ParticleSystem>().loop = false;
	}
}
