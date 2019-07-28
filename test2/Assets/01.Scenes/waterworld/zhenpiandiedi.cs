using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zhenpiandiedi : MonoBehaviour {
    private SpriteRenderer diediRenderer;
    private SpriteRenderer dimianRnderer;
	void Start () {
		
	}
    void Awake()
    {
        dimianRnderer = GameObject.Find("basebackground").GetComponent<SpriteRenderer>();
        diediRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update () {
        diediRenderer.material.color = diediRenderer.material.color * dimianRnderer.material.color;
	}
}
