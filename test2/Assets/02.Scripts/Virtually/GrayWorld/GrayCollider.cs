using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
public class GrayCollider : MonoBehaviour { 
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fish")
        { 
            transform.position = other.transform.position;
            gameObject.GetComponent<VirtuallyWalk>().Stay();
            EventManager.getInstance().TriggerEvent("StayFish");
        }

        if(other.tag=="StoneZone")
        {
            gameObject.GetComponent<VirtuallyWalk>().runSpeed = 10f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "fish")
        {
            EventManager.getInstance().TriggerEvent("LeaveFish");
        }
        if (other.tag == "StoneZone")
        {
            if (transform.position.x > 10)
            {
                EventManager.getInstance().TriggerEvent("LoseControl");
            }
            else
            {
                gameObject.GetComponent<VirtuallyWalk>().runSpeed = 8f;
            }
        }

    }
}
