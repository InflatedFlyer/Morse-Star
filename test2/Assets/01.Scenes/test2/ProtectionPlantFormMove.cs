using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectionPlantFormMove : MonoBehaviour {

    public Vector3 MoveSpeed;
    public int direction;
    public float fullspeed;
    public string state;//state1向右加速,state2向右减速,state3向左加速,state4向左减速

	// Use this for initialization
	void Start () {
        MoveSpeed = new Vector3(0, 0, 0);
        state = "state1";
	}
	
	// Update is called once per frame
	void Update () {
        SpeedChange();
        Move();
        fullspeed = 0.08f;
    }

    private void Move()
    {
        this.transform.localPosition += MoveSpeed;
    }
    private void SpeedChange()
    {
        if(state=="state1")
        {
            if (MoveSpeed.x <= fullspeed)
                MoveSpeed += Time.deltaTime*Time.deltaTime*new Vector3(1, 0, 0);
           else if (MoveSpeed.x >= fullspeed)
                state = "state2";
        }
        else
            if(state=="state2")
        {
            if (MoveSpeed.x >= 0)
                MoveSpeed -= Time.deltaTime * Time.deltaTime * new Vector3(1, 0, 0);
            else if (MoveSpeed.x <= 0)
                state = "state3";
        }
        else
            if(state=="state3")
        {
            if (MoveSpeed.x <= 0 && MoveSpeed.x >= -fullspeed)
                MoveSpeed -= Time.deltaTime * Time.deltaTime * new Vector3(1, 0, 0);
            else if (MoveSpeed.x <= -fullspeed)
                state = "state4";
        }
        else
            if(state=="state4")
        {
            if (MoveSpeed.x <= 0)
                MoveSpeed += Time.deltaTime * Time.deltaTime * new Vector3(1, 0, 0);
           else if (MoveSpeed.x >= 0)
                state = "state1";
        }
    }
}