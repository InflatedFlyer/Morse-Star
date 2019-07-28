using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class AXYInsideCh1Scene : BaseScene
{
    public static bool isFirstTime = true;

    private enum AXYInsideCh1SceneState
    {
        BeformAquarium,
        Aquarium,
        AfterMontageGame
    }
    private AXYInsideCh1SceneState state = AXYInsideCh1SceneState.BeformAquarium;

    private GameObject history_world;
    private GameObject reality_world;
    protected override void Awake()
    {
        base.Awake();
        _name = "AXYDormitoryInside";
    }

    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        EventManager.getInstance().StartListening("培养缸前", AquariumTrigger);
        EventManager.getInstance().StartListening("培养缸", AquariumClickTrigger);
        EventManager.getInstance().StartListening("办公桌前人影", ShadowTrigger);
        EventManager.getInstance().StartListening("办公桌", DeskTrigger);
        EventManager.getInstance().StartListening("空相框", FrameTrigger);
        EventManager.getInstance().StartListening("仿生人状态扫描仪", ScanMachineTrigger);
        EventManager.getInstance().StartListening("投影地图", MapTrigger);
        EventManager.getInstance().StartListening("安星语宿舍", AXYDorTrigger);
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
        EventManager.getInstance().StopListening("培养缸前", AquariumTrigger);
        EventManager.getInstance().StopListening("培养缸", AquariumClickTrigger);
        EventManager.getInstance().StopListening("办公桌前人影", ShadowTrigger);
        EventManager.getInstance().StopListening("办公桌", DeskTrigger);
        EventManager.getInstance().StopListening("空相框", FrameTrigger);
        EventManager.getInstance().StopListening("仿生人状态扫描仪", ScanMachineTrigger);
        EventManager.getInstance().StopListening("投影地图", MapTrigger);
        base.Close();
    }

    public override void LoadSuccess()
    {
        base.LoadSuccess();
        isFirstTime = false;
        history_world = GameObject.Find("历史世界");
        reality_world = GameObject.Find("现实世界");

        SetHistoryWorld(false);
    }

    public void AquariumTrigger()
    {
        if (state == AXYInsideCh1SceneState.BeformAquarium)
        {
            state = AXYInsideCh1SceneState.Aquarium;
            DialogueManager.StartConversation("⾛走到培养缸前 ");
        }
    }

    public void AquariumClickTrigger()
    {
        if (state == AXYInsideCh1SceneState.Aquarium)
        {
            DialogueManager.StartConversation("蒙太奇引导");
            StartCoroutine(StartMontageGame());
        }
    }

    public IEnumerator StartMontageGame()
    {
        Debug.Log("StartMontageGame");
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
        state = AXYInsideCh1SceneState.AfterMontageGame;
        SetHistoryWorld(true);
    }

    public void ShadowTrigger()
    {
        Debug.Log("ShadowTrigger");
        StartCoroutine(ShadowAnimation());
    }

    public IEnumerator ShadowAnimation()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("ShadowAnimation");
        SetHistoryWorld(false);
        DialogueManager.StartConversation("创的消息");
    }

    public void SetHistoryWorld(bool isHistory)
    {
        history_world.gameObject.SetActive(isHistory);
        reality_world.gameObject.SetActive(!isHistory);
    }

    public void DeskTrigger()
    {
        Debug.Log(1);
        DialogueManager.StartConversation("办公桌");
    }

    public void FrameTrigger()
    {
        DialogueManager.StartConversation("空相框");
    }

    public void ScanMachineTrigger()
    {
        DialogueManager.StartConversation("仿生人状态扫描仪");
    }

    public void MapTrigger()
    {
        DialogueManager.StartConversation("投影地图 ");
    }

    public void AXYDorTrigger()
    {
        GameManager.getInstance().ChangeSceneTo(
            GameManager.getInstance().gameObject.AddComponent<AXYDormitoryCh1Scene>(), new Vector3(17.12f, -1.16f, -3));
    }
}
