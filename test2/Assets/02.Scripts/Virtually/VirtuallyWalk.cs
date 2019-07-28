using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DragonBones;

public class VirtuallyWalk : MonoBehaviour
{
    private enum PlayState
    {
        Stay,
        Jump,
        FishJump,
        OnGround
    }

    // 人物调参属性
    public float gravity = 15f;
    public float runSpeed = 8;//奔跑速度
    public float acceleration = 30;//奔跑加速度
    public float jumpHeight = 10;//跳跃高度
    public float jump_force_factor = 1.5f;//改变鱼群跳跃的力
    private int jumpsAllowed = 1;//最大跳跃次数
    private Vector2 wind=Vector2.zero;

    //人物各项动态属性
    private float currentSpeed;//奔跑速度
    private float jumpSpeed;//跳跃高度
    private int totalJumps;//当前跳跃次数
    private PlayState player_state;//人物状态
    
    private UnityArmatureComponent unityArmature;
    private CharacterController _characterController;

    //骨骼动画各个动作的名字
    private List<string> animation_names;
    private Dictionary<string, int> animation_name2index;

    //时间控制相关
    public bool EffectedByTime=true; //人物是否受时间减缓影响
    private float deltaTime     //两帧间隔的时间
    {
        get
        {
            return EffectedByTime ? Time.deltaTime : Time.unscaledDeltaTime;
        }
    }

    //人物的旋转矩阵
    private Matrix4x4 rotation_mat
    {
        get
        {
            return Matrix4x4.Rotate(transform.localRotation);
        }
    }

    //人物是否被平台保护
    public bool isProtected=false;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        unityArmature =gameObject.GetComponent<UnityArmatureComponent>();

        currentSpeed = 0;
        jumpSpeed = 0;
        totalJumps = 0;
        player_state = PlayState.OnGround;
        //ChangeWind(-4f);

        animation_name2index = new Dictionary<string, int>();
        animation_name2index.Add("走路", 0);
        animation_name2index.Add("跑步", 1);
        animation_name2index.Add("起跳", 2);
        animation_name2index.Add("上升", 3);
        animation_name2index.Add("下落", 4);
        animation_name2index.Add("落地", 5);
        animation_name2index.Add("静止", 8);
        animation_names = unityArmature.animation.animationNames;
    }

    void Update()
    {
        //Debug.Log(jumpSpeed);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
        }
        float horizontal = Input.GetAxis("Horizontal");
        move(horizontal);
        
        CheckState();
        ForcePower();
    }

    // Increase n towards target by speed
    #region 人物移动函数

    //慢慢将速度改变到目标速度，进行移动
    public void move(float target_speed)
    {
        if (player_state==PlayState.Stay)
        {
            return;
        }
        //在地上才允许改变横向速度，跳跃过程中不能更改
        if (player_state != PlayState.FishJump)
        {
            target_speed *= runSpeed;
            currentSpeed = IncrementTowards(currentSpeed, target_speed, acceleration);
        }
        //人物转向
        if (currentSpeed != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(currentSpeed) * Mathf.Abs(transform.localScale.x)
                 , transform.localScale.y);
        }

        Vector4 move_direct = rotation_mat * new Vector4(currentSpeed + wind.x, jumpSpeed+wind.y);
        //transform.Translate(move_direct * deltaTime);
        //_characterController.Move(move_direct*deltaTime);
        //播放动画效果
        if (jumpSpeed<=0&&jumpSpeed>-1)
        {
            //播放原地不动动画
            if (currentSpeed == 0)
            {
                if (!unityArmature.animation.isPlaying || (
                    unityArmature.animation.lastAnimationName != animation_names[animation_name2index["静止"]]&&
                    unityArmature.animation.lastAnimationName!= animation_names[animation_name2index["落地"]] &&
                    unityArmature.animation.lastAnimationName!=animation_names[animation_name2index["起跳"]]))
                {
                    unityArmature.animation.Play(animation_names[animation_name2index["静止"]]);
                    unityArmature.animation.GetState(animation_names[animation_name2index["静止"]]).timeScale = GetTimeScale(1);
                }
            }
            //播放跑步动画
            else
            {
                if (!unityArmature.animation.isPlaying || (
                    unityArmature.animation.lastAnimationName != animation_names[animation_name2index["跑步"]] &&
                    unityArmature.animation.lastAnimationName != animation_names[animation_name2index["下落"]] && 
                    unityArmature.animation.lastAnimationName != animation_names[animation_name2index["上升"]]))
                {
                    unityArmature.animation.Play(animation_names[animation_name2index["跑步"]],1);
                    unityArmature.animation.GetState(animation_names[animation_name2index["跑步"]]).timeScale = GetTimeScale(1);
                }
            }
        }
        else if(player_state!=PlayState.Stay) 
        {
            //空中上升动画
            if (jumpSpeed > 0)
            {
                if(!unityArmature.animation.isPlaying||(
                    unityArmature.animation.lastAnimationName != animation_names[animation_name2index["起跳"]] &&
                    unityArmature.animation.lastAnimationName != animation_names[animation_name2index["上升"]]))
                {
                    unityArmature.animation.Play(animation_names[animation_name2index["上升"]],1);
                    unityArmature.animation.GetState(animation_names[animation_name2index["上升"]]).timeScale = GetTimeScale(1.5f);
                }
            }
            //空中下落动画
            else
            {
                if (!unityArmature.animation.isPlaying ||
                    (unityArmature.animation.lastAnimationName != animation_names[animation_name2index["下落"]]))
                {
                    unityArmature.animation.Play(animation_names[animation_name2index["下落"]],1);
                    unityArmature.animation.GetState(animation_names[animation_name2index["下落"]]).timeScale = GetTimeScale(1);
                }
            }
        }
            
    }

    //跳跃
    public void jump()
    {
        player_state = PlayState.Jump;
        if (totalJumps < jumpsAllowed)
        {
            totalJumps++;
            unityArmature.animation.Play(animation_names[animation_name2index["起跳"]], 1);
            unityArmature.animation.GetState(animation_names[animation_name2index["起跳"]]).timeScale = GetTimeScale(2.0f);
            //播放动画一段时间后才起跳
           
            Invoke("ChangeJumpSpeed", GetTime(0.16f));
        }
    }
    
    private void ChangeJumpSpeed()
    {
        jumpSpeed = jumpHeight;
    }

    //不踩在陆地上时的掉落过程,风吹过程
    public void ForcePower()
    {
        //不在陆地上且不在鱼群上
        if (!isOnGround()&&player_state != PlayState.Stay)
        {
            jumpSpeed -= deltaTime * gravity;
        }
    }

    //进入风区
    public void ChangeWind(Vector2 w)
    {
        wind = w;
    }

    //转盘跳
    public void JumpAngle(float angle, bool isRadian = false)
    {
        //非弧度制，转换为弧度制
        if (!isRadian)
        {
            angle = angle * (float)Math.PI / 180;
        }
        
        jumpSpeed = jumpHeight * jump_force_factor * (float)(Math.Sin(angle));
        currentSpeed= -(jumpHeight * (float)(Math.Cos(angle)));
        
        player_state = PlayState.FishJump;
    }

    //静止空中
    public void Stay()
    {
        player_state = PlayState.Stay;
        currentSpeed = 0;
        jumpSpeed = 0;
    }

    //减速
    public void SlowDown()
    {
        StartCoroutine(SlowDownCoroutine());
    }

    private IEnumerator SlowDownCoroutine()
    {
        acceleration = 6f;
        unityArmature.animation.Play(animation_names[animation_name2index["走路"]]);
        yield return null;
    }
    #endregion

    //慢慢加速到需要的速度
    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n);
            n += a * deltaTime * dir;

            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }

    //是否落地函数
    private bool isOnGround()
    {
        //通过射线判断，从人物位置向下发出射线，得到碰撞点
        Vector4 ray_direct = rotation_mat * new Vector4(0, -1, 0, 1);
        Ray ray = new Ray(transform.position, ray_direct);
        RaycastHit hit;
        //如果有焦点
        if(Physics.Raycast(ray, out hit))
        {
            //Debug.Log(hit.collider.name);
            //Debug.Log(hit.distance);
            //Debug.Log(!(hit.collider.isTrigger));
            //碰撞点的tag不能是鱼
            return hit.distance < 8.5f*transform.localScale.y && !(hit.collider.isTrigger);
        }
        else
        {
            return false;
        }
    }

    //每帧判断是否落地
    private void CheckState()
    {

        if(isOnGround())
        {
            //如果刚从空中落地，播放落地动作，并更新跳跃参数
            if(player_state==PlayState.Jump||player_state==PlayState.FishJump)
            {
                if (jumpSpeed < 0)
                {
                    if (jumpSpeed < -1)
                    {
                        if (!unityArmature.animation.isPlaying ||
                            unityArmature.animation.lastAnimationName != animation_names[animation_name2index["落地"]])
                        {
                            unityArmature.animation.Play(animation_names[animation_name2index["落地"]], 1);
                            unityArmature.animation.GetState(animation_names[animation_name2index["落地"]]).timeScale = GetTimeScale(1.5f);
                        }
                    }
                    totalJumps = 0;
                    jumpSpeed = 0;
                }
            }
           
            player_state = PlayState.OnGround;
        }
        //如果跑步过程中凌空，就转换状态
        else if (player_state != PlayState.Stay && player_state != PlayState.FishJump)
        {
            player_state = PlayState.Jump;
        }

        ////摔死了，人生重来一次
        //if (jumpSpeed<-50)
        //{
        //    transform.position = new Vector3(-113, 1, -5);
        //}
    }

    public float GetTimeScale(float time_scale)
    {
        return EffectedByTime ? time_scale : time_scale / Time.timeScale;
    }

    public float GetTime(float time)
    {
        return EffectedByTime ? time : time * Time.timeScale;
    }

    public void Rotation(float angle,float speed=90f)
    {
        StartCoroutine(RotationByTime(angle, speed));
    }


    private IEnumerator RotationByTime(float angle,float speed)
    {
        Debug.Log("angle:" + angle + "  speed:" + speed);
        float current = transform.localRotation.eulerAngles.z;
        speed *= Mathf.Sign(angle-current);
        while(Math.Abs(current - angle) > 1)
        {
            current += speed * Time.deltaTime;
            if (speed>0&&current>angle)
            {
                current = angle;
            }
            else if(speed<0&&current<angle)
            {
                current = angle;
            }
            Debug.Log(current);
            transform.localRotation = Quaternion.Euler(0, 0, current);
            Camera.main.transform.localRotation = Quaternion.Euler(0, 0, current);
            yield return null;
        }
        yield return null;
    }

    public void Die()
    {
        Debug.Log("person died.");
    }
}

