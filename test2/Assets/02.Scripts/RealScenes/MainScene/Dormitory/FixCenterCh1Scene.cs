using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class FixCenterCh1Scene : BaseScene
{
    public bool hastrans = false;
    private static bool isWatchScreen=false;
    private float temp = 0;
    private float S_time = 0;

    public static bool isFirstTime = true;

    protected override void Awake()
    {
        
        base.Awake();
        _name = "Dormitory(Day)";

    }

    public override void Load(Vector3 init_pos,bool isAsync = true)
    {
        EventManager.getInstance().StartListening("显示屏", ScreenTrigger);
        EventManager.getInstance().StartListening("门", DoorTrigger);
        EventManager.getInstance().StartListening("床", BedTrigger);
        EventManager.getInstance().StartListening("机械臂", MachineArmTrigger);
        base.Load(init_pos,isAsync);
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
        base.Close();
        EventManager.getInstance().StopListening("显示屏", ScreenTrigger);
        EventManager.getInstance().StopListening("门", DoorTrigger);
        EventManager.getInstance().StopListening("床", BedTrigger);
        EventManager.getInstance().StopListening("机械臂", MachineArmTrigger);
    }

    public override void LoadSuccess()
    {
        base.LoadSuccess();
        if (isFirstTime)
        {
            StartCoroutine(WakeUpAnimation());
            isFirstTime = false;
        }
    }

    public IEnumerator WakeUpAnimation()
    {
        //播放在床上动画
        liming.LoseControl();
        liming.target_pos = new Vector3(2.6f, -1.42f, -3);
        liming.transform.position = new Vector3(2.6f, -1.33f, -3);
        liming.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);

       
        liming.UnityArmature.animation.Play("起床全过程", 1);
        liming.UnityArmature.animation.timeScale = 1f;
        yield return new WaitForSeconds(liming.UnityArmature.animation.lastAnimationState.totalTime/2);
        //liming.UnityArmature.animation.Stop ();
        StartCoroutine(TransformP());
       
        //播放对话

        DialogueManager.StartConversation("黎茗苏醒");
        yield return new WaitForSeconds(liming.UnityArmature.animation.lastAnimationState.totalTime /2);

        ////对话结束后播放起床动画
        //liming.UnityArmature.animation.Play("起床全过程", 1);
        //liming.UnityArmature.animation.timeScale = 1f;
        //yield return new WaitForSeconds(6.5f);




        //liming.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        //liming.target_pos = new Vector3(2.6f, -1.9f, -3);
        //liming.transform.position = new Vector3(2.6f, -1.9f, -3);
        liming.UnityArmature.animation.Play("侧面懒腰",1);
        liming.UnityArmature.animation.timeScale = 0.5f;
        yield return new WaitForSeconds(liming.UnityArmature.animation.lastAnimationState.totalTime*2);

        liming.GetControl();

    }

    public void ScreenTrigger()
    {
        StartCoroutine(TalkToScreen());
    }

    private IEnumerator TalkToScreen()
    {
       
            DialogueManager.StartConversation("显示屏");
            while (DialogueManager.IsConversationActive)
            {
                yield return null;
            }
            isWatchScreen = true;
      
    }

    public void DoorTrigger()
    {
        if (isWatchScreen)
        {
            StartCoroutine(WaitDoorDialogueEnd());
        }
    }

    private IEnumerator WaitDoorDialogueEnd()
    {

        while (DialogueManager.IsConversationActive)
        {
            yield return null;
        }
        ChangeToMainStreet1Ch1Scene();
    }

    public void BedTrigger()
    {
        DialogueManager.StartConversation("床");
    }

    public void MachineArmTrigger()
    {
        DialogueManager.StartConversation("机械臂");
    }

    public IEnumerator TransformP()
    {
        S_time = Vector3.Distance(liming.transform.localPosition, new Vector3(2.6f, -1.8f, -3f)) / 0.1f;
        //yield return new WaitForSeconds(2f);
        while (liming.transform.localPosition != new Vector3(2.6f, -1.8f, -3f) && !hastrans)

        {
            liming.target_pos = Vector3.Lerp(liming.transform.localPosition, new Vector3(2.6f, -1.8f, -3f), Time.deltaTime / 5f);

            temp += Time.deltaTime;
            yield return null;
            if (Vector3.Distance(liming.transform.localPosition, new Vector3(2.6f, -1.8f, -3f)) < 0.05f)
            {
                hastrans = true;
            }
        }
    }

    public void ChangeToMainStreet1Ch1Scene()
    {
        GameManager.getInstance().ChangeSceneTo(
            GameManager.getInstance().gameObject.AddComponent<MainStreet1Ch1Scene>(),new Vector3(-15.42f, -1.2f, -3));
    }

    public void ChangeToDormitoryHistoryScene()
    {
        GameManager .getInstance ().ChangeSceneTo (
        GameManager .getInstance ().gameObject .AddComponent <VirtuallyworldCh1 >(),new Vector3(2f, 0f, -1f));
    }
   
}
