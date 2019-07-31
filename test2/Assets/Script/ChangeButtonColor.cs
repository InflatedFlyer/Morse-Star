using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeButtonColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private Color oldC;

    public Color newC=new Color(1,1,1,1);

    // Start is called before the first frame update
    void Start()
    {
        oldC = this.GetComponentInChildren<Text>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //鼠标进入
        //Debug.Log("鼠标进入");

        this.GetComponentInChildren<Text>().color = newC;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //鼠标滑出
        //Debug.Log("鼠标滑出");

        this.GetComponentInChildren<Text>().color = oldC;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //鼠标按下

        this.GetComponentInChildren<Text>().color = new Color(0.7f, 0.7f, 0.7f, 0.8f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        //鼠标抬起

        this.GetComponentInChildren<Text>().color = oldC;
    }
}

