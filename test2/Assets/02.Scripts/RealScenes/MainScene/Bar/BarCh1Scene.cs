using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class BarCh1Scene : BaseScene
{
    private float wait_time;

    public static bool isFirstTime = true;
    OverLay overlay;

    public enum BarCh1SceneState
    {
        None,
        TalkToWaiter,
        ReadMemoryFile,
        HistoryBack
    }
    public BarCh1SceneState state = BarCh1SceneState.None;

    protected override void Awake()
    {
        base.Awake();
        _name = "Bar";
    }

    public override void Load(Vector3 init_pos, bool isAsync = true)
    {

        EventManager.getInstance().StartListening("服务员", WaiterTrigger);
        EventManager.getInstance().StartListening("记忆文件", ReadMemoryTrigger);
        EventManager.getInstance().StartListening("主街", MainStreetTrigger);
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
        EventManager.getInstance().StopListening("服务员", WaiterTrigger);
        EventManager.getInstance().StopListening("记忆文件", ReadMemoryTrigger);
        base.Close();
    }

    public override void LoadSuccess()
    {
        base.LoadSuccess();
        if(state==BarCh1SceneState.HistoryBack)
        {
            DialogueManager.StartConversation("和安星语对话后");
            state = BarCh1SceneState.TalkToWaiter;
        }
        isFirstTime = false;

        overlay = GameObject.Find("OverLay").GetComponent<OverLay>();
    }

    //和服务生对话trigger
    public void WaiterTrigger()
    {
        StartCoroutine(TalkToWaiter());
    }

    //读取记忆文件
    public void ReadMemoryTrigger()
    {
        StartCoroutine(ReadMemoryFile());
    }
    

    //和服务生对话
    public IEnumerator TalkToWaiter()
    {
        Debug.Log("TalkToWaiter");
        
        if (state == BarCh1SceneState.None)
        {
            state = BarCh1SceneState.TalkToWaiter;
            DialogueManager.StartConversation("服务员");
            while(DialogueManager.IsConversationActive)
            {
                yield return null;
            }
            yield return new WaitForSeconds(1);
            DialogueManager.StartConversation("搭话T2319");
        }
        else if(state==BarCh1SceneState.ReadMemoryFile)
        {
            state = BarCh1SceneState.TalkToWaiter;
            DialogueManager.StartConversation("服务员");
            while (DialogueManager.IsConversationActive)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1);
            DialogueManager.StartConversation("搭话T2319");
            while (DialogueManager.IsConversationActive)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1);
            DialogueManager.StartConversation("T2319的补充");
        }
        else if(state==BarCh1SceneState.TalkToWaiter)
        {
            DialogueManager.StartConversation("服务员");
        }
    }
    
    //读取记忆文件分支
    public IEnumerator ReadMemoryFile()
    {
        if (state == BarCh1SceneState.TalkToWaiter)
        {
            StartCoroutine(StartOverlay());
        }
        else
        {
            state = BarCh1SceneState.ReadMemoryFile;
            DialogueManager.StartConversation("黎茗分析");
        }
        yield return null;
    }

    public IEnumerator StartOverlay()
    {
        Debug.Log("StartOverlay");
        liming.LoseControl();
        
        StartCoroutine(overlay.GameStart());
        while(overlay.gameObject.activeSelf)
        {
            yield return null;
        }

        liming.GetControl();
        ChangeToBarHistory();

    }

    public void ChangeToBarHistory()
    {
        BarHistoryCh1Scene bar_history = GameManager.getInstance().gameObject.AddComponent<BarHistoryCh1Scene>();
        bar_history.reality_pos = liming.transform.position;
        GameManager.getInstance().ChangeSceneTo(
            bar_history
            , new Vector3(-14.7f,-2,-3));
    }

    public void MainStreetTrigger()
    {
        GameManager.getInstance().ChangeSceneTo(
            GameManager.getInstance().gameObject.AddComponent<MainStreet1Ch1Scene>(), new Vector3(81, -1.2f, -3));
    }
}


