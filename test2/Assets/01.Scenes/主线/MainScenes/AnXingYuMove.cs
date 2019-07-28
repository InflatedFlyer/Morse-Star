using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnXingYuMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Transform>().position.x <=15.0f)
            transform.position += new Vector3(1.2f * Time.deltaTime, 0, 0);
	}
}
