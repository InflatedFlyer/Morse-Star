using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jiaocuo : MonoBehaviour
{
    private Vector3 speed=new Vector3(1,0,0);
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
            this.transform.Translate(speed * Time.deltaTime, Space.World);
        else
           if (Input.GetKey(KeyCode.A))
            this.transform.Translate(-speed * Time.deltaTime, Space.World);
    }
}
