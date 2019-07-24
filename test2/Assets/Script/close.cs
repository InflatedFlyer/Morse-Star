using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class close : MonoBehaviour
{
    // Start is called before the first frame update
    //private bool flag;
    public GameObject image;
    private buttonclick menu;
    public GameObject b1;
    public GameObject b2;
    void Start()
    {
        menu =GameObject.Find("menu").GetComponent<buttonclick>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        menu.flag = false;
        image.SetActive(menu.flag);
        b1.SetActive(menu.flag);
        b2.SetActive(menu.flag);
    }
}
