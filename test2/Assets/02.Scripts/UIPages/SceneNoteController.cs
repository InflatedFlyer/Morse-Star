using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneNoteController : BaseUICtrl
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text)
    {
        transform.GetChild(0).GetComponent<Text>().text = text;
    }
}
