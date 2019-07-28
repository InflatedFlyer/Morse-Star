using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogText : MonoBehaviour
{
    private Text t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string content)
    {
        if (t == null)
        {
            t = GetComponent<Text>();
        }
        if (t != null)
        {
            t.text = content;
        }
    }
}
