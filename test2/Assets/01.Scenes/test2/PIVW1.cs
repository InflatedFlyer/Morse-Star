using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIVW1 : MonoBehaviour
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
        if (other.gameObject.tag == "WorldRotarySwitch")
        {
            GameObject.Find("MAP").GetComponent<RotAB>().RC = true;
            GameObject.Find("MAP").GetComponent<RotAB>().RTarget = new Vector3(0, 0, 90f);
            GameObject.Find("MAP").GetComponent<RotAB>().RSpeed = new Vector3(0, 0, 1f);
            GameObject.Find("Player").GetComponent<CharacterController>().enabled = false;
        }
        if( other .gameObject .tag =="LiftSwitch")
        {
            other.GetComponent<RotAB>().PC = true;
        }
    }
}
