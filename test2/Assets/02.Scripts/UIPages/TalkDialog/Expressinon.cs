using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Expressinon : MonoBehaviour
{
    public List<Sprite> images;
    private int current_expression=-1;

    private Image image_ctrl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetExpression(int idx)
    {
        if (image_ctrl == null)
        {
            image_ctrl = GetComponent<Image>();
        }
        if (image_ctrl != null)
        {
            if (idx >= 0 && idx < images.Count && idx != current_expression)
            {
                current_expression = idx;
                image_ctrl.sprite = images[idx];
            }
        }
    }
}
