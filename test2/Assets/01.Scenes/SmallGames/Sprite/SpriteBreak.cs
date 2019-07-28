using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
public class SpriteBreak : MonoBehaviour
{
    public string state;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;
    GameObject SubSprites;
    GameObject light1;
    GameObject light2;
    GameObject light3;
    int wall=0;
    GameObject MainCamera;

    void CameraCommon()
    {
        MainCamera.GetComponent<ProCamera2D>().enabled = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.name == "切割1")
        {
            GameObject.Find("SubSprite1").GetComponent<SpriteFollow>().enabled = false;
            GameObject.Find("SubSprite2").GetComponent<SpriteFollow>().enabled = false;
            state = "1";            
        }

        if (collision.name == "切割2")
        {
            GameObject.Find("SubSprite3").GetComponent<SpriteFollow>().enabled = false;
            GameObject.Find("SubSprite4").GetComponent<SpriteFollow>().enabled = false;
            GameObject.Find("SubSprite5").GetComponent<SpriteFollow>().enabled = false;
            state = "2";
        }

        if (collision.name == "切割3")
        {
            GameObject.Find("SubSprite7").GetComponent<SpriteFollow>().enabled = false;
            GameObject.Find("SubSprite6").GetComponent<SpriteFollow>().enabled = false;
            GameObject.Find("SubSprite8").GetComponent<SpriteFollow>().enabled = false;
            state = "3";
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        SubSprites = GameObject.Find("SubSprites");
        state = "0";
        light1 = GameObject.Find("light1");
        light2 = GameObject.Find("light2");
        light3 = GameObject.Find("light3");
        MainCamera = GameObject.Find("Main Camera");
    }

    void    ColorCommon()
    {
        this.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name=="wall4"||collision.gameObject.name=="wall3")
        {
            this.transform.GetComponent<SpriteRenderer>().color=new Color(1,1,1,0.2f) ;
            Invoke("ColorCommon", 0.5f);
        }
        if(collision.gameObject.name=="wall1")
        {
            wall1.gameObject.SetActive(false);
        }
        if (collision.gameObject.name == "wall4")
        {
            wall++;            
            MainCamera.GetComponent<ProCamera2D>().enabled = false;
            Invoke("CameraCommon", 0.5f);
        }
        if (collision.gameObject.name == "wall2")
        {
            MainCamera.GetComponent<ProCamera2D>().enabled = false;
            wall++;
            Invoke("CameraCommon", 0.5f);
        }
        if (collision.gameObject.name == "wall3")
        {
            MainCamera.GetComponent<ProCamera2D>().enabled = false;
            wall++;
            Invoke("CameraCommon", 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (wall == 1)
            wall2.SetActive(false);
        if (wall == 4)
            wall3.SetActive(false);
        if (wall == 14)
            wall4.SetActive(false);
        if (state == "1" && light1.transform.position.y > -500)
            light1.transform.position += new Vector3(0, -180, 0) * Time.deltaTime;
        if (state == "2" && light2.transform.position.y > -500)
            light2.transform.position += new Vector3(0, -180, 0) * Time.deltaTime;
        if (state == "3" && light3.transform.position.y > -500)
            light3.transform.position += new Vector3(0, -180, 0) * Time.deltaTime;

    }
}