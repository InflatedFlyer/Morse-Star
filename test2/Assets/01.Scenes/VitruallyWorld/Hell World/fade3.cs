using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade3 : MonoBehaviour
{

    private SpriteRenderer Renderer;
    private GameObject layer2;
    float alpha = 255.0f;
    public bool start = false;
    public bool end = false;
    void Start()
    {
        start = false;
        end = false;
        layer2 = GameObject.FindGameObjectWithTag("treelayer2");
        Renderer = GetComponent<SpriteRenderer>();
        Renderer.color = new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        start = layer2.GetComponent<fade2>().end;
        if (start)
        {
            if (alpha >= 1.0f)
                alpha -= 1.0f;
            Renderer.color = new Vector4(255.0f, 255.0f, 255.0f, alpha / 250);
            if (alpha == 0.0f)
            {
                start = false;
                end = true;
            }
        }
    }
}
