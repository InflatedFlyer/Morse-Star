using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DragonBones;

public class test_VirtuallyWalk : MonoBehaviour
{
    private enum PlayState
    {
        Stay,
        Jump,
        FishJump,
        OnGround
    }

    // 人物调参属性
    public float gravity = 15;//重力
    public float runSpeed = 8;//奔跑速度
    public float walkSpeed = 4;
    public float acceleration = 30;//奔跑加速度
    public float jumpHeight = 10;//跳跃高度
    public float jump_force_factor = 1.5f;//改变鱼群跳跃的力
    private int jumpsAllowed = 1;//最大跳跃次数
    private Vector2 wind = Vector2.zero;

    //人物各项动态属性
    private float currentSpeed;//奔跑速度
    private float jumpSpeed;//跳跃高度
    private int totalJumps;//当前跳跃次数
    private PlayState player_state;//人物状态
    private bool isRun=true;

    private UnityArmatureComponent unityArmature;

    CharacterController _characterController;

    //骨骼动画各个动作的名字
    private List<string> animation_names;
    private Dictionary<string, int> animation_name2index;

    void Start()
    {
        unityArmature = gameObject.GetComponent<UnityArmatureComponent>();
        _characterController = GetComponent<CharacterController>();
        currentSpeed = 0;
        jumpSpeed = 0;
        totalJumps = 0;
        player_state = PlayState.OnGround;

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
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRun = !isRun;
        }
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
        if (player_state == PlayState.Stay)
        {
            return;
        }
        //在地上才允许改变横向速度，跳跃过程中不能更改
        if (player_state != PlayState.FishJump)
        {
            target_speed *= isRun?runSpeed:walkSpeed;
            currentSpeed = IncrementTowards(currentSpeed, target_speed, acceleration);
        }
        //人物转向
        if (currentSpeed != 0)
            transform.localScale = new Vector2(Mathf.Sign(currentSpeed) * Mathf.Abs(transform.localScale.x)
                , transform.localScale.y);
        if (_characterController != null)
        {
            _characterController.Move(new Vector2(currentSpeed + wind.x, jumpSpeed) * Time.deltaTime);
            //播放动画效果
            if (jumpSpeed <= 0 && jumpSpeed > -5)
            {
                //播放原地不动动画
                if (currentSpeed == 0)
                {
                    if (!unityArmature.animation.isPlaying || (
                        unityArmature.animation.lastAnimationName != animation_names[animation_name2index["静止"]] &&
                        unityArmature.animation.lastAnimationName != animation_names[animation_name2index["落地"]] &&
                        unityArmature.animation.lastAnimationName != animation_names[animation_name2index["起跳"]]))
                    {
                        unityArmature.animation.Play(animation_names[animation_name2index["静止"]]);
                    }
                }
                //播放跑步动画
                else
                {
                    if (isRun)
                    {
                        if (!unityArmature.animation.isPlaying || (
                            unityArmature.animation.lastAnimationName != animation_names[animation_name2index["跑步"]] &&
                            unityArmature.animation.lastAnimationName != animation_names[animation_name2index["下落"]] &&
                            unityArmature.animation.lastAnimationName != animation_names[animation_name2index["上升"]]))
                        {
                            unityArmature.animation.Play(animation_names[animation_name2index["跑步"]], 1);
                        }
                    }
                    else
                    {
                        if (!unityArmature.animation.isPlaying || (
                            unityArmature.animation.lastAnimationName != animation_names[animation_name2index["走路"]] &&
                            unityArmature.animation.lastAnimationName != animation_names[animation_name2index["下落"]] &&
                            unityArmature.animation.lastAnimationName != animation_names[animation_name2index["上升"]]))
                        {
                            unityArmature.animation.Play(animation_names[animation_name2index["走路"]], 1);
                        }
                    }
                }
            }
            else if (player_state != PlayState.Stay)
            {
                //空中上升动画
                if (jumpSpeed > 0)
                {
                    if (!unityArmature.animation.isPlaying || (
                        unityArmature.animation.lastAnimationName != animation_names[animation_name2index["起跳"]] &&
                        unityArmature.animation.lastAnimationName != animation_names[animation_name2index["上升"]]))
                    {
                        unityArmature.animation.Play(animation_names[animation_name2index["上升"]], 1);
                        unityArmature.animation.GetState(animation_names[animation_name2index["上升"]]).timeScale = 1.5f;
                    }
                }
                //空中下落动画
                else
                {
                    if (!unityArmature.animation.isPlaying ||
                        (unityArmature.animation.lastAnimationName != animation_names[animation_name2index["下落"]]))
                    {
                        unityArmature.animation.Play(animation_names[animation_name2index["下落"]], 1);
                        unityArmature.animation.GetState(animation_names[animation_name2index["下落"]]).timeScale = 1f;
                    }
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
            unityArmature.animation.Play(animation_names[animation_name2index["起跳"]], 1);
            unityArmature.animation.GetState(animation_names[animation_name2index["起跳"]]).timeScale = 2f;
            //播放动画一段时间后才起跳
            Invoke("ChangeJumpSpeed", 0.16f);
        }
    }

    private void ChangeJumpSpeed()
    {
        totalJumps++;
        jumpSpeed = jumpHeight;
    }

    //不踩在陆地上时的掉落过程,风吹过程
    public void ForcePower()
    {
        //不在陆地上且不在鱼群上
        if (!isOnGround() && player_state != PlayState.Stay)
        {
            jumpSpeed -= Time.deltaTime * (gravity+wind.y);
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
        currentSpeed = -(jumpHeight * (float)(Math.Cos(angle)));

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
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target;
        }
    }

    //是否落地函数
    private bool isOnGround()
    {
        //通过射线判断，从人物位置向下发出射线，得到碰撞点
        Ray ray = new Ray(transform.position, -Vector3.up);
        RaycastHit hit;
        //如果有焦点
        if (Physics.Raycast(ray, out hit))
        {

            //碰撞点的tag不能是鱼
            return hit.distance < 8.5f * transform.localScale.y && !(hit.collider.isTrigger);
        }
        else
        {
            return false;
        }
    }

    //每帧判断是否落地
    private void CheckState()
    {

        if (isOnGround())
        {
            //如果刚从空中落地，播放落地动作，并更新跳跃参数
            if (player_state == PlayState.Jump || player_state == PlayState.FishJump)
            {
                if (jumpSpeed < -5)
                {
                    if (!unityArmature.animation.isPlaying ||
                        unityArmature.animation.lastAnimationName != animation_names[animation_name2index["落地"]])
                    {
                        unityArmature.animation.Play(animation_names[animation_name2index["落地"]], 1);
                        unityArmature.animation.GetState(animation_names[animation_name2index["落地"]]).timeScale = 1.5f;
                    }
                }
                totalJumps = 0;
                jumpSpeed = 0;
            }

            player_state = PlayState.OnGround;
        }
        //如果跑步过程中凌空，就转换状态
        else if (player_state != PlayState.Stay && player_state != PlayState.FishJump)
        {
            player_state = PlayState.Jump;
        }

        //摔死了，人生重来一次
        if (jumpSpeed < -50)
        {
            transform.position = new Vector3(-113, 1, -5);
        }
    }
}

