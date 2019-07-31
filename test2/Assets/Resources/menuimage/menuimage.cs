using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class menuimage : MonoBehaviour
{
    // Start is called before the first frame update
    private Sprite[] img;
    private Image image;
    private int lenth;
    private int num;
    void Start()
    {
        img = Resources.LoadAll<Sprite>("menuimage");
        image = this.gameObject.GetComponent<Image>();
        num = 0;
        image.sprite = img[num];
        lenth = img.Length;
    }

    // Update is called once per frame
    void Update()
    {
        num++;
        if (num < lenth)
        {
            image.sprite = img[num];
        }
        else {
            num = 0;
            image.sprite = img[num];
        }
    }
}
