using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class VirtuallyworldCh1 : BaseScene
{
    private int[,] blockstate;
    private Vector3[,] blockposition;

    protected override void Awake()
    {
        blockstate = new int[3, 3] { { 1, 2, 3 }, { 4, 5, 9 }, { 7, 8, 6 } };
        blockposition = new Vector3[3, 3];
        blockposition[0, 0] = new Vector3(-4.2f, 2.5f, 0.36f);
        blockposition[0, 1]= new Vector3(-1.7f, 2.5f, 0.36f);
        blockposition[0, 2] = new Vector3(0.8f, 2.5f, 0.36f);
        blockposition[1, 0] = new Vector3(-4.2f, 0f, 0.36f);
        blockposition[1, 1]= new Vector3(-1.7f, 0f, 0.36f);
        blockposition[1, 2] = new Vector3(0.8f, 0f, 0.36f);
        blockposition[2, 0] = new Vector3(-4.2f, -2.5f, 0.36f);
        blockposition[2, 1] = new Vector3(-1.7f, -2.5f, 0.36f);
        blockposition[2, 2] = new Vector3(0.8f, -2.5f, 0.36f);

        base.Awake();
        _name = "Virtuallyworld(Dark)";
        setblockposition();
      

    }

    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
      
      
        EventManager.getInstance().StartListening("黑暗 Hud",DarkHudTrigger);
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
     
    }

    public override void LoadSuccess()
    {
        base.LoadSuccess();
        //StartCoroutine(WakeUpAnimation());
    }

    public void Block1Trigger()
    {
        detectblock(1);
     
    }
    public void Block2Trigger()
    {
        detectblock(2);
 
    }
    public void Block3Trigger()
    {
        detectblock(3);
     
    }
    public void Block4Trigger()
    {
        detectblock(4);
        
    }
    public void Block5Trigger()
    {
        detectblock(5);
       
    }
    public void Block6Trigger()
    {
        detectblock(6);
    }
    public void Block7Trigger()
    {
        detectblock(7);
        
    }
    public void Block8Trigger()
    {
        detectblock(8);
    }
    public void DarkHudTrigger()
    {
        EventManager.getInstance().StartListening("方块1", Block1Trigger);
        EventManager.getInstance().StartListening("方块2", Block2Trigger);
        EventManager.getInstance().StartListening("方块3", Block3Trigger);
        EventManager.getInstance().StartListening("方块4", Block4Trigger);
        EventManager.getInstance().StartListening("方块5", Block5Trigger);
        EventManager.getInstance().StartListening("方块6", Block6Trigger);
        EventManager.getInstance().StartListening("方块7", Block7Trigger);
        EventManager.getInstance().StartListening("方块8", Block8Trigger);
        GameObject.Find("过程控制器").GetComponent<DarkSceneChange>().ifchangetostate1 = true;
        EventManager.getInstance().StopListening("黑暗 Hud", DarkHudTrigger);
        GameObject.Destroy(GameObject.Find("黑暗 Hud"));
    }

    void detectblock(int cblock)
    {
        for (int i = 0; i < 3; i++)
        {
            int findflag = 0;
            for (int j = 0; j < 3; j++)
            {
                if (blockstate[i, j] == cblock)
                {
                    int flag = 0;
                    Debug.Log("find" + cblock);
                    if (i - 1 > -1&&flag ==0)
                    {
                        if (blockstate[i - 1, j] == 9)
                        {
                            int x = blockstate[i, j];
                            blockstate[i, j] = blockstate[i - 1, j];
                            blockstate[i - 1, j] = x;
                            flag = 1;
                            findflag = 1;
                        }
                        setblockposition();
                    }
                    if (i + 1 < 3&&flag ==0)
                    {
                        Debug.Log("in i+1<3");
                        if (blockstate[i + 1, j] == 9)
                        {
                            Debug.Log("in bs[i+1,j] == 9");
                            int x = blockstate[i, j];
                            blockstate[i, j] = blockstate[i + 1, j];
                            blockstate[i + 1, j] = x;
                            flag = 1;
                                findflag = 1;
                        }
                        setblockposition();
                    }
                    if (j - 1 > -1&&flag ==0)
                    {
                        if (blockstate[i, j - 1] == 9)
                        {
                            int x = blockstate[i, j];
                            blockstate[i, j] = blockstate[i, j - 1];
                            blockstate[i, j - 1] = x;
                            flag = 1;
                            findflag = 1;
                        }
                        setblockposition();
                    }
                    if (j + 1 < 3&&flag==0)
                    {
                        if (blockstate[i, j + 1] == 9)
                        {
                            int x = blockstate[i, j];
                            blockstate[i, j] = blockstate[i , j+1];
                            blockstate[i, j + 1] = x;
                            flag = 1;
                            findflag = 1;
                        }
                        setblockposition();
                    }
                    break;

                }
               
            }
            if (findflag == 1)
            {
                break;
            }
        }
    }
    //传入点击了第几个方块，检测四周有没有黑色方块，有则移动

    public void setblockposition()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {

                string blockname = "方块" + blockstate[i, j];

                GameObject currentblock = GameObject.Find(blockname);
                currentblock.transform.position = blockposition[i, j];
            }
        }
        if (isblockcorrect())
        {
            GameObject.Find("过程控制器").GetComponent<DarkSceneChange>().ifchangetostate2 = true;
            EventManager.getInstance().StopListening("方块1", Block1Trigger);
            EventManager.getInstance().StopListening("方块2", Block2Trigger);
            EventManager.getInstance().StopListening("方块3", Block3Trigger);
            EventManager.getInstance().StopListening("方块4", Block4Trigger);
            EventManager.getInstance().StopListening("方块5", Block5Trigger);
            EventManager.getInstance().StopListening("方块6", Block6Trigger);
            EventManager.getInstance().StopListening("方块7", Block7Trigger);
            EventManager.getInstance().StopListening("方块8", Block8Trigger);
            GameObject.Destroy(GameObject.Find("方块1"));
            GameObject.Destroy(GameObject.Find("方块2"));
            GameObject.Destroy(GameObject.Find("方块3"));
            GameObject.Destroy(GameObject.Find("方块4"));
            GameObject.Destroy(GameObject.Find("方块5"));
            GameObject.Destroy(GameObject.Find("方块6"));
            GameObject.Destroy(GameObject.Find("方块7"));
            GameObject.Destroy(GameObject.Find("方块8"));
            EventManager.getInstance().StartListening("蛋糕预留位置", CakeTrigger);
        }
    }
    //设置拼图位置，若所以拼图位置正确，执行接下来的逻辑

    public bool isblockcorrect()
    {
        bool flag = true;
        int count = 1;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                Debug.Log("the first is" + i + "the second is" + j);
                if(blockstate[i,j] != count)
                {
                    flag = false;
                    Debug.Log("false");
                }
                count++;
            }
        }
        return flag;
    }
   //判断拼图位置是否位置正确

    public void CakeTrigger()
    {
        DialogueManager.StartConversation("草莓芝士蛋糕");
        EventManager.getInstance().StartListening("门", DoorTrigger);
    }
    public void DoorTrigger()
    {
        GameManager.getInstance().ChangeSceneTo(
         GameManager.getInstance().gameObject.AddComponent<VirtualWorldMirrorCh1>(), new Vector3(-113, -4, -3));
    }
}
