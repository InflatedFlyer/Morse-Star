﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftSwitchChangeWind : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Character")
        {
            other.GetComponent<VirtuallyWalk>().ChangeWind(new Vector2(0,GetComponent<RotAB>().vertical_speed));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Character")
        {
            other.GetComponent<VirtuallyWalk>().ChangeWind(Vector2.zero);
        }
    }
}