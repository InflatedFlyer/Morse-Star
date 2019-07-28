using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class LabPassCh1Scene : BaseScene
{
    public static bool isFirstTime = true;
    protected override void Awake()
    {
        base.Awake();
        _name = "Passageaway";

    }
    

    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        EventManager.getInstance().StartListening("损坏机械个体", BrokenMacTrigger);
        EventManager.getInstance().StartListening("走廊拖动血迹", BloodTrigger);
        EventManager.getInstance().StartListening("创的机房", EngineRoomTrigger);
        EventManager.getInstance().StartListening("杂乱的电线", MessyWireTrigger);
        EventManager.getInstance().StartListening("墙裂开背的后机械结构", WallBehindTrigger);
        EventManager.getInstance().StartListening("被启动的实验室安保记录", SecRecordTrigger);
        EventManager.getInstance().StartListening("不稳定的显示设备", ScreenTrigger);
        EventManager.getInstance().StartListening("实验室", LabTrigger);
        EventManager.getInstance().StartListening("图书馆", LibTrigger);
        base.Load(init_pos, isAsync);

    }

    public override void SceneFlow(Operate.Command operate)
    {
        base.SceneFlow(operate);

    }

    public override void Close()
    {
        EventManager.getInstance().StopListening("损坏机械个体", BrokenMacTrigger);
        EventManager.getInstance().StopListening("走廊拖动血迹", BloodTrigger);
        EventManager.getInstance().StopListening("创的机房", EngineRoomTrigger);
        EventManager.getInstance().StopListening("杂乱的电线", MessyWireTrigger);
        EventManager.getInstance().StopListening("墙裂开背的后机械结构", WallBehindTrigger);
        EventManager.getInstance().StopListening("被启动的实验室安保记录", SecRecordTrigger);
        EventManager.getInstance().StopListening("不稳定的显示设备", ScreenTrigger);
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
        if(isFirstTime)
        {
            DialogueManager.StartConversation("引导");
        }
        isFirstTime = false;
        
    }

    public void BrokenMacTrigger()
    {
        DialogueManager.StartConversation("损坏机械个体");
    }

    public void BloodTrigger()
    {
        DialogueManager.StartConversation("走廊拖动血渍");
    }

    public void EngineRoomTrigger()
    {
        DialogueManager.StartConversation("创的机房");
    }

    public void MessyWireTrigger()
    {
        DialogueManager.StartConversation("杂乱的电线");
    }

    public void WallBehindTrigger()
    {
        DialogueManager.StartConversation("墙裂开背的后机械结构 ");
    }

    public void SecRecordTrigger()
    {
        DialogueManager.StartConversation("被启动的实验室安保记录 ");
    }

    public void ScreenTrigger()
    {
        DialogueManager.StartConversation("不稳定的显示设备 ");
    }

    public void LabTrigger()
    {
        GameManager.getInstance().ChangeSceneTo(
        GameManager.getInstance().gameObject.AddComponent<LaboratoryCenterCh1Scene>(), new Vector3(-24.91f, -2.58f, -4f));
    }

    public void LibTrigger()
    {
        GameManager.getInstance().ChangeSceneTo(
         GameManager.getInstance().gameObject.AddComponent<LibraryCh1Scene>(), new Vector3(40.87f, -2.25f, -4f));
    }
}
