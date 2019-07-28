using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAlphaFlash : MonoBehaviour
{
    public float period;
    public float period2;
    public float left;
    public float right;
    public float startalpha;
    public Color color1;
    int i = 0;

    IEnumerator LightChange()
    {
        color1 = transform.GetComponent<SpriteRenderer>().material.color;
        while (true)
        {
            if (color1.a == left)
            {
                if (i == 0)
                {
                    i = 1;
                   // yield return new WaitForSecondsRealtime(period2);
                }
                else
                    i = 0;
                color1.a = right;
                transform.GetComponent<SpriteRenderer>().material.color = color1;
        }
            else
            {
                if (i == 0)
                {
                    i = 1;
                    yield return new WaitForSecondsRealtime(period2);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(period2);
                    i = 0;
                }
                color1.a = left;
                transform.GetComponent<SpriteRenderer>().material.color = color1;
            }
            yield return new WaitForSecondsRealtime(period);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        color1 = transform.GetComponent<SpriteRenderer>().material.color;
        color1.a = startalpha;
        transform.GetComponent<SpriteRenderer>().material.color = color1;
        StartCoroutine(LightChange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
