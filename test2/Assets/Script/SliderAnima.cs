using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnima : MonoBehaviour
{
    public int fullValue = 15;  //最大数值
    public int nowValue = 6;    //当前数值
    public float Percent = 0.1f;    //单次插值从from到to的百分比
    public float Multiple = 0.025f;   //颜色插值倍数
    public float Boundary = 0.005f;     //插值最小精度

    private float aimValue;   //目标缩放
    private bool flag;  //是否执行插值动画
    private bool isplus; //加减正负号
    private Transform pic; //子物体中上覆盖图片
    private float mul;



    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Slider>().value = ((float)nowValue / fullValue);

        Transform obj = transform.Find("Fill");
        pic = obj.Find("Image");
        mul = Multiple;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)   //插值动画
        {
            this.GetComponent<Slider>().value = Mathf.Lerp(this.GetComponent<Slider>().value, aimValue, Percent);
            if (isplus)
                pic.GetComponent<Image>().color = Color.Lerp(new Color(0.2f, 1, 0.2f, 1), Color.white, mul = mul * (1 + mul));
            else
                pic.GetComponent<Image>().color = Color.Lerp(new Color(1, 0.2f, 0.2f, 1), Color.white, mul = mul * (1 + mul));

            if (Mathf.Abs(this.GetComponent<Slider>().value-aimValue) < Boundary)
            {
                this.GetComponent<Slider>().value = aimValue;
                if (mul >= 1)
                {
                    pic.GetComponent<Image>().color = Color.white;
                    flag = false;
                    mul = Multiple;
                }
            }
        }
    }

    public void AddValue(int p)
    {
        nowValue += p;
        aimValue = 1 * ((float)nowValue / fullValue);   //数值转换为缩放
        flag = true;
        if (p > 0)
            isplus = true;
        else
            isplus = false;
    }

    public void AddValue(string p)
    {
        nowValue += int.Parse(p);
        aimValue = 1 * ((float)nowValue / fullValue);   //数值转换为缩放
        flag = true;
        if (int.Parse(p) > 0)
            isplus = true;
        else
            isplus = false;
    }
}
