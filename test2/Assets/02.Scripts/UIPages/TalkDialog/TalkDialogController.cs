using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkDialogController : BaseUICtrl
{
    private Expressinon expression;
    private DialogText content;
    // Start is called before the first frame update
    void Awake()
    {
        expression = transform.GetComponentInChildren<Expressinon>();
        
        content = transform.GetComponentInChildren<DialogText>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Talk(int exp_idx,string text)
    {
        if (expression != null)
        {
            expression.SetExpression(exp_idx);

        }
        if (content != null)
        {
            content.SetText(text);
        }
    }
}
