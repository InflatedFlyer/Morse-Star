using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateboardShoes : MonoBehaviour {
   
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input .GetKey (KeyCode .Space ))
        {
            ChangeGravity(5);
        }
        if(Input .GetKeyUp (KeyCode .Space ))
        {
            ChangeGravity(20);
        }
	}

    private void ChangeGravity(int Gravity)
    {
      GetComponent<VirtuallyWalk>().gravity = Gravity;
    }
}
