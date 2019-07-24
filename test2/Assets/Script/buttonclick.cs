using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonclick : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector] public bool flag;
    public GameObject image;
    public GameObject b1;
    public GameObject b2;
    void Start()
    {
        flag = false;
        //image = GameObject.Find("image");
        //b1= GameObject.Find("Button (1)");
        //b2 = GameObject.Find("Button (2)");
        image.SetActive(flag);
        b1.SetActive(flag);
        b2.SetActive(flag);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        flag = !flag;
        image.SetActive(flag);
        b1.SetActive(flag);
        b2.SetActive(flag);
        
    }
}
