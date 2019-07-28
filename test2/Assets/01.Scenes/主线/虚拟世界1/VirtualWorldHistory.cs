using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Com.LuisPedroFonseca.ProCamera2D;

public class VirtualWorldHistory : BaseScene 
{
    public bool isT = false;
    public bool ifC = false; // 这里控制外部的特效是否发生改变
    public bool isdooropen = false;
    double calpha = 0;
    GameObject door;
    protected override void Awake()
    {
        base.Awake();
        _name = "VirtualWordlHistory";
        door = door;
        GameObject.Find("粒子门");
        GameObject.Find("粒子门").SetActive(false);
    }


    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        EventManager.getInstance().StartListening("人物残影",PeopleShadowTrigger);
        EventManager.getInstance().StartListening("镜子",MirrorTrigger);
        base.Load(init_pos, isAsync);
    }

    public override void SceneFlow(Operate.Command operate)
    {
        base.SceneFlow(operate);
        if(ifC == true)
        {
            Color mirrorcolor = GameObject.Find("镜子5").GetComponent<SpriteRenderer>().color;
            mirrorcolor.a = (float)calpha;
            calpha += 0.01;
            GameObject.Find("镜子5").GetComponent<SpriteRenderer>().color = mirrorcolor;
        }
        if (calpha > 1)
        {
            showmirror4();
        }
        if (calpha > 1.4)
        {
            showmirror3();
        }
        if (calpha > 1.8)
        {
            showmirror2();
        }
        if (calpha > 2.2)
        {
            showmirror1();
        }
        if(calpha > 3)
        {
           door.SetActive(true);
            GameObject.Find("Main Camera").GetComponent<ProCamera2D>().OffsetX = 0;
            isdooropen = true;
        }
        Debug.Log(GameObject.Find("Armature").transform.position.x);
        if (isdooropen&& GameObject.Find("Armature").transform.position.x<-13)
        {
            
            TestManager.getInstance().ChangeSceneTo(
           TestManager.getInstance().gameObject.AddComponent<VirtuallyworldCh1>(), new Vector3(-113, -4, -3));
        }
    }

    public override void LoadSceneFile(string filename)
    {
        base.LoadSceneFile(filename);
    }

    public override void Close()
    {
        base.Close();
        EventManager.getInstance().StopListening("人物残影", PeopleShadowTrigger);
        EventManager.getInstance().StopListening("镜子", MirrorTrigger);
    }

    public void PeopleShadowTrigger()
    {
        StartCoroutine(PSTrigger());
    }
    private IEnumerator PSTrigger()
    {
        DialogueManager.StartConversation("Z1442残影");
        while(DialogueManager .IsConversationActive )
        {
            yield return null;
        }
        isT = true;
    }

    public void MirrorTrigger()
    {
        if (isT )
        {
            StartCoroutine(MRTrigger());
        }
    }

    private IEnumerator MRTrigger()
    {

        DialogueManager.StartConversation("镜子互动");
        while (DialogueManager .IsConversationActive)
        {
            yield return null;
        }

        


        Debug.Log("Mirror");//双引号写字符，不然会报字符过多，不识别的错
        ifC = true;

    }

    public void showmirror4()
    {
        GameObject.Find("Main Camera").GetComponent<ProCamera2D>().OffsetX = (float)-0.3;
        Color mirrorcolor = GameObject.Find("镜子5").GetComponent<SpriteRenderer>().color;
        mirrorcolor.a = (float)calpha;
        GameObject.Find("镜子4").GetComponent<SpriteRenderer>().color = mirrorcolor;
    
    }
    public void showmirror3()
    {
        GameObject.Find("Main Camera").GetComponent<ProCamera2D>().OffsetX = (float)-0.3;
        Color mirrorcolor = GameObject.Find("镜子5").GetComponent<SpriteRenderer>().color;
        mirrorcolor.a = (float)calpha;

        GameObject.Find("镜子3").GetComponent<SpriteRenderer>().color = mirrorcolor;
  
    }
    public void showmirror2()
    {
        GameObject.Find("Main Camera").GetComponent<ProCamera2D>().OffsetX = (float)-0.3;
        Color mirrorcolor = GameObject.Find("镜子5").GetComponent<SpriteRenderer>().color;
        mirrorcolor.a = (float)calpha;

        GameObject.Find("镜子2").GetComponent<SpriteRenderer>().color = mirrorcolor;

    }
    public void showmirror1()
    {
        GameObject.Find("Main Camera").GetComponent<ProCamera2D>().OffsetX = (float)-0.3;
        Color mirrorcolor = GameObject.Find("镜子5").GetComponent<SpriteRenderer>().color;
        mirrorcolor.a = (float)calpha;
 
        GameObject.Find("镜子1").GetComponent<SpriteRenderer>().color = mirrorcolor;

    }
    public void showmirror0()
    {
        GameObject.Find("Main Camera").GetComponent<ProCamera2D>().OffsetX = (float)-0.3;
        Color mirrorcolor = GameObject.Find("镜子5").GetComponent<SpriteRenderer>().color;
        mirrorcolor.a = (float)calpha;
     
        GameObject.Find("镜子0").GetComponent<SpriteRenderer>().color = mirrorcolor;
    }



}
