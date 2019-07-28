using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMove : MonoBehaviour {
    public static float left_bound = -3f;
    public static float right_bound = 3f;

    private static float start_time;
    private static float current_time;
    public static float move_time = 0.1f;
    public static float fall_speed = 5f;

    private int direct = -1;
    
    //方块信息
    public float width
    {
        get
        {
            return GetComponent<MeshFilter>().mesh.bounds.size.x * transform.localScale.x;
        }
    }
    public float height
    {
        get
        {
            return GetComponent<MeshFilter>().mesh.bounds.size.y * transform.localScale.y;
        }
    }
    public float left
    {
        get
        {
            return transform.position.x - width / 2;
        }
    }
    public float right
    {
        get
        {
            return transform.position.x + width / 2;
        }
    }

    public static float unit_count = 5;
    private static float step =0.4f;

    //方块状态 0不动， 1左右移动 ，2下落
    public int state = 0;
    
    private void Start()
    {
        start_time = Time.time;
        current_time = start_time;
    }

    // Update is called once per frame
    void Update () {
        if (state==1)
        {
            if (left <= left_bound)
            {
                direct = 1;
            }
            else if (right >= right_bound)
            {
                direct = -1;
            }
            float t = Time.time;
            if (t - current_time > move_time)
            {
                current_time = t;
                transform.Translate(Vector3.right * direct * step);
            }
        }
    }

    public void StartMove()
    {
        state = 1;
    }
    

    public void StopMove()
    {
        state = 0;
    }

    public void ChangeShape(float left,float right)
    {
        Vector3 scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * (right - left) / width, scale.y, scale.z);

        transform.Translate(left - this.left, 0, 0);
    }
    
}
