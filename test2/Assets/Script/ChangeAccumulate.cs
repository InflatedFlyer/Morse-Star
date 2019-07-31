using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAccumulate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Change()
    {
        bool t = this.GetComponent<PixelCrushers.DialogueSystem.StandardUISubtitlePanel>().accumulateText;
        this.GetComponent<PixelCrushers.DialogueSystem.StandardUISubtitlePanel>().accumulateText = !t;
    }
}
