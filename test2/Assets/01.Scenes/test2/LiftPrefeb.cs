using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftPrefeb : MonoBehaviour
{
    public float speed;
    //-33   33
    public Vector3 reset;
    public Vector3 stop;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition += (stop - reset) / 100 * speed*Time.deltaTime;
        //if ( (this.transform.localPosition.x-reset.x)/(stop.x-reset.x)<0|| (this.transform.localPosition.y - reset.y) / (stop.y - reset.y) < 0
        //    || (this.transform.localPosition.z - reset.z) / (stop.z - reset.z) < 0)
        if ((this.transform.localPosition - stop).x * (this.transform.localPosition - stop).x + (this.transform.localPosition - stop).y * (this.transform.localPosition - stop).y
            + (this.transform.localPosition - stop).z * (this.transform.localPosition - stop).z <= 0.02)
            this.transform.localPosition = reset;
    }
}
