using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemNoteController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetContent(Sprite image,string content)
    {
        Image image_ctrl= transform.GetChild(0).GetComponent<Image>();
        if(image_ctrl != null)
        {
            image_ctrl.sprite = image;
        }

        Text text_ctrl = GetComponentInChildren<Text>();
        if (text_ctrl != null)
        {
            text_ctrl.text = content;
        }
    }
}
