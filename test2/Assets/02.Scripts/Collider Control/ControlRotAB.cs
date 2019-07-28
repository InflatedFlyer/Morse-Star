using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlRotAB : MonoBehaviour
{
    public GameObject obj;
    public bool PC, RC, SC, AC;
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
        if (other .gameObject .tag == "Player")
        {
            Debug.Log(" Collider Player");
            if (PC)
            {
                obj.GetComponent<RotAB>().PC = true;
            }
            if (RC)
            {
                obj.GetComponent<RotAB>().RC = true;
            }
            if (SC)
            {
                obj.GetComponent<RotAB>().SC = true;
                Debug.Log(" Collider Player");
            }
            if (AC)
            {
                obj.GetComponent<RotAB>().AC = true;
            }
        }
    }
}
