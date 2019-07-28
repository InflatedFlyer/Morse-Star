using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class GameChange : MonoBehaviour
{
    public string GameState;
    public GameObject MainCamera;
    public bool change1, change2, change3;
    public float TargetView;
    // Start is called before the first frame update
    void Start()
    {
        change1 = false;
        change2 = false;
        change3 = false;
        TargetView = Camera.main.orthographicSize;
        GameState = "Game1";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Game1Clear")
            change1 = true;
        if (other.name == "Game2Clear")
            change2 = true;
        if (other.name == "Game3Clear")
            change3 = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (Camera.main.orthographicSize <= TargetView)
            Camera.main.orthographicSize += 2;
        if (Camera.main.orthographicSize >= TargetView)
            Camera.main.orthographicSize -= 2;
        if (change1)

        {
            GameState = "Game2";
            this.transform.position = new Vector3(53, -16, -4);
            MainCamera.GetComponent<ProCamera2D>().enabled = false;
            MainCamera.GetComponent<camerashaking>().enabled = false;
            TargetView = 70;
            if (MainCamera.transform.localEulerAngles.z <= 120)
                MainCamera.transform.localEulerAngles += new Vector3(0, 0, 80) * Time.deltaTime;
            if(MainCamera.transform.localEulerAngles.z>=120)
            {
                TargetView = 10;
                MainCamera.GetComponent<ProCamera2D>().enabled = true;
                MainCamera.GetComponent<camerashaking>().enabled = true;
                change1 = false;
            }
        }

        if (change2)
        {
            GameState = "Game3";
            this.transform.position = new Vector3(5.78f, 54.17f, -4);
            MainCamera.GetComponent<ProCamera2D>().enabled = false;
            MainCamera.GetComponent<camerashaking>().enabled = false;
            TargetView = 70;
            if (MainCamera.transform.localEulerAngles.z <= 240)
                MainCamera.transform.localEulerAngles += new Vector3(0, 0, 80) * Time.deltaTime;
            if (MainCamera.transform.localEulerAngles.z >= 240)
            {
                TargetView = 10;
                MainCamera.GetComponent<ProCamera2D>().enabled = true;
                MainCamera.GetComponent<camerashaking>().enabled = true;
                change2 = false;
            }
        }
    }
}
