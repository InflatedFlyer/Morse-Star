using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCross : MonoBehaviour
{
    public GameObject one;
    public GameObject two;
    public Color color1;
    public Color color2;
    IEnumerator LightChangeOne()
    {
        color1=one.transform.GetComponent<SpriteRenderer>().material.color;
        while(true)
        {
            if (color1.a == 0)
            {
                color1.a = 1;
                one.transform.GetComponent<SpriteRenderer>().material.color = color1;
            }
            else
            {
                color1.a = 0;
                one.transform.GetComponent<SpriteRenderer>().material.color = color1;
            }
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }

    IEnumerator LightChangeTwo()
    {
        color2 = two.transform.GetComponent<SpriteRenderer>().material.color;
        while (true)
        {
            if (color2.a == 0)
            {
                color2.a = 1;
                two.transform.GetComponent<SpriteRenderer>().material.color = color2;
            }
            else 
            {
                color2.a = 0;
                two.transform.GetComponent<SpriteRenderer>().material.color = color2;
            }
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        color1=one.transform.GetComponent<SpriteRenderer>().material.color;
        color1.a = 0;
        one.transform.GetComponent<SpriteRenderer>().material.color = color1;
        color2 = one.transform.GetComponent<SpriteRenderer>().material.color;
        color2.a = 1;
        two.transform.GetComponent<SpriteRenderer>().material.color = color2;
        StartCoroutine(LightChangeOne());
        StartCoroutine(LightChangeTwo());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
