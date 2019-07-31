using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeResponsePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePanel(string n)
    {
        this.GetComponent<PixelCrushers.DialogueSystem.StandardDialogueUI>().conversationUIElements.defaultMenuPanel =
            this.GetComponent<PixelCrushers.DialogueSystem.StandardDialogueUI>().conversationUIElements.menuPanels[int.Parse(n)];

        this.GetComponent<PixelCrushers.DialogueSystem.StandardDialogueUI>().conversationUIElements.InitializeMenu();   //插入的自定义更改
    }
}
