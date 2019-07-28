using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stonedown : MonoBehaviour
{

    private float adddownspeed = 3;
    public bool trigger1 = false;
    private bool trigger2 = false;
    private float y;
    private bool add = false;
    private float t=0;

    private void Awake()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Rigidbody>().useGravity = true;
        Invoke("addspeed", 0.3f);
        trigger1 = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        trigger2 = true;
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().useGravity = true;
    }

    void Start()
    {
        y = this.transform.localPosition.y;
    }

    void Update()
    {
        if (!trigger2 && trigger1 && GetComponent<Rigidbody>().useGravity == true)
        {
            t += Time.deltaTime;
            GetComponent<Transform>().position -= (new Vector3(0, adddownspeed, 0) *t*t);
            //if(add)
                GetComponent<Transform>().position -= (new Vector3(0, adddownspeed*3, 0) *t*t);
        }
        if (trigger2)
        {
           // GetComponent<Transform>().position -= (new Vector3(0, adddownspeed, 0) * Time.deltaTime);
        }
        //{
        //    if (this.transform.localPosition.y == 0)
        //    {

        //    }
        //   // GetComponent<Rigidbody>().useGravity = false;

        //    //if (this.transform.localPosition.y < y)
        //    //{

        //    //    GetComponent<Transform>().position += (new Vector3(0, 2, 0) * Time.deltaTime);
        //    //    if (this.transform.localPosition.y >= y)
        //    //        trigger2 = false;
        //    //}
        //}
    }

    public IEnumerator corcotinue()
    {
        Debug.Log(1);
        yield return new WaitForSeconds(10.0f);
        Debug.Log(2);
        yield return null;
        Debug.Log(3);

    }

    private void addspeed()
    {
        add = true;
    }
}
