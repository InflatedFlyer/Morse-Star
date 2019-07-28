using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftright : MonoBehaviour
{
    public float speedTime,MoveSpeed,startTime;
    public bool LR;
    //public GameObject Object;
    private float temp,tempTotal;
    bool IsDisplay = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LR)
        {
            left_right();
        }
    }

    void left_right()
    {
        tempTotal += Time.deltaTime;
        if (tempTotal <= startTime)
        {
            return;
        }
        temp += Time.deltaTime;
        Debug.Log(temp);
        if (IsDisplay)
        {
            transform.Translate(Vector3.left * Time.deltaTime * MoveSpeed);
            if (temp >= speedTime)
            {
                IsDisplay = false;
                temp = 0;
            }
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * MoveSpeed);
            if (temp >= speedTime)
            {
                IsDisplay = true;
                temp = 0;
            }
        }
    }

  
}
