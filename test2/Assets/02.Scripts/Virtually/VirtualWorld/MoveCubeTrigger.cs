using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeTrigger : MonoBehaviour
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
        if(other.tag=="Character")
        {
            other.GetComponent<VirtuallyWalk>().ChangeWind(new Vector2(GetComponent<RotAB>().horizon_speed, 0));
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
