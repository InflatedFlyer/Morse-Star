using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightcircleround : MonoBehaviour {
    public int direction = 1;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0,0,direction*50 * Time.deltaTime,Space.Self);
	}
}
