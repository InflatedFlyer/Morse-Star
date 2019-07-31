using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "to2")]
public class to2 : ScriptableObject
{
    // Start is called before the first frame update
    private gamemanager gamemanager;

    void Start()
    {
        gamemanager = GameObject.Find("GameManager").GetComponent<gamemanager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void seth(int x)
    {
        gamemanager.Hungar = x;
    }
    public void addh(int x)
    {
        gamemanager.Hungar += x;
    }
    public void setm(int x)
    {
        gamemanager.Money = x;
    }
    public void addm(int x)
    {
        gamemanager.Money += x;
    }
    public void setc(int x)
    {
        gamemanager.Charm = x;
    }
    public void addc(int x)
    {
        gamemanager.Charm += x;
    }
    public void setw(int x)
    {
        gamemanager.Word = x;
    }
    public void addw(int x)
    {
        gamemanager.Word += x;
    }
}
