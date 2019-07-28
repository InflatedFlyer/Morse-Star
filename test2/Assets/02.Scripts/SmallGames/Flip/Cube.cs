using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {

    public Point pos;
    public float moveSpeed = 0.1f;

    public bool isMoving = false;
    public bool canMove = true;
    private Vector2 target;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(isMoving)
        {
            Vector2 current = transform.position;
            if(Vector2.Distance(current,target)<0.1f)
            {
                transform.position = target;
                isMoving = false;
            }
            else
            {
                Vector2 direct = target - current;
                transform.Translate(direct.normalized* moveSpeed);
            }
        }
	}

    public void MoveTo(Vector2 target)
    {
        isMoving = true;

        this.target = target;
    }
}
