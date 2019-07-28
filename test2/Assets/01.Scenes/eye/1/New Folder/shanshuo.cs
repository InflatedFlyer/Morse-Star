using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shanshuo : MonoBehaviour
{
    public GameObject Object;
    public bool open;
    private float temp,temp1;
    public float gapTime,lastTime; //闪烁的间隔时间，在Unity中修改
    bool IsDisplay = true;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("has been in twinkle update");
        if (open)
        {
            Effect();
        }
    }
    public void Effect()
    { 

    temp += Time.deltaTime;
    temp1 += Time.deltaTime;
        if (temp1 <= lastTime)
        {
            if (temp >= gapTime)
            {
                if (IsDisplay)
                {
                    Object.GetComponent<Renderer>().enabled = false;
                    IsDisplay = false;
                    temp = 0;
                }
                else
                {
                    Object.GetComponent<Renderer>().enabled = true;
                    IsDisplay = true;
                    temp = 0;
                }
            }
        }else
        {
            Object.GetComponent<Renderer>().enabled = true;
        }
    }  
}


