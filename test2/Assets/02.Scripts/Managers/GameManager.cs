using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using PathologicalGames;
public class GameManager : MonoBehaviour {

    #region 管理类
    private LevelManager level_manager;
    private AudioManager audio_manager;
    private EventManager event_manager;
    private UIControl ui_manager;
    private SaveManager save_manager;
    private SpawnPool ui_pool;

    private static GameManager instance;
    #endregion
    
    private VirtuallyWalk virtual_liming;
    
    private BaseScene current_scene;

    //保存文件名
    private string base_scene_file;//当前场景编号，人物位置
    private string extend_scene_file;//具体场景的状态

    private float last_click;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        instance = this;

        current_scene = null;

        base_scene_file = "BaseScene.json";
        extend_scene_file = null;
    }

    void Start() {
        GameStart();
    }

    private void Update()
    {
        getOperate();

        DeliverOperate();
    }

    public static GameManager getInstance()
    {
        return instance;
    }

    //游戏开始的配置信息
    public void GameStart()
    {
        //加载管理器实例
        level_manager = LevelManager.getInstance();
        audio_manager = AudioManager.getInstance();
        event_manager = EventManager.getInstance();
        ui_manager = UIControl.getInstance(); 
        save_manager = SaveManager.getInstance();
        ui_pool = GameObject.FindWithTag("SpawnPool").GetComponent<SpawnPool>();
        
        BaseScene.liming = GameObject.FindWithTag("Character").GetComponent<WalkCharacter>();
        BaseScene.liming.gameObject.SetActive(false);

        //读取场景
        if (save_manager.IsFileExists(base_scene_file))
        {
            LoadSaveScene();
        }
        else
        {
            LoadFixCenterCh1Scene();
            //LoadMainStreet1Ch1Scene();
            //LoadAXYDormitoryCh1Scene();
            //LoadLibraryCh1Scene();
            //LoadLaboratoryCenterCh1Scene();
            //LoadAXYInsideCh1Scene();
            //LoadBarCh1Scene();
            //LoadLabPassCh1Scene();
            //LoadBarCh1Scene();
        }
    }
    
    
    #region 操作层封装
    //读取操作
    public void getOperate()
    {
        if (current_scene != null)
        {
            //虚拟世界的操作
            if (current_scene.isVirtualWorld())
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Operate.AddCommand(Operate.Command.VirtualTrigger);
                }
                float horizontal = Input.GetAxisRaw("Horizontal");
                if (horizontal!=0)
                {
                    Operate.AddCommand(Operate.Command.VirtualMove, horizontal);
                }
            }
            //非虚拟世界的操作
            else
            {
                if(Input.GetMouseButtonUp(1))
                {
                    Operate.AddCommand(Operate.Command.Walk, Camera.main.ScreenToWorldPoint(Input.mousePosition));     
                }

                if(Input.GetMouseButtonDown(1))
                {
                    last_click = Time.time;
                }

                if(Input.GetMouseButton(1))
                {
                    if(Time.time-last_click>0.5f)
                    {
                        Operate.AddCommand(Operate.Command.Run, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    }
                }
                //bool isRun = false;
                //if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                //{
                //    isRun = true;
                //}

                //if (Input.GetKey(KeyCode.A))
                //{
                //    if (isRun)
                //    {
                //        Operate.AddCommand(Operate.Command.LeftRun);
                //    }
                //    else
                //    {
                //        Operate.AddCommand(Operate.Command.LeftWalk);
                //    }
                //}
                //else if (Input.GetKey(KeyCode.D))
                //{
                //    if (isRun)
                //    {
                //        Operate.AddCommand(Operate.Command.RightRun);
                //    }
                //    else
                //    {
                //        Operate.AddCommand(Operate.Command.RightWalk);
                //    }
                //}

                //if (Input.GetKeyDown(KeyCode.Alpha1))
                //{
                //    Operate.AddCommand(Operate.Command.SaveScene);
                //}

                //if (Input.GetKeyDown(KeyCode.Space))
                //{
                //    Operate.AddCommand(Operate.Command.Trigger);
                //}
            }
        }
    }

    //分发操作到不同的模块
    public void DeliverOperate()
    {
        Operate.Command cmd = Operate.GetCommand();
        switch(cmd)
        {
            case Operate.Command.None:
            case Operate.Command.Walk:
            case Operate.Command.Run:
            //case Operate.Command.LeftWalk:
            //case Operate.Command.LeftRun:
            //case Operate.Command.RightWalk:
            //case Operate.Command.RightRun:
            case Operate.Command.Trigger:
            case Operate.Command.VirtualMove:
            case Operate.Command.VirtualTrigger:
                {
                    if (current_scene != null)
                    {
                        current_scene.SceneFlow(cmd);
                    }
                    break;
                }
            case Operate.Command.SaveScene:
                {
                    //if (current_scene != null)
                    //{
                    //    current_scene.Save(base_scene_file, extend_scene_file);
                    //}
                    break;
                }
            default:
                break;
        }
    }
    #endregion

    #region 场景功能函数

    //切换场景
    public void ChangeSceneTo(BaseScene new_scene, Vector3 liming_pos,bool isAsync = true)
    {
        CloseCurrentScene();
        LoadNewScene(new_scene,liming_pos,isAsync);
    }

    //关闭当前场景
    public void CloseCurrentScene()
    {
        if (current_scene != null)
        {
            current_scene.Close();
            Destroy(current_scene);
            current_scene = null;
        }
    }

    //加载新场景
    public void LoadNewScene(BaseScene new_scene,Vector3 liming_pos,bool isAsync=true)
    {
        current_scene = new_scene;
        current_scene.Load(liming_pos,isAsync);
        extend_scene_file =current_scene.GetType().ToString()+".json";
    }

    //保存当前场景
    public void SaveCurrentScene(string base_scene,string extend_scene=null)
    {
        current_scene.Save(base_scene,extend_scene);
    }

    //读取存档
    public void LoadSaveScene()
    {
        //加载基础场景
        SaveBaseScene save_basescene = new SaveBaseScene();
        save_basescene =(SaveBaseScene)(save_manager.GetData(base_scene_file, typeof(SaveBaseScene)));

        BaseScene.liming.transform.position = 
            new Vector3(save_basescene.player_position_x, save_basescene.player_position_y);
        
        if(extend_scene_file!=null)
        {
            current_scene.LoadSceneFile(extend_scene_file);
        }
    }
    #endregion

    #region 加载主线场景
    public void LoadFixCenterCh1Scene()
    {
        ChangeSceneTo(gameObject.AddComponent<FixCenterCh1Scene>(), new Vector3(11.76f, -1.86f, -3));
    }

    public void LoadMainStreet1Ch1Scene()
    {
        ChangeSceneTo(gameObject.AddComponent<MainStreet1Ch1Scene>(), new Vector3(137.7f, -1.2f, -3));
    }

    public void LoadAXYDormitoryCh1Scene()
    {
        ChangeSceneTo(gameObject.AddComponent<AXYDormitoryCh1Scene>(), new Vector3(-18.1f, -1.16f, -3));
    }

    public void LoadLibraryCh1Scene()
    {
        ChangeSceneTo(gameObject.AddComponent<LibraryCh1Scene>(), new Vector3(-17, -25f, -3));
    }

    public void LoadLaboratoryCenterCh1Scene()
    {
        ChangeSceneTo(gameObject.AddComponent<LaboratoryCenterCh1Scene>(), new Vector3(-20, -2, -3));
    }

    public void LoadAXYInsideCh1Scene()
    {
        ChangeSceneTo(gameObject.AddComponent<AXYInsideCh1Scene>(), new Vector3(-2, 0, -3));
    }

    public void LoadBarCh1Scene()
    {
        ChangeSceneTo(gameObject.AddComponent<BarCh1Scene>(), new Vector3(-32, -1.6f, -4.5f));
    }

    public void LoadLabPassCh1Scene()
    {
        ChangeSceneTo(gameObject.AddComponent<LabPassCh1Scene>(), new Vector3(85.24f, -1.95f, -3));
    }
    #endregion

}