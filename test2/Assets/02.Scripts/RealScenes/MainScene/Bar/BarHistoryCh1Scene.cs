using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class BarHistoryCh1Scene : BaseScene
{
    private bool[] isShadowsTrigger=new bool[] { false,false,false};
    public Vector3 reality_pos;

    protected override void Awake()
    {
        base.Awake();
        _name = "Bar(history)";
    }

    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        EventManager.getInstance().StartListening("门口残影", BarShadowTrigger);
        EventManager.getInstance().StartListening("过道残影", WindowShadowTrigger);
        EventManager.getInstance().StartListening("座位处人影", SeatShadowTrigger);
        base.Load(init_pos, isAsync);
    }

    public override void SceneFlow(Operate.Command operate)
    {
        base.SceneFlow(operate);

    }

    public override void Save(string filename, string extend_filename = null)
    {
        base.Save(filename, extend_filename);
    }

    public override void LoadSceneFile(string filename)
    {
        base.LoadSceneFile(filename);
    }

    public override void Close()
    {
        EventManager.getInstance().StopListening("门口残影", BarShadowTrigger);
        EventManager.getInstance().StopListening("过道残影", WindowShadowTrigger);
        EventManager.getInstance().StopListening("座位处人影", SeatShadowTrigger);
        base.Close();
    }

    public override void LoadSuccess()
    {
        base.LoadSuccess();
        DialogueManager.StartConversation("历史世界引导");
        //StartCoroutine(LimingTalk());
    }
    
    public void BarShadowTrigger()
    {
        StartCoroutine(StartConversation("门口残影", 0));
    }

    public void WindowShadowTrigger()
    {
        StartCoroutine(StartConversation("过道残影", 1));
    }

    public void SeatShadowTrigger()
    {
        StartCoroutine(StartConversation("座位处人影",2));
        
    }
    public IEnumerator StartConversation(string title,int shadow_index)
    {
        DialogueManager.StartConversation(title);
        while(DialogueManager.IsConversationActive)
        {
            yield return null;
        }
        isShadowsTrigger[shadow_index] = true;
        CheckState();
    }

    public void CheckState()
    {
        if(isShadowsTrigger[0]&&isShadowsTrigger[1]&&isShadowsTrigger[2])
        {
            ChangeSceneToReality();
        }
    }

    public void ChangeSceneToReality()
    {
        BarCh1Scene barCh1Scene = GameManager.getInstance().gameObject.AddComponent<BarCh1Scene>();
        barCh1Scene.state = BarCh1Scene.BarCh1SceneState.HistoryBack;
        GameManager.getInstance().ChangeSceneTo(barCh1Scene,
            reality_pos);
        
    }
}


