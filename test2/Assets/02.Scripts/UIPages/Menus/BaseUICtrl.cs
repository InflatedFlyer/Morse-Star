using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUICtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void ShowInPos(Vector3 pos)
    {
        transform.position = pos;
        Show();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool isShow()
    {
        return gameObject.activeSelf;
    }
}
