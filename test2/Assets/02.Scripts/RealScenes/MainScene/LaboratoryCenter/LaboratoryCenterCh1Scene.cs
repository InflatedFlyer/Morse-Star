using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.Video;
using UnityEngine.UI;

public class LaboratoryCenterCh1Scene : BaseScene
{
    private GameObject video;

    protected override void Awake()
    {
        base.Awake();
        _name = "Laboratory";
    }

    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        EventManager.getInstance().StartListening("安检机器",SecurityCheckTrigger);
        EventManager.getInstance().StartListening("神经扫描分析仪", NeuroScanTrigger);
        EventManager.getInstance().StartListening("主操作台", ConsoleTrigger);
        EventManager.getInstance().StartListening("实验室的血迹", BloodStainTrigger);
        EventManager.getInstance().StartListening("安星语的尸体", AXYBodyTrigger);
        EventManager.getInstance().StartListening("1442的尸体", T1442BodyTrigger);
        EventManager.getInstance().StartListening("墙上贴纸1",WallPaper1Trigger);
        EventManager.getInstance().StartListening("墙上贴纸2", WallPaper2Trigger);
        EventManager.getInstance().StartListening("墙上贴纸3", WallPaper3Trigger);
        EventManager.getInstance().StartListening("墙上贴纸4", WallPaper4Trigger);
        EventManager.getInstance().StartListening("墙上贴纸5", WallPaper5Trigger);
        EventManager.getInstance().StartListening("走廊", LabPassTrigger);
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
        EventManager.getInstance().StopListening("安检机器", SecurityCheckTrigger);
        EventManager.getInstance().StopListening("神经扫描分析仪", NeuroScanTrigger);
        EventManager.getInstance().StopListening("主操作台", ConsoleTrigger);
        EventManager.getInstance().StopListening("实验室的血迹", BloodStainTrigger);
        EventManager.getInstance().StopListening("安星语的尸体", AXYBodyTrigger);
        EventManager.getInstance().StopListening("1442的尸体", T1442BodyTrigger);
        EventManager.getInstance().StopListening("墙上贴纸1", WallPaper1Trigger);
        EventManager.getInstance().StopListening("墙上贴纸2", WallPaper2Trigger);
        EventManager.getInstance().StopListening("墙上贴纸3", WallPaper3Trigger);
        EventManager.getInstance().StopListening("墙上贴纸4", WallPaper4Trigger);
        EventManager.getInstance().StopListening("墙上贴纸5", WallPaper5Trigger);
        EventManager.getInstance().StopListening("走廊", LabPassTrigger);
        base.Close();
    }

    public override void LoadSuccess()
    {
        base.LoadSuccess();

        video = GameObject.Find("video");
        video.SetActive(false);
    }

    public void SecurityCheckTrigger()
    {

        DialogueManager.StartConversation("安检机器");
    }

    public void WallPaper1Trigger()
    {

        DialogueManager.StartConversation("墙上贴纸1");
    }

    public void WallPaper2Trigger()
    {
        DialogueManager.StartConversation("墙上贴纸2");

    }

    public void WallPaper3Trigger()
    {
        DialogueManager.StartConversation("墙上贴纸3");

    }

    public void WallPaper4Trigger()
    {

        DialogueManager.StartConversation("墙上贴纸4");

    }

    public void WallPaper5Trigger()
    {

        DialogueManager.StartConversation("墙上贴纸5");

    }

    public void NeuroScanTrigger()
    {

        DialogueManager.StartConversation("神经扫描分析仪");
    }

    public void ConsoleTrigger()
    {

        DialogueManager.StartConversation("主操作台");
    }

    public void BloodStainTrigger()
    {

        DialogueManager.StartConversation("实验室的血迹");
    }

    public void AXYBodyTrigger()
    {
        
        DialogueManager.StartConversation("安星语尸体");

    }

    public void T1442BodyTrigger()
    {
        StartCoroutine(T1442Flow());
    }

    public IEnumerator T1442Flow()
    {
        DialogueManager.StartConversation("1442的尸体");
        while(DialogueManager.IsConversationActive)
        {
            yield return null;
        }
        StartCoroutine(StartMontageGame());

    }
    public IEnumerator StartMontageGame()
    {
        Debug.Log(1);
        GameObject go = GameObject.Find("SortStory");
        SortStory sort = go.GetComponent<SortStory>();
        liming.LoseControl();

        sort.ShowScrollMenu();
        while (sort.gameObject.activeSelf)
        {
            if (Input.GetMouseButtonDown(1) && !sort.sort_page.gameObject.activeSelf)
            {
                sort.ChangeToSortPage();
            }
            yield return null;
        }

        liming.GetControl();
        StartCoroutine(PlayVideo());
    }

    public IEnumerator PlayVideo()
    {
        liming.GetComponent<AudioSource>().Stop();
        Debug.Log(1);
        if (video != null)
        {
            Debug.Log(1);
            video.SetActive(true);
            VideoPlayer video_play = video.GetComponent<VideoPlayer>();
            if (video_play != null)
            {
                
                video_play.Play();

                Debug.Log((float)video_play.clip.length);
                yield return new WaitForSeconds((float)video_play.clip.length);
                
                video.SetActive(false);
            }
        }
    }

    public void LabPassTrigger()
    {
        GameManager.getInstance().ChangeSceneTo(
          GameManager.getInstance().gameObject.AddComponent<LabPassCh1Scene>(), new Vector3(85.24f, -1.95f, -3));
    }


}
