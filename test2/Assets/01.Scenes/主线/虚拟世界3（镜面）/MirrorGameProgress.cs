using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class MirrorGameProgress : MonoBehaviour
{
    public GameObject video;
    public GameObject character;
    public GameObject MainCamera;
    public GameObject picture1;
    public GameObject picture2;
    public GameObject picture3;
    public bool ifSuccess;
    public string state;

    public int index;

    void Start()
    {
        video.SetActive(false);
        ifSuccess = false;
        state = "state0";
        index = 0;
    }

    private void OnMouseDown()
    {
        index++;
    }

    void Update()
    {
        if(ifSuccess==true)
        {
            MainCamera.GetComponent<ProCamera2D>().enabled = false;
            MainCamera.transform.position = Vector3.Lerp(MainCamera.transform.position, new Vector3(0, 0, -10), Time.deltaTime * 3);
        }
        if (MainCamera.transform.position.x >= -0.1)
            video.SetActive(true);
        if (index == 3)
            index = 0;

        if(index==0)
        {
            picture1.SetActive(true);
            picture2.SetActive(false);
            picture3.SetActive(false);
            state = "state0";
        }
        if(index==1)
        {
            picture1.SetActive(false);
            picture2.SetActive(true);
            picture3.SetActive(false);
            state = "state1";
        }
        if(index==2)
        {
            picture1.SetActive(false);
            picture2.SetActive(false);
            picture3.SetActive(true);
            state = "state2";
            ifSuccess = true;
        }
    }
}
