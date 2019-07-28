using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MsgBoxControl : MonoBehaviour
{
    public Transform player_transfrom=null;
    private float xoffset=0;
    private float yoffset=5.5f;
    private RectTransform dialog_transform=null;

    private void Start()
    {
        dialog_transform =(RectTransform)this.gameObject.transform;
        if (player_transfrom != null && dialog_transform != null)
        {
            dialog_transform.position = player_transfrom.position + new Vector3(xoffset, yoffset, 0);
        }
    }

    private void Update()
    {
        if (player_transfrom != null && dialog_transform != null)
        {
            dialog_transform.position = player_transfrom.position + new Vector3(xoffset, yoffset, 0);
        }
    }

    // 更改显示文本
    public void ShowText(string content,string name="黎茗")
    {
        Text text_name, text_content;
        text_name = transform.GetChild(0).GetComponent<Text>();
        text_content=transform.GetChild(1).GetComponent<Text>();
        text_name.text = name;
        text_content.text = content;
    }
}
