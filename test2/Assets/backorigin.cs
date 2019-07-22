using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backorigin : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] blocks;
    private Vector3[] pos;
    private int i;
    void Start()
    {
        pos = new Vector3[blocks.Length];
        for (i = 0; i < blocks.Length; i++)
        {
            pos[i] = blocks[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        for (i = 0; i < blocks.Length; i++)
        {
            blocks[i].transform.position = pos[i];
        }
    }
}
