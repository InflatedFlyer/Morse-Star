using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryTriangleRotate : MonoBehaviour
{
    public float rotatespeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localEulerAngles += new Vector3(transform.localEulerAngles.x,Time.deltaTime * rotatespeed,transform.localEulerAngles.z);
    }
}
