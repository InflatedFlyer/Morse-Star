using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBarAnima : MonoBehaviour
{
    public int fullValue = 15;  //最大数值
    public int nowValue = 6;    //当前数值
    public bool isx = true; //是否启用这个轴的缩放
    public bool isy = false;
    public bool isz = false;
    public float Percent = 0.1f;    //单次插值从from到to的百分比
    public float Boundary = 0.005f;     //插值最小精度

    private Vector3 fullScale; //最大缩放
    private Vector3 aimScale;   //目标缩放
    private bool flag;  //是否执行插值动画

    private 


    // Start is called before the first frame update
    void Start()
    {
        if (isx)
            aimScale.x = fullScale.x * ((float)nowValue / fullValue);
        if (isy)
            aimScale.y = fullScale.y * ((float)nowValue / fullValue);
        if (isz)
            aimScale.z = fullScale.z * ((float)nowValue / fullValue);

        transform.localScale = aimScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)   //插值动画
        {
            transform.localScale = Vector3.Lerp(transform.localScale, aimScale, Percent);
            if (Mathf.Abs(transform.localScale.x - aimScale.x) + Mathf.Abs(transform.localScale.y - aimScale.y) + Mathf.Abs(transform.localScale.z - aimScale.z) < Boundary)
            {
                transform.localScale = aimScale;
                flag = false;
            }
        }
    }

    public void AddValue(int p)
    {
        nowValue += p;

        if(isx)
        aimScale.x = fullScale.x * ((float)nowValue / fullValue);
        if(isy)
        aimScale.y = fullScale.y * ((float)nowValue / fullValue);
        if(isz)
        aimScale.z = fullScale.z * ((float)nowValue / fullValue);

        flag = true;
    }

    public void AddValue(string p)
    {
        nowValue += int.Parse(p);

        if (isx)
            aimScale.x = fullScale.x * ((float)nowValue / fullValue);
        if (isy)
            aimScale.y = fullScale.y * ((float)nowValue / fullValue);
        if (isz)
            aimScale.z = fullScale.z * ((float)nowValue / fullValue);

        flag = true;
    }
}
