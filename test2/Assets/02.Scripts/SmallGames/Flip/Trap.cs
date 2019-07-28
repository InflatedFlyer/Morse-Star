using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    private bool isTrigger = false;
    private FlipGame game;
    public GameObject trap;
    public Point trap_pos;
	// Use this for initialization
	void Start () {
        game=GameObject.Find("FlipGame").GetComponent<FlipGame>();
        game.map[trap_pos.x, trap_pos.y] = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(isTrigger);
        if(isTrigger)
        {
            game.map[trap_pos.x, trap_pos.y] = false;
            trap.SetActive(true);
        }
        else
        {
            game.map[trap_pos.x, trap_pos.y] = true;
            trap.SetActive(false);
        }
        isTrigger = !isTrigger;
    }
}
