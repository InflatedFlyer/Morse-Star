using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class MainStreet1Ch1Scene : BaseScene
{

    public static bool isFirstTime = true;

    protected override void Awake()
    {
        Cursor.SetCursor(default, new Vector2(100f, 100f), CursorMode.Auto);
        base.Awake();
        _name = "MainStreet2";

    }

    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        EventManager.getInstance().StartListening("蛋糕店", CakeStoreTrigger);
        EventManager.getInstance().StartListening("女学生们", GirlsTrigger);
        EventManager.getInstance().StartListening("花坛", GardenTrigger);
        EventManager.getInstance().StartListening("路人奶奶", OldWomanTrigger);
        EventManager.getInstance().StartListening("咖啡厅", CoffeeBarTrigger);
        EventManager.getInstance().StartListening("图书馆", LibraryTrigger);
        EventManager.getInstance().StartListening("安星语家", AXYDORTrigger);
        EventManager.getInstance().StartListening("黎茗家", LiMingDorTrigger);
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
        base.Close();
        EventManager.getInstance().StopListening("蛋糕店", CakeStoreTrigger);
        EventManager.getInstance().StopListening("女学生们", GirlsTrigger);
        EventManager.getInstance().StopListening("花坛", GardenTrigger);
        EventManager.getInstance().StopListening("路人奶奶", OldWomanTrigger);
        EventManager.getInstance().StopListening("黎茗家", LiMingDorTrigger );
        EventManager.getInstance().StopListening("安星语家", AXYDORTrigger);
        EventManager.getInstance().StopListening("咖啡厅", CoffeeBarTrigger);
        EventManager.getInstance().StopListening("图书馆", LibraryTrigger);
        DialogueManager.StopConversation();
    }

    public override void LoadSuccess()
    {
        base.LoadSuccess();
        if (isFirstTime)
        {
            isFirstTime = false;
            DialogueManager.StartConversation("引导");
        }
    }
    
    public void GardenTrigger()
    {
        //DialogueManager.StartConversation("床");
    }

    public void CakeStoreTrigger()
    {
        DialogueManager.StartConversation("蛋糕店");
    }

    public void OldWomanTrigger()
    {
        liming.LoseControl();
       
        StartCoroutine(OldWoman());

       
    }

    public IEnumerator OldWoman()
    {
        DialogueManager.StartConversation("路人奶奶");
        while (DialogueManager .IsConversationActive )
        {
            yield return null;
        }
        liming.GetControl();
    }

    public void GirlsTrigger()
    {
        //DialogueManager.StartConversation("床");
    }

    public void CoffeeBarTrigger()
    {
        ChangeSceneToBarCh1Scene();
    }

    public void LiMingDorTrigger()
    {
        ChangeSceneToLiMingDor();
    }

    public void AXYDORTrigger()
    {
        ChangeSceneToAXYDor();
    }

    public void LibraryTrigger()
    {
        StartCoroutine(ChangeSceneToLibraryCh1Scene());
    }

    public IEnumerator ChangeSceneToLibraryCh1Scene()
    {
        Vector2 start = new Vector2(140.26f, -2.7f), end = new Vector2(146.66f, -0.1f);
        liming.Walk(start);
        liming.LoseControl();
        yield return new WaitForSeconds(Vector2.Distance(start, liming.transform.position) / 2);
        liming.GetControl();
        liming.Walk(end);
        liming.LoseControl();
        yield return new WaitForSeconds(Vector2.Distance(end, liming.transform.position) / 2);
        liming.GetControl();
        
        GameManager.getInstance().ChangeSceneTo(
          GameManager.getInstance().gameObject.AddComponent<LibraryCh1Scene>(), new Vector3(2.85f, -17.54f, -4f));
    }
    
    public void ChangeSceneToBarCh1Scene()
    {
        GameManager.getInstance().ChangeSceneTo(
            GameManager.getInstance().gameObject.AddComponent<BarCh1Scene>(), new Vector3(-32, -1.6f, -4.5f));
    }
    public void ChangeSceneToLiMingDor()
    {
        GameManager.getInstance().ChangeSceneTo(
            GameManager.getInstance().gameObject.AddComponent<FixCenterCh1Scene >(), new Vector3(13.5f,-1.9f,0));
    }
    public void ChangeSceneToAXYDor()
    {
        GameManager.getInstance().ChangeSceneTo(
            GameManager.getInstance().gameObject.AddComponent<AXYDormitoryCh1Scene>(), new Vector3(-18.1f, -1.16f, -3));
    }
}
