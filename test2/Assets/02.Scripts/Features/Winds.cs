using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winds : MonoBehaviour {

    test_VirtuallyWalk virtual_liming=null;
    public Vector2 current_wind = new Vector2(-4,6);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("stay");
        if (virtual_liming == null)
        {
            virtual_liming = other.transform.GetComponent<test_VirtuallyWalk>();
        }
        if (virtual_liming != null)
        { 
            virtual_liming.ChangeWind(current_wind);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        Debug.Log("leave");
        if (virtual_liming == null)
        {
            virtual_liming = other.transform.GetComponent<test_VirtuallyWalk>();
        }
        if (virtual_liming != null)
        {
            virtual_liming.ChangeWind(Vector2.zero);
        }
    }
}
