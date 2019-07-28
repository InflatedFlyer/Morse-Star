using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Trail : MonoBehaviour {

    public int trail;//开始10个单位长度，中切4次，3,2,2,3
	void Start () {
        trail = 10;
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "trail3")
            trail -= 3;
        if (other.tag == "trail2")
            trail -= 2;
        if (other.tag == "trail1")
            trail -= 1;
    }
    void Update () {
		
	}
}
