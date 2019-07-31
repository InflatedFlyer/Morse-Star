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
    public int Charm;
    public int Money;
    public int Hungar;
    public int Word;
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
        itemword = new Dictionary<string, string>();
        itemword.Add("手机", "这是一个手机");
        itemword.Add("钱包", "这是一个钱包");
        itemword.Add("戒指", "这是一个戒指");
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
    }
    public void lossitem(string x)
    {
        int s;
        s = itemd[x];
        itemflag[s] = false;
    }
}
