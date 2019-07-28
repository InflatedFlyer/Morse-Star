using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSceneChange : MonoBehaviour
{
    public bool ifchangetostate1;
    public bool ifchangetostate2;
    public GameObject wall;
    public GameObject Hud;
    public GameObject LightOff;
    public GameObject ColorOff;
    public GameObject cube1;
    public GameObject cube2;
    public GameObject cube3;
    public GameObject cube4;
    public GameObject cube5;
    public GameObject cube6;
    public GameObject cube7;
    public GameObject cube8;
    public Color color1;
    public float changespeed;
    // Start is called before the first frame update
    void Start()
    {
        ifchangetostate1 = false;
        ifchangetostate2 = false;
        color1 = LightOff.GetComponent<SpriteRenderer>().material.color;
        wall.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(ifchangetostate1)
        {
            if(color1.a>0)
            {
                color1.a -= Time.deltaTime * changespeed;
                LightOff.GetComponent<SpriteRenderer>().material.color = color1;
                Hud.GetComponent<SpriteRenderer>().material.color = color1;
            }
            else
            {
                color1.a = 1;
                ifchangetostate1 = false;
            }
        }

        if (ifchangetostate2)
        {
            if (color1.a > 0)
            {
                color1.a -= Time.deltaTime * changespeed;
                ColorOff.GetComponent<SpriteRenderer>().material.color = color1;
                cube1.GetComponent<SpriteRenderer>().material.color = color1;
                cube2.GetComponent<SpriteRenderer>().material.color = color1;
                cube3.GetComponent<SpriteRenderer>().material.color = color1;
                cube4.GetComponent<SpriteRenderer>().material.color = color1;
                cube5.GetComponent<SpriteRenderer>().material.color = color1;
                cube6.GetComponent<SpriteRenderer>().material.color = color1;
                cube7.GetComponent<SpriteRenderer>().material.color = color1;
                cube8.GetComponent<SpriteRenderer>().material.color = color1;
            }
            else
            {
                wall.SetActive(true);
                color1.a = 1;
                ifchangetostate2 = false;
            }
        }
    }
}
