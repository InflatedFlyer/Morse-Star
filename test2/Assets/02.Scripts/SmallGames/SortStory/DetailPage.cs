using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailPage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetContent(Sprite image,string description)
    {
        gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<ImageItemCtrl>().SetImage(image);

        transform.GetChild(1).gameObject.GetComponentInChildren<Text>().text = description;
    }
}
