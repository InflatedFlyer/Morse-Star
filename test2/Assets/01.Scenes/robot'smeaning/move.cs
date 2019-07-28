using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

    private bool fade;
    public bool enablemove;
    public string state;
    public GameObject thisobject;
    public GameObject everything;
    private bool protect;
    public GameObject thiscamera;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "fadeblock")
        {
            if (!protect)
            {
                enablemove = false;
                state = "stop";
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ThreeLittleGameComfire1")
            enablemove = false;

    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "ThreeLittleGameComfire1")
    //        state = "state1";
    //    if (other.tag == "ThreeLittleGameComfire2")
    //        state = "state2";
    //    if (other.tag == "ThreeLittleGameComfire3")
    //        state = "state3";
    //}
    // Use this for initialization
    void Start () {
        fade = false;
        enablemove = true;
        protect = false;
        thiscamera = GameObject.Find("Main Camera");
	}
	//130 90   300 370     270 260
	// Update is called once per frame
	void Update () {
        if (enablemove)
        {
            //transform.localPosition += new Vector3(0f, 0, 0.6f);
            if (Input.GetKey(KeyCode.D)||Input.GetKey(KeyCode.RightArrow))
                transform.localPosition += new Vector3(0.2f, 0, 0);
            if (Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.LeftArrow))
                transform.localPosition += new Vector3(-0.2f, 0, 0);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                transform.localPosition += new Vector3(0, 0, 0.2f);
            if(Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.DownArrow))
                transform.localPosition += new Vector3(0, 0, -0.2f);
        }
        if (everything.GetComponent<WholeRotation>().state == "state1" )
        {
            transform.localPosition = new Vector3(-250, -92, -250);
            enablemove = false;
            protect = true;
            thiscamera.GetComponent<ThreeLittleGameCamera>().Target = everything;
            //Debug.Log("K");
            thiscamera.GetComponent<ThreeLittleGameCamera>().TargetView = 200;

        }
        if (everything.GetComponent<WholeRotation>().state == "state2")
        {
            transform.localPosition = new Vector3(-140, -92, -240);
            enablemove = false;
            protect = true;
            thiscamera.GetComponent<ThreeLittleGameCamera>().Target = everything;
            Debug.Log("K");
            thiscamera.GetComponent<ThreeLittleGameCamera>().TargetView = 200;
        }
        if (everything.GetComponent<WholeRotation>().angle == 210 || everything.GetComponent<WholeRotation>().angle == 90)
        {
            enablemove = true;
            protect = false;
            thiscamera.GetComponent<ThreeLittleGameCamera>().TargetView = 100;
            thiscamera.GetComponent<ThreeLittleGameCamera>().Target = GameObject.Find("Sphere");
        }
    }
}
