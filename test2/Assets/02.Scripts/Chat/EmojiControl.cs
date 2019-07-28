using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmojiControl : MonoBehaviour
{
    public Transform player_transfrom=null;
    private float xoffset = -1.2f;
    private float yoffset = 5.8f;
    private Transform expression_transform=null;

    private void Start()
    {
        expression_transform = transform;
        if (player_transfrom != null && expression_transform != null)
        {
            expression_transform.position = player_transfrom.position + new Vector3(xoffset, yoffset, 0);
        }
    }

    private void Update()
    {
        if (player_transfrom != null && expression_transform!=null)
        {
            expression_transform.position = player_transfrom.position + new Vector3(xoffset, yoffset, 0);
        }
    }
}
