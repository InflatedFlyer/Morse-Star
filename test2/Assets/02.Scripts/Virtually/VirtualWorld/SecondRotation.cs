using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Character")
        {
            other.GetComponent<VirtuallyWalk>().Rotation(315f,15f);
        }
        //EventManager.getInstance().TriggerEvent("FirstRotation");
    }
}
