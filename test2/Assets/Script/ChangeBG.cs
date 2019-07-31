using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeBG : MonoBehaviour
{
    public GameObject Root; //图集根节点
    public float Percent = 0.05f;    //单次插值从from到to的百分比
    public float Boundary = 0.005f;     //插值最小精度
    public float fadeSpeed = 2f;

    private GameObject obj;
    private bool flag; //插值激活flag

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            obj.GetComponent<Image>().color = Color.Lerp(obj.GetComponent<Image>().color, Color.white, fadeSpeed * Time.deltaTime);
            if (Mathf.Abs(obj.GetComponent<Image>().color.a - 1) < Boundary)
            {
                obj.GetComponent<Image>().color = Color.white;
                flag = false;
            }
        }
    }

    public void ChangeBGTo(string n)
    {
        obj = Root.transform.Find(n).gameObject;
        obj.GetComponent<Transform>().SetAsLastSibling();   //移动到最上
        obj.SetActive(true);    //启用
        obj.GetComponent<Image>().color = new Color(1, 1, 1, 0);    //设为透明

        flag = true;
    }

    public void ChangeBGTo(int n)
    {
        obj = Root.transform.Find(n.ToString()).gameObject;
        obj.GetComponent<Transform>().SetAsLastSibling();   //移动到最上
        obj.SetActive(true);    //启用
        obj.GetComponent<Image>().color = new Color(1, 1, 1, 0);    //设为透明

        flag = true;
    }
}

