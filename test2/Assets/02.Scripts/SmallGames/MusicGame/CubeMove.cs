using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CubeMove : MonoBehaviour {

    [HideInInspector]
    public float horizon_speed = 5f;
    [HideInInspector]
    public float vertical_speed = 5f;
    [HideInInspector]
    private int vertical_direct = 0;

    [HideInInspector]
    public bool gameStart=false;
    [HideInInspector]
    public bool gameOver = false;
	// Use this for initialization
	void Start () {
		
	}
    private void Update()
    {

    }
    private void FixedUpdate()
    {
    }
}
