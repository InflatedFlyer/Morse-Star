using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageItemCtrl : MonoBehaviour
{
    public string description;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImage(Sprite image)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = image;
    }

    public Sprite GetImage()
    {
        return transform.GetChild(0).GetComponent<Image>().sprite;
    }
}
