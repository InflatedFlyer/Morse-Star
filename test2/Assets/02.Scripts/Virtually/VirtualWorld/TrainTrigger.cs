using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Character")
        {
            transform.GetChild(0).GetComponent<RotAB>().PC = true;
        }
    }


}
