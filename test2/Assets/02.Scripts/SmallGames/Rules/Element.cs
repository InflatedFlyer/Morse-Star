using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {

    public enum ElementType
    {
        Door,
        Wall,
        Spring,
        Spine
    }
    
    public ElementType type;
    public bool trigger;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
