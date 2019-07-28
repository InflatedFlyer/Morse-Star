using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerSize : MonoBehaviour
{
    private Vector4 S;
    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other . gameObject .tag =="bigger")
        {

            GameObject.Find("Sphere").GetComponent<RotAB>().SBegin = GameObject.Find("Sphere").transform.localScale;
            GameObject.Find("Sphere").GetComponent<RotAB>().STarget = GameObject.Find("Sphere").GetComponent<RotAB>().SBegin * 1.6f;
            GameObject.Find("Sphere").GetComponent<RotAB>().SSpeed = new Vector3(0.05f, 0.05f, 0.05f);

            GameObject.Find("Sphere").GetComponent<RotAB>().SC = true;
        }
    }
    void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "bigger")
        {
            GameObject.Find("Sphere").GetComponent<RotAB>().SC = false ;
        }
    }
}
