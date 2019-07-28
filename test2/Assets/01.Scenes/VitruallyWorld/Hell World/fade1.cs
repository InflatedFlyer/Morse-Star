using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fade1 : MonoBehaviour {

    private SpriteRenderer Renderer;
    float alpha=255.0f;
    public bool end = false;
	void Start () {
        Renderer = GetComponent<SpriteRenderer>();
        Renderer.color = new Vector4(255.0f, 255.0f, 255.0f, 255.0f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (alpha >= 1.0f)
            alpha -= 1.0f;
        if (alpha == 0.0f)
            end = true;
        Renderer.color = new Vector4(255.0f, 255.0f, 255.0f, alpha/250);       
	}
}
