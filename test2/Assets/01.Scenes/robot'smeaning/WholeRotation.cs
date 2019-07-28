using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WholeRotation : MonoBehaviour {

    public string state;//state1120,state2240,state3360;

    public Vector3 RotateSpeed;
    private string speedstate;
    private GameObject object1;
    private GameObject object2;
    private GameObject object3;
    public float angle;
	// Use this for initialization
	void Start () {
        object1 = GameObject.FindWithTag("ThreeLittleGameComfire1");
        object2 = GameObject.FindWithTag("ThreeLittleGameComfire2");
        object3 = GameObject.FindWithTag("ThreeLittleGameComfire3");
        state = "state0";
        RotateSpeed = new Vector3(0, -10, 0);
        speedstate = "state1";
        angle = 330;
    }

    // Update is called once per frame
    void Update()
    {
        if (object1.GetComponent<RotationStateJudge>().StateOn == true)
            state = "state1";
        if (object2.GetComponent<RotationStateJudge>().StateOn == true)
            state = "state2";
        if (object3.GetComponent<RotationStateJudge>().StateOn == true)
            state = "state3";

        if (state != "state0")
        {
            if (state == "state1")
            {
                
                if (transform.eulerAngles.y <= 330 && transform.eulerAngles.y >= 210)
                {
                    this.transform.localEulerAngles += Time.deltaTime * RotateSpeed;
                    if (speedstate == "state1")
                    {
                        if (RotateSpeed.y >= -65)
                            RotateSpeed += RotateSpeed * Time.deltaTime;
                        else
                            speedstate = "state2";
                    }
                    else
                    {
                        RotateSpeed += -RotateSpeed * Time.deltaTime;
                    }
                }
                else
                {
                    angle = 210;
                    state = "state0";
                    RotateSpeed = new Vector3(0, -10, 0);
                    speedstate = "state1";
                }
            }

            if (state == "state2")
            {
                
                if (transform.eulerAngles.y <= 210 && transform.eulerAngles.y >= 90)
                {
                    this.transform.localEulerAngles += Time.deltaTime * RotateSpeed;
                    if (speedstate == "state1")
                    {
                        if (RotateSpeed.y >= -65)
                            RotateSpeed += RotateSpeed * Time.deltaTime;
                        else
                            speedstate = "state2";
                    }
                    else
                    {
                        RotateSpeed += -RotateSpeed * Time.deltaTime;
                    }
                }
                else
                {
                    angle = 90;
                    state = "state0";
                    RotateSpeed = new Vector3(0, -10, 0);
                    speedstate = "state1";
                }
            }

            //transform.eulerAngles.y <= 90 && transform.eulerAngles.y >= 0) || (transform.eulerAngles.y >= 330 && transform.eulerAngles.y <= 360
            if (state == "state3")
            {
                if ((transform.eulerAngles.y <= 90 && transform.eulerAngles.y >= 0) || (transform.eulerAngles.y >= 330 && transform.eulerAngles.y <= 360))
                {
                    this.transform.localEulerAngles += Time.deltaTime * RotateSpeed;
                    if (speedstate == "state1")
                    {
                        if (RotateSpeed.y >= -65)
                            RotateSpeed += RotateSpeed * Time.deltaTime;
                        else
                            speedstate = "state2";
                    }
                    else
                    {
                        RotateSpeed += -RotateSpeed * Time.deltaTime;
                    }
                }
                else
                {
                    state = "state0";
                    RotateSpeed = new Vector3(0, -10, 0);
                    speedstate = "state1";
                }
            }
        }
    }
}
