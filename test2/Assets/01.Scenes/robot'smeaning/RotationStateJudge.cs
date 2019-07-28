using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationStateJudge : MonoBehaviour {
    public bool StateOn;
	// Use this for initialization
	void Start () {
        StateOn = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ThreeLittleGameSphere")
            StateOn = true;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
