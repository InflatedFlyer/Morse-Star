using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactermove : MonoBehaviour
{
    public float m=3;
    private Vector3 speed = new Vector3(0,0,0);
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position = new Vector3 (this.transform.position.x + m * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position = new Vector3(this.transform.position.x - m * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + m * Time.deltaTime, this.transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position = new Vector3(this.transform.position.x , this.transform.position.y - m * Time.deltaTime, this.transform.position.z);

        }
    }
}
