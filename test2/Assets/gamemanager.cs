using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static gamemanager gameInstance;
    private SceneFadeInOut fadescene;
    public playbox playbox;
    [HideInInspector] public int Charm;
    [HideInInspector] public int Money;
    [HideInInspector] public int Hungar;
    [HideInInspector] public int Word;
    //[HideInInspector] public Dictionary<int, string> order;
    [HideInInspector] public Dictionary<string,int> itemd;
    [HideInInspector] public Dictionary<string, string> itemword;
    public bool[] itemflag;
    void Awake()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
        }
        else if (gameInstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //order = new Dictionary<int, string>();
        //order.Add(0,"手机");
        //order.Add(1,"钱包");
        //order.Add(2,"戒指");
        

        itemd = new Dictionary<string, int>();
        itemd.Add("手机", 0);
        itemd.Add("钱包", 1);
        itemd.Add("戒指", 2);
        itemd.Add("吐司", 3);
        itemd.Add("手杖", 4);
        itemd.Add("巧克力", 5);
        itemd.Add("小玩偶", 6);
        itemd.Add("绳子", 7);
        itemd.Add("首饰盒", 8);
        itemword = new Dictionary<string, string>();
        itemword.Add("手机", "这是一个手机");
        itemword.Add("钱包", "这是一个钱包");
        itemword.Add("戒指", "这是一个戒指");
        itemword.Add("吐司", "常见的甜点，在家里放了一段时间了");
        itemword.Add("手杖", "");
        itemword.Add("巧克力", "");
        itemword.Add("小玩偶", "");
        itemword.Add("绳子", "");
        itemword.Add("首饰盒", "");
        //item.Add("钥匙", 0);
        //item.Add("笔", 0);
        //item.Add("手机", 0);
        //item.Add("晕车药", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tosence(int x)
    {
        //SceneManager.LoadScene(x);
        fadescene = GameObject.Find("RawImage").GetComponent<SceneFadeInOut>();
        fadescene.scenenum = x;
        fadescene.sceneEnding = true;
    }
    public void seth(int x)
    {
        Hungar = x;
    }
    public void addh(int x)
    {
        Hungar += x;
    }
    public void setm(int x)
    {
        Money = x;
    }
    public void addm(int x)
    {
        Money += x;
    }
    public void setc(int x)
    {
        Charm = x;
    }
    public void addc(int x)
    {
        Charm += x;
    }
    public void setw(int x)
    {
        Word = x;
    }
    public void addw(int x)
    {
        Word += x;
    }
    public void additem(string x)
    {
        int s;
        s = itemd[x];
        itemflag[s] = true;
        playbox.sort();
    }
    public void lossitem(string x)
    {
        int s;
        s = itemd[x];
        itemflag[s] = false;
        playbox.sort();
    }
}
