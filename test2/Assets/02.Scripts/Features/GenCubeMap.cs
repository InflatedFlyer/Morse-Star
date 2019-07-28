using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenCubeMap : MonoBehaviour {
    public Cubemap cubemap;

	// Use this for initialization
	void Start () {
        CreateCubemap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateCubemap()
    {
        GameObject go = new GameObject("CubemapCamera");
        go.AddComponent<Camera>();
        go.transform.position = transform.position;

        go.GetComponent<Camera>().RenderToCubemap(cubemap);
        Destroy(go);
    }
}
