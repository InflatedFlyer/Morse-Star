using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class AXYDormitoryCh1Scene : BaseScene
{
    public enum AXYDormitoryCh1SceneState
    {
        None,
        AirConditioning,
        Piano
    }
    public static bool isFirstTime = true;
    public static AXYDormitoryCh1SceneState state=AXYDormitoryCh1SceneState.None;

    private GameObject piano_game;
    private GameObject inside_door;


    protected override void Awake()
    {
        base.Awake();
        _name = "AXYDormitory";
    
    }

  

    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        EventManager.getInstance().StartListening("吉他",GuitarTrigger);
        EventManager.getInstance().StartListening("电脑桌", ComputerTrigger);
        EventManager.getInstance().StartListening("空调", AirCTrigger);
        EventManager.getInstance().StartListening("主街", MainStreetTrigger);
        EventManager.getInstance().StartListening("安星语密室门", InsideDoorTrigger);
        EventManager.getInstance().StartListening("钢琴", PianoTrigger);
        //EventManager.getInstance().StartListening("床头柜", BedsideTrigger);
        base.Load(init_pos, isAsync);
        
    }

    public override void SceneFlow(Operate.Command operate)
    {
        base.SceneFlow(operate);
        
    }

    public override void Close()
    {
        EventManager.getInstance().StopListening("吉他", GuitarTrigger);
        EventManager.getInstance().StopListening("电脑桌", ComputerTrigger);
        EventManager.getInstance().StopListening("空调", AirCTrigger);
        EventManager.getInstance().StopListening("主街", MainStreetTrigger);
        EventManager.getInstance().StopListening("安星语密室门", InsideDoorTrigger);
        EventManager.getInstance().StopListening("钢琴", PianoTrigger);
        // EventManager.getInstance().StopListening("床头柜", BedsideTrigger);
        base.Close();
    }

    public override void Save(string filename, string extend_filename = null)
    {
        base.Save(filename, extend_filename);
    }

    public override void LoadSceneFile(string filename)
    {
        base.LoadSceneFile(filename);
    }

    public override void LoadSuccess()
    {
        base.LoadSuccess();

        piano_game = GameObject.Find("Piano");
        inside_door = GameObject.Find("安星语密室门");
        if (inside_door != null)
        {
            inside_door.SetActive(false);
        }

        if (isFirstTime)
        {
            DialogueManager.StartConversation("黎茗自言自语");
        }
        isFirstTime = false;

        if (state==AXYDormitoryCh1SceneState.Piano)
        {
            OpenInsideDoor();
        }
    }

    public void GuitarTrigger()
    {
        DialogueManager.StartConversation("吉他");
    }

    public void ComputerTrigger()
    {
        DialogueManager.StartConversation("电脑桌");
    }

    public void AirCTrigger()
    {
        DialogueManager.StartConversation("空调");
        if (state == AXYDormitoryCh1SceneState.None)
        {
            state = AXYDormitoryCh1SceneState.AirConditioning;
        }
    }
    public void PianoTrigger()
    {
        if (state == AXYDormitoryCh1SceneState.AirConditioning)
        {
            StartCoroutine(StartPianoGame());
        }
    }

    public IEnumerator StartPianoGame()
    {
        AudioSource music=null;
        GameObject go = GameObject.Find("BackGroundMusic");
        if(go!=null)
        {
            music = go.GetComponent<AudioSource>();
            if(music!=null)
            {
                music.Pause();
            }
        }
               
        if (piano_game != null)
        {
            PianoCtrL piano_ctrl = piano_game.GetComponent<PianoCtrL>();
            if (piano_ctrl != null)
            {
                liming.LoseControl();
                
                piano_ctrl.StartGame();
                while(piano_game.activeSelf)
                {
                    yield return null;
                }

                OpenInsideDoor();
                liming.GetControl();
                if(music!=null)
                {
                    music.Play();
                }
            }
        }
        yield return null;
    }

    public void OpenInsideDoor()
    {
        if(state==AXYDormitoryCh1SceneState.AirConditioning)
        {
            DialogueManager.StartConversation("秘密通道");
        }
        inside_door.SetActive(true);
        GameObject.Find("安星语居室3_0000_挡住门").GetComponent<OpenAXYDoor>().ifopen = true;

        state = AXYDormitoryCh1SceneState.Piano;
    }
    
    public void InsideDoorTrigger()
    {
        GameManager.getInstance().ChangeSceneTo(
               GameManager.getInstance().gameObject.AddComponent<AXYInsideCh1Scene>(), new Vector3(-18.1f, -1.16f, -3));
    }

    public void MainStreetTrigger()
    {
        GameManager.getInstance().ChangeSceneTo(
                 GameManager.getInstance().gameObject.AddComponent<MainStreet1Ch1Scene>(), new Vector3(120.95f, -1.2f, -3));

    }
}
