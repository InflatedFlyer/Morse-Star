using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBoxFollow : MonoBehaviour {

    public RectTransform messagebox;
    public float xoffset;
    public float yoffset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 player2DPoint = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log("player point at screen:" + player2DPoint);
        messagebox.anchoredPosition = player2DPoint + new Vector2(xoffset, yoffset);
	}
}
