using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityScript;
using UnityEngine.UI;
using PathologicalGames;
using System;
using PixelCrushers.DialogueSystem;
public class WalkCharacter : MonoBehaviour
{
    #region 需要保存的状态
    public enum PersonState
    {
        Talking,
        Stopping,
        Walking,
        Running,
        LoseControl
    }
    public PersonState last_state = PersonState.Stopping;
    public PersonState state = PersonState.Stopping;
    #endregion

    public Vector2 target_pos;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public DragonBones.UnityArmatureComponent UnityArmature;

    public bool isControl
    {
        get
        {
            return state != PersonState.LoseControl && state != PersonState.Talking;
        }
    }

    public bool isMove
    {
        get
        {
            return state == PersonState.Walking||  state == PersonState.Running;
        }
    }

    private void Awake()
    {
        target_pos = transform.position;
    }
    private void Update()
    {
        if(state==PersonState.Stopping||state==PersonState.Talking)
        {
            target_pos = transform.position;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(target_pos.x - transform.position.x);
            Debug.Log(state);
        }
        
        if(Vector2.Distance(transform.position, target_pos) >= 0.1f)
        {
            Vector2 direct = (target_pos - new Vector2(transform.position.x, transform.position.y)).normalized;
            if (state != PersonState.Running)
            {
                Move(direct, walkSpeed);
                if (!UnityArmature.animation.isPlaying || UnityArmature.animation.lastAnimationName != "侧面走路")
                {
                    UnityArmature.animation.Play("侧面走路");
                    UnityArmature.animation.timeScale = 1.6f;
                }
            }
            else
            {
                Move(direct, runSpeed);
                if (!UnityArmature.animation.isPlaying || UnityArmature.animation.lastAnimationName != "跑步")
                {
                    UnityArmature.animation.Play("跑步");
                    UnityArmature.animation.timeScale = 1.6f;
                }
            }
        }
        else
        {
            transform.position = new Vector3(target_pos.x, target_pos.y, transform.position.z);
            if (isMove)
            {
                ChangeState(PersonState.Stopping);
            }
        }

        //if(Input.GetKey(KeyCode.LeftArrow))
        //{
        //    Walk(-1);
        //}
        //else if((Input.GetKey(KeyCode.RightArrow)))
        //{
        //    Walk(1);
        //}
        //else
        //{
        //    Stop();
        //}
    }

    public void Stop()
    {
        if (UnityArmature == null)
        {
            return;
        }

        if (state==PersonState.Stopping)
        {
            UnityArmature.animation.Play("暂停");
        }   
    }

    #region 走路
    public void Walk(float x)
    {
        Walk(new Vector2(x, transform.position.y));   
    }

    public void Walk(float x,float y)
    {
        Walk(new Vector2(x, y));
    }

    public void Walk(Vector2 target)
    {

        if (Mathf.Abs(target.x - transform.position.x) > 1&&isControl)
        {

            target_pos = target;

            state = PersonState.Walking;
        }
    }

    public void Walk(Vector3 target)
    {
        Run(new Vector2(target.x,target.y));
    }
    #endregion

    #region 跑步
    public void Run(float x)
    {
        Run(new Vector2(x, transform.position.y));
    }

    public void Run(float x, float y)
    {
        Run(new Vector2(x, y));
    }

    public void Run(Vector2 target)
    {
        if (Mathf.Abs(target.x - transform.position.x) > 1 && isControl)
        {
            target_pos = target;

            state = PersonState.Running;
        }
    }

    public void Run(Vector3 target)
    {
        Run(new Vector2(target.x, target.y));
    }
    #endregion

    public IEnumerator WalkCoroutine()
    {
        state = PersonState.Walking;
        Vector2 direct = (target_pos - new Vector2(transform.position.x,transform.position.y)).normalized;
        while(Vector2.Distance(transform.position,target_pos)!=0)
        {
            Move(direct, walkSpeed);
            if (!UnityArmature.animation.isPlaying || UnityArmature.animation.lastAnimationName != "侧面走路")
            {
                UnityArmature.animation.Play("侧面走路");
            }

            if (Vector2.Distance(transform.position, target_pos)<0.1f)
            {
                transform.position = new Vector3(target_pos.x,target_pos.y, transform.position.z);
                state = PersonState.Stopping;
                break;
            }
            yield return null;
        }
    }
    #region 原先走路部分
    //public void Walk(int direct)
    //{
    //    if (state == PersonState.Talking||state==PersonState.LoseControl)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        ChangeState(PersonState.Walking);
    //        Move(direct, walkSpeed, 1.50f);
    //        if (!UnityArmature.animation.isPlaying || UnityArmature.animation.lastAnimationName != "侧面走路")
    //        {
    //            UnityArmature.animation.Play("侧面走路");
    //        }
    //    }
    //}

    //public void Run(int direct)
    //{
    //    if (state == PersonState.Talking)
    //    {
    //        return;
    //    }
    //    else
    //    {
    //        ChangeState(PersonState.Running);
    //        Move(direct, runSpeed, 1.5f);
    //        if (!UnityArmature.animation.isPlaying||UnityArmature.animation.lastAnimationName!="奔跑")
    //        {
    //            UnityArmature.animation.Play("奔跑");
    //        }
    //    }
    //}

    //private void Move(int direct, float speed, float timeScale)
    //{
    //    Vector3 currentPosition = transform.position;
    //    if (direct != -1 && direct != 1)
    //    {
    //        Debug.Log("方向错误.");
    //        return;
    //    }
    //    float move_dist = direct * speed * Time.deltaTime;

    //    GetComponent<Transform>().position = new Vector3(currentPosition.x + move_dist, currentPosition.y, currentPosition.z);
    //    Vector3 scale = GetComponent<Transform>().localScale;
    //    GetComponent<Transform>().localScale = new Vector3(direct * Math.Abs(scale.x),scale.y,1);
    //    UnityArmature.animation.timeScale = timeScale;
    //}
    #endregion

    private void Move(Vector2 direct,float speed,float timeScale=1)
    {
        Vector3 currentPosition = transform.position;
        Vector2 move_dist = direct * speed * Time.deltaTime;

        transform.Translate(move_dist);
        Vector3 scale = GetComponent<Transform>().localScale;
        GetComponent<Transform>().localScale = new Vector3(-Mathf.Sign(direct.x) * Math.Abs(scale.x), scale.y, 1);
        UnityArmature.animation.timeScale = timeScale;
        
    }

    public List<Sprite> emojis;
    private Transform expression=null;
    private Transform dialog=null;
    private SpawnPool ui_pool;
    private Transform canvas;

    private string person_name = "黎茗";

    private void Start()
    {
        //ui_pool = GameObject.FindWithTag("SpawnPool").GetComponent<SpawnPool>();
        //canvas = GameObject.FindWithTag("canvas").transform;
    }

    public void Talk()
    {
        ChangeState(PersonState.Talking);
        UnityArmature.animation.Play("暂停");    
    }

    public void LoseControl()
    {
        ChangeState(PersonState.LoseControl);
    }
    

    public void GetControl()
    {
        ChangeState(PersonState.Stopping);
    }

    public void OnConversationStart(Transform actor)
    {
        if(actor.gameObject.name=="T2319")
        {
            return;
        }
        Debug.Log("start conversation with "+actor.gameObject.name);
        Talk();
    }

    public void OnConversationEnd(Transform actor)
    {
        if (actor.gameObject.name == "T2319")
        {
            return;
        }
        Debug.Log("end conversation with " + actor.gameObject.name);
        ChangeState(last_state);
    }

    public void ChangeState(PersonState new_state)
    {
        //Debug.Log(string.Format("Change state {0} to {1}",state,new_state));
        last_state = state;
        state = new_state;
    }

    public void ReSumeLastState()
    {
        ChangeState(last_state);
    }
}
