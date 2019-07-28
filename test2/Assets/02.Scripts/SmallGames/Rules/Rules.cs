using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rules : MonoBehaviour {

    private List<Element> _elements=null;
    public List<Element> elements
    {
        get
        {
            if(_elements==null)
            {
                _elements = new List<Element>();
                _elements.AddRange(GetComponentsInChildren<Element>());
            }
            return _elements;
        }
    }
    
    private Electron _user;
    public Electron user
    {
        get
        {
            if(_user==null)
            {
                _user = GameObject.Find("User").GetComponent<Electron>();
            }
            return _user;
        }
    }
    // Use this for initialization
    void Start () {
        TestEffect();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void ExchangeType(Element e1,Element e2)
    {
        bool e1_tag = e1.trigger, e2_tag = e2.trigger;
        Element.ElementType e1_type = e1.type, e2_type = e2.type;

        List<Element> all_e1 = elements.FindAll(item => item.trigger == e1_tag && item.type == e1_type);
        List<Element> all_e2 = elements.FindAll(item => item.trigger == e2_tag && item.type == e2_type);

        foreach(Element e in all_e1)
        {
            e.type = e2_type;
            e.trigger = e2_tag;
        }
        foreach(Element e in all_e2)
        {
            e.type = e1_type;
            e.trigger = e1_tag;
        }
    }

    private void PrintElements()
    {
        for(int i=0;i<elements.Count;i++)
        {
            Debug.Log(i + ": type=" + elements[i].type + " trigger=" + elements[i].trigger);
        }

    }
    
    private void TestExchange()
    {
        PrintElements();
        ExchangeType(elements[0], elements[3]);
        PrintElements();
    }

    private void TestEffect()
    {
        user.trigger = true;
        for(int i=0;i<elements.Count;i++)
        {
            user.GetEffect(elements[i]);
        }

        user.trigger = false;
        for(int i=0;i<elements.Count;i++)
        {
            user.GetEffect(elements[i]);
        }
    }
}
