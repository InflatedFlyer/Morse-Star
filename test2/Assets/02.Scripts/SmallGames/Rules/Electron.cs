using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : MonoBehaviour {
    public bool trigger;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetEffect(Element e)
    {
        if(trigger==e.trigger)
        {
            switch(e.type)
            {
                case Element.ElementType.Door:
                    TouchDoor();
                    break;
                case Element.ElementType.Wall:
                    TouchWall();
                    break;
                case Element.ElementType.Spring:
                    TouchSpring();
                    break;
                case Element.ElementType.Spine:
                    TouchSpine();
                    break;
                default:
                    break;
            }
        }
    }

    public void GetEffect(Field f)
    {

    }

    private void TouchDoor()
    {
        Debug.Log("Door");
    }

    private void TouchWall()
    {
        Debug.Log("Wall");
    }

    private void TouchSpring()
    {
        Debug.Log("Spring");
    }

    private void TouchSpine()
    {
        Debug.Log("Spine");
    }
}
