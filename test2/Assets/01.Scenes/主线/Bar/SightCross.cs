using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SightCross : MonoBehaviour {

    public GameObject Target;
    public float Speed;
    public bool ifSightCross;
    public float left;
    public float right;
    private float originx;
    private float nowx;
	// Use this for initialization
	void Start () {
        nowx = Target.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        originx = nowx;
        nowx = Target.transform.position.x;
        if (nowx != originx)
        {
            if (Target.transform.position.x >= left && Target.transform.position.x <= right)
                ifSightCross = true;
            else
                ifSightCross = false;
            if (ifSightCross)
            {
                if (Input.GetKey(KeyCode.A))
                    transform.localPosition -= Time.deltaTime * Speed * new Vector3(1, 0, 0);
                if (Input.GetKey(KeyCode.D))
                    transform.localPosition += Time.deltaTime * Speed * new Vector3(1, 0, 0);
            }
        }
    }
}
