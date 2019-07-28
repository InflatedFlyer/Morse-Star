using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDisappearCtr : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Disappear()
    {
        RotAB[] father = GetComponentsInChildren<RotAB>();
        foreach (var child in father)
        {
            child.GetComponent<RotAB>().SBegin = new Vector3 (0.5f,0.5f,0.5f);
            child.GetComponent<RotAB>().STarget = new Vector3(-0.1f, -0.1f, -0.1f);
        }

        Invoke("PDisappear",2.3f);
    }

    private void PDisappear()
    {
        this.gameObject.SetActive(false);
    }

    public void Begin()
    {
        RotAB[] father = GetComponentsInChildren<RotAB>();

        foreach (var child in father)
        {
            child.GetComponent<RotAB>().SC = true;
            this.GetComponent<RotAB>().AC = true;
        }
        Invoke("Disappear", 3f);
    }
}
