using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GrayWorld : MonoBehaviour {

    [HideInInspector]
    public int angle=0;

    private int start_angle=60;
    private int end_angle=120;

    //角度旋转速度
    private float period;
    private int increment;
    private ProcessBar process_control;

    //场景状态
    public enum WaterSceneState
    {
        None,
        OnFish,
        StartChangeAngle,
        StopChangeAngle,
    }

    public WaterSceneState scene_state;

    // Use this for initialization
    void Start () {
        //角度旋转速度
        angle = start_angle+2;
        increment = 1;
        period = (float)(end_angle-start_angle) / (increment * 50);

        //进度条控制
        process_control = GameObject.Find("Armature").GetComponent<ProcessBar>();
        process_control.PlayProcessBar();
        process_control.SetAnimationLength(period);
        process_control.StopPlay();
        process_control.HideProcessBar();

        EventManager.getInstance().StartListening("StayFish", StayFish);
        EventManager.getInstance().StartListening("LeaveFish", LeaveFish);
        scene_state = WaterSceneState.None;
    }

    private void FixedUpdate()
    {
        if (scene_state == WaterSceneState.OnFish)
        {
            ChangeAngle();
            if (angle % 10 == 0)
            {
                process_control.SetProcess((((float)angle -start_angle) /(end_angle-start_angle)));
            }
        }
    }

    #region 场景函数
    private void ChangeAngle()
    {
        angle += increment;
        if (angle > end_angle-2||angle<start_angle+2)
        {
            increment = -increment;
            process_control.InversePlay();
        }
    }

    //进度条暂停
    public void StopAll()
    {
        process_control.StopPlay();
    }

    //进度条重新开始
    public void ReStart()
    {
        angle = start_angle+2;
        increment = 1;
        process_control.ReStartPlay();
    }

    public void StayFish()
    {
        scene_state = WaterSceneState.OnFish;
        process_control.SetPosition(GameObject.FindWithTag("Character").transform.position);
        process_control.ShowProcessBar();
        ReStart();
        
    }

    public void LeaveFish()
    {
        process_control.HideProcessBar();

    }
    #endregion
}
