using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dongtaihuanse : MonoBehaviour {
    public Material mat;
    Color c;
    float k = 0;
	void Start () {
        c = new Vector4(0, 0, 0, 1);
        mat = GetComponent<MeshRenderer>().material;
    }
	
	void Update () {        
        k += 0.008f;
        if(k<=0.9)
        c = new Vector4(k,k,k,1);
        mat.color = c;
	}
}
