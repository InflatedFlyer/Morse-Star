using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuiShi4 : MonoBehaviour
{

    private bool play = false;
    GameObject suishi;
    void Start()
    {
        suishi = GameObject.Find("SuiShi4");
        suishi.GetComponent<ParticleSystem>().Stop();
    }

    void Update()
    {
        if (play)
        {
            suishi.GetComponent<ParticleSystem>().Play();
            play = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        play = true;
    }
}
