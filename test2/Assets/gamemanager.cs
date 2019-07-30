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
    public int charm;
    public int money;
    public int hungar; 
    public Dictionary<string,int> item;
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
        item.Add("笔", 0);
        item.Add("手电筒", 0);
        item.Add("绳子", 0);
        item.Add("钥匙", 0);
        item.Add("钱包", 0);
        item.Add("手机", 0);
        item.Add("晕车药", 0);
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
        hungar = x;
    }
    public void addh(int x)
    {
        hungar += x;
    }
    public void setm(int x)
    {
        money = x;
    }
    public void addm(int x)
    {
        money += x;
    }
    public void setc(int x)
    {
        charm = x;
    }
    public void addc(int x)
    {
        charm += x;
    }
}
