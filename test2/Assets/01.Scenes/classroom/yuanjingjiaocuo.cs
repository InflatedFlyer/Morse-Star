using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yuanjingjiaocuo : MonoBehaviour {

    Vector3 move = new Vector3(0, 0, 0);
    float changex;
    float xx;
    public float m = 2;
    public GameObject camera;
	void Start () {
        move.x = this.gameObject.transform.localPosition.x;
        changex = camera.transform.localPosition.x;
	}

	void Update () {
        xx = camera.transform.localPosition.x - changex;
        changex = camera.transform.localPosition.x;
        move.x = xx / m;
        this.gameObject.transform.Translate(move);
	}
}
