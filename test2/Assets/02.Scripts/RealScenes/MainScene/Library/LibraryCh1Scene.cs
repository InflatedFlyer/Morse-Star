using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class LibraryCh1Scene : BaseScene
{
    private Vector2 LeftFirst=new Vector2(-27,-17);
    private Vector2 LeftMiddle=new Vector2(-11.6f,-9.7f);
    private Vector2 LeftSecond=new Vector2(-25f,-2.4f);
    private Vector2 RightFirst = new Vector2(33f, -17.3f);
    private Vector2 RightMiddle = new Vector2(17.4f, -9.8f);
    private Vector2 RightSecond = new Vector2(30f, -2.4f);

    private GameObject history_world;
    private GameObject reality_world;

    private bool[] history_triggers=new bool[] { false, false };
    protected override void Awake()
    {
        base.Awake();
        _name = "Library 1";
    }

    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        EventManager.getInstance().StartListening("一楼电脑", ComputerTrigger);
        EventManager.getInstance().StartListening("二楼的球", BallTrigger);
        EventManager.getInstance().StartListening("二楼光点", LightPointTrigger);
        EventManager.getInstance().StartListening("主街", MainStreetTrigger);
        EventManager.getInstance().StartListening("实验室走廊", LabPassTrigger);

        EventManager.getInstance().StartListening("二楼两人残影", SecondFloorShadowTrigger);
        EventManager.getInstance().StartListening("门口三个人的残影", ThreeShadowBesideDoorTrigger);

        EventManager.getInstance().StartListening("下二右", RightDownStairsSecond);
        EventManager.getInstance().StartListening("下二左", LeftDownStairsSecond);
        EventManager.getInstance().StartListening("下中右", RightDownStairsMiddle);
        EventManager.getInstance().StartListening("下中左", LeftDownStairsMiddle);
        EventManager.getInstance().StartListening("上二右", RightUpStairsFirst);
        EventManager.getInstance().StartListening("上二左", LeftUpStairsFirst);
        EventManager.getInstance().StartListening("上中右", RightUpStairsMiddle);
        EventManager.getInstance().StartListening("上中左", LeftUpStairsMiddle);

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
        EventManager.getInstance().StopListening("一楼电脑", ComputerTrigger);
        EventManager.getInstance().StopListening("二楼的球", BallTrigger);
        EventManager.getInstance().StopListening("二楼光点", LightPointTrigger);
        EventManager.getInstance().StopListening("主街", MainStreetTrigger);
        EventManager.getInstance().StopListening("实验室走廊", LabPassTrigger);

        EventManager.getInstance().StopListening("门口三个人的残影", ThreeShadowBesideDoorTrigger);
        EventManager.getInstance().StopListening("二楼两人残影", SecondFloorShadowTrigger);

        EventManager.getInstance().StopListening("下二右", RightDownStairsSecond);
        EventManager.getInstance().StopListening("下二左", LeftDownStairsSecond);
        EventManager.getInstance().StopListening("下中右", RightDownStairsMiddle);
        EventManager.getInstance().StopListening("下中左", LeftDownStairsMiddle);
        EventManager.getInstance().StopListening("上二右", RightUpStairsFirst);
        EventManager.getInstance().StopListening("上二左", LeftUpStairsFirst);
        EventManager.getInstance().StopListening("上中右", RightUpStairsMiddle);
        EventManager.getInstance().StopListening("上中左", LeftUpStairsMiddle);
        base.Close();
    }

    public override void LoadSuccess()
    {
        base.LoadSuccess();
        DialogueManager.StartConversation("图书馆引导");
        history_world = GameObject.Find("历史世界");
        reality_world = GameObject.Find("现实世界");
        SetHistoryWorld(false);

    }

    public void ComputerTrigger()
    {
        DialogueManager.StartConversation("现实丶一楼电脑");
    }

    public void BallTrigger()
    {
        DialogueManager.StartConversation("现实丶二楼的球");
    }
    

    #region 上楼下楼
    public void LeftUpStairsFirst()
    {
        StartCoroutine(LimingMoveOnStairs(LeftFirst,LeftMiddle));
    }

    public void LeftUpStairsMiddle()
    {
        StartCoroutine(LimingMoveOnStairs(LeftMiddle,LeftSecond));
    }

    public void RightUpStairsFirst()
    {
        StartCoroutine(LimingMoveOnStairs(RightFirst,RightMiddle));
    }

    public void RightUpStairsMiddle()
    {
        StartCoroutine(LimingMoveOnStairs(RightMiddle,RightSecond));
    }

    public void LeftDownStairsSecond()
    {
        StartCoroutine(LimingMoveOnStairs(LeftSecond,LeftMiddle));
    }

    public void LeftDownStairsMiddle()
    {
        StartCoroutine(LimingMoveOnStairs(LeftMiddle,LeftFirst));

    }

    public void RightDownStairsSecond()
    {
        StartCoroutine(LimingMoveOnStairs(RightSecond, RightMiddle));
    }

    public void RightDownStairsMiddle()
    {
        StartCoroutine(LimingMoveOnStairs(RightMiddle, RightFirst));
    }

    public IEnumerator LimingMoveOnStairs(Vector2 start,Vector2 end)
    {   
        liming.Walk(start);
        liming.LoseControl();
        yield return new WaitForSeconds(Vector2.Distance(start, liming.transform.position) /2);
        liming.GetControl();
        liming.Walk(end);
        liming.LoseControl();
        yield return new WaitForSeconds(Vector2.Distance(end, liming.transform.position) / 2);
        liming.GetControl();
    }
    #endregion

    public void LightPointTrigger()
    {
        Debug.Log(1);
        DialogueManager.StartConversation("蒙太奇引导");
        StartCoroutine(StartMontageGame());
    }
    
    public void SetHistoryWorld(bool isHistory)
    {
        history_world.gameObject.SetActive(isHistory);
        reality_world.gameObject.SetActive(!isHistory);
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
        SetHistoryWorld(true);
    }

    public void SecondFloorShadowTrigger()
    {

        StartCoroutine(StartConversation("CG", 0));
    }
    public void ThreeShadowBesideDoorTrigger()
    {
        StartCoroutine(StartConversation("历史丶三人残影",1));
    }

    public IEnumerator StartConversation(string title, int shadow_index)
    {
        DialogueManager.StartConversation(title);
        while (DialogueManager.IsConversationActive)
        {
            yield return null;
        }
        history_triggers[shadow_index] = true;
        CheckState();
    }

    public void CheckState()
    {
        if (history_triggers[0] && history_triggers[1])
        {
            SetHistoryWorld(false);
        }
    }

    public void MainStreetTrigger()
    {
        GameManager.getInstance().ChangeSceneTo(
            GameManager.getInstance().gameObject.AddComponent<MainStreet1Ch1Scene>(), new Vector3(137.7f, -1.2f, -3));
    }

    public void LabPassTrigger()
    {
        GameManager.getInstance().ChangeSceneTo(
           GameManager.getInstance().gameObject.AddComponent<LabPassCh1Scene>(), new Vector3(-75.91f, -1.95f, -3));
    }
}
