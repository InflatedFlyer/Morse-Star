using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DragonBones;
public class ProcessBar : MonoBehaviour {

    private float time_scale=0;
    private UnityArmatureComponent unity_armature;
    private string animation_name;
    // Use this for initialization
    void Awake () {
        unity_armature = GetComponent<UnityArmatureComponent>();
    }
	
	public void SetProcess(float current_process)
    {
        if(current_process<0||current_process>1)
        {
            return;
        }
        var anim_state = unity_armature.animation.GetState("point");
        if (anim_state != null)
        {
            anim_state.currentTime = current_process * anim_state.totalTime;
        }
        else
        {
            Debug.Log("anim state is null");
        }
    }

    public void SetAnimationLength(float time)
    {
        if(time<0)
        {
            return;
        }
        var anim_state=unity_armature.animation.GetState("point");
        if (anim_state != null)
        {
            time_scale= anim_state.totalTime / time;
            anim_state.timeScale = time_scale;
        }
        else
        {
            Debug.Log("anim state is null");
        }
    }

    public void PlayProcessBar(int playTimes=0)
    {
        if(playTimes<0)
        {
            playTimes = 0;
        }
        unity_armature.animation.Play("point", playTimes);
    }

    public void StopPlay()
    {
        var anim_state=unity_armature.animation.GetState("point");
        if (anim_state != null)
        {
            anim_state.timeScale = 0;
        }
        else
        {
            Debug.Log("anim state is null");
        }
    }

    public void ReStartPlay()
    {
        var anim_state = unity_armature.animation.GetState("point");
        if (anim_state != null)
        {
            anim_state.currentTime = 0;
            anim_state.timeScale = time_scale;
        }
        else
        {
            Debug.Log("anim state is null");
        }
    }

    public float GetCurrentProcess()
    {
        var anim_state = unity_armature.animation.GetState("point");
        return anim_state.currentTime/anim_state.totalTime;
    }

    public void ShowProcessBar()
    {
        ReStartPlay();
        unity_armature.gameObject.SetActive(true);
    }

    public void HideProcessBar()
    {
        unity_armature.gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        unity_armature.transform.position = position;
    }

    public void InversePlay()
    {
        var anim_state = unity_armature.animation.GetState("point");
        if (anim_state != null)
        {
            anim_state.timeScale = -anim_state.timeScale;
        }
        else
        {
            Debug.Log("anim state is null");
        }
    }
}
