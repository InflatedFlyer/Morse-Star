using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class easywalk : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.D))
            transform.position += (new Vector3(4, 0, 0)) * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
            transform.position += (new Vector3(-4, 0, 0)) * Time.deltaTime;
    }
}
