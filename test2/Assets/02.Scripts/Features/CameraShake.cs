using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
public class CameraShake : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ProCamera2DShake.Instance.Shake(0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
