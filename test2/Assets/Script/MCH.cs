using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MCH")]
public class MCH : MonoBehaviour
{
    // Start is called before the first frame update
    private gamemanager gamemanager;
    private int charm;
    private int money;
    private int hungar;
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
        gamemanager.hungar = x;
    }
    public void addh(int x)
    {
        gamemanager.hungar += x;
    }
    public void setm(int x)
    {
        gamemanager.money = x;
    }
    public void addm(int x)
    {
        gamemanager.money += x;
    }
    public void setc(int x)
    {
        gamemanager.charm = x;
    }
    public void addc(int x)
    {
        gamemanager.charm += x;
    }
}
