using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
public class BaseScene:MonoBehaviour
{
    //场景index和名称
    protected int _index;
    public int index
    {
        get
        {
            return _index;
        }
    }

    protected string _name;
    public string name
    {
        get
        {
            return _name;
        }
    }

    protected bool isVirtual;

    protected Vector3 init_pos;
    //人物脚本
    public static WalkCharacter liming = null;
    public static VirtuallyWalk virtual_liming = null;
    

    protected virtual void Awake()
    {
        Init();
    }

    public void Init()
    {
        isVirtual = false;
    }

    //加载场景
    virtual public void Load(Vector3 init_pos,bool isAsync=true)
    {
        if (isVirtual && virtual_liming != null)
        {
            virtual_liming.gameObject.SetActive(false);
        }
        else if (!isVirtual && liming != null)
        {
            liming.gameObject.SetActive(false);
        }

        this.init_pos = init_pos;
        liming.target_pos = init_pos;
        
        if (_name != null)
        {
            EventManager.getInstance().StartListening(string.Format("LoadScene{0}", _name), LoadSuccess);
            if (LevelManager.getInstance().getActiveSceneName() != _name)
            {
                LevelManager.getInstance().LoadScene(_name, isAsync);

            }
            else
            {
                EventManager.getInstance().TriggerEvent(string.Format("LoadScene{0}", _name));
            }
        }
        else if (_index != -1)
        {
            EventManager.getInstance().StartListening(string.Format("LoadScene{0}", index), LoadSuccess);
            LevelManager.getInstance().LoadScene(_index, isAsync);
        }
    }

    //场景的流程
    virtual public void SceneFlow(Operate.Command operate)
    {
        //基本人物动作
        #region people behaviour
        switch (operate)
        {
            //case Operate.Command.LeftWalk:
            //    liming.Walk(-1);
            //    break;
            //case Operate.Command.RightWalk:
            //    liming.Walk(1);
            //    break;
            //case Operate.Command.LeftRun:
            //    liming.Run(-1);
            //    break;
            //case Operate.Command.RightRun:
            //    liming.Run(1);
            //    break;
            case Operate.Command.Walk:
                {
                    Vector2 target;
                    bool isMove = Operate.GetMousePosition(out target);
                    if (isMove)
                    {
                        liming.Walk(target.x);
                    }
                    break;
                }
            case Operate.Command.Run:
                {
                    Vector2 target;
                    bool isMove = Operate.GetMousePosition(out target);
                    if (isMove)
                    {
                        liming.Run(target.x);
                    }
                    break;
                }
            case Operate.Command.None:
                if (isVirtual)
                {
                    virtual_liming.move(0);
                }
                else
                {
                    liming.Stop();
                }
                break;
            case Operate.Command.VirtualMove:
                virtual_liming.move(Operate.GetAxisHorizon());
                break;
            default:
                break;
        }
        #endregion
    }

    //保存场景
    virtual public void Save(string filename,string extend_filename=null)
    {
        //保存当前场景index和人物

        //提取场景信息
        SaveBaseScene save_base = new SaveBaseScene();
        save_base.scene_index = index;
        save_base.player_position_x = liming.transform.position.x;
        save_base.player_position_y = liming.transform.position.y;

        SaveManager.getInstance().SetData(filename, save_base);
    }

    //场景关闭函数
    virtual public void Close()
    {
        UIControl.getInstance().LinkToMainManager();

        EventManager.getInstance().StopListening(string.Format("LoadScene{0}", index), LoadSuccess);
        if (isVirtual && virtual_liming != null)
        {
            virtual_liming.gameObject.SetActive(false);
        }
        else if (!isVirtual && liming != null)
        {
            liming.gameObject.SetActive(false);
        }
    }

    //场景读取存档函数
    virtual public void LoadSceneFile(string filename)
    {
        
    }
    
    virtual public void LoadSuccess()
    {
        if (isVirtual && virtual_liming != null)
        {
            virtual_liming.gameObject.SetActive(true);
            virtual_liming.transform.position = init_pos;
        }
        else if (!isVirtual && liming != null)
        {
            liming.gameObject.SetActive(true);
            liming.transform.position = init_pos;
        }

        liming.GetComponent<AudioSource>().clip = null;

        Cursor.SetCursor(default, new Vector2(100f, 100f), CursorMode.Auto);

        Operate.ClearCommand();
        
        //添加摄像机对象的目标
        if (Camera.main.GetComponent<ProCamera2D>() != null)
        {
            if (isVirtual)
            {
                ProCamera2D.Instance.AddCameraTarget(virtual_liming.transform);
            }
            else
            {
                ProCamera2D.Instance.AddCameraTarget(liming.transform);
            }
            ProCamera2D.Instance.CenterOnTargets();
            UIControl.getInstance().LinkToMainCamera();
        }


        
    }

    public bool isVirtualWorld()
    {
        return isVirtual;
    }
}

public class VirtualBaseScene : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
        isVirtual = true;
    }
}