using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullScreenSpike : MonoBehaviour {

    public bool collider1;
    public bool collider2;
    public bool ifAttacked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Protection")
            collider2 = true;
        if (other.tag == "FullScreenSpike")
            collider1 = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Protection")
            collider2 = false;
        if (other.tag == "FullScreenSpike")
            collider1 = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Protection")
            collider2 = true;
        if (other.tag == "FullScreenSpike")
            collider1 = true;
    }
    // Use this for initialization
    void Start () {
        collider1 = false;
        collider2 = false;
        ifAttacked = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (collider2 == true)
            ifAttacked = false;
        else
        {
            if (collider1 == true)
                ifAttacked = true;
            if (collider1 == false)
                ifAttacked = false;
        }
	}
}
