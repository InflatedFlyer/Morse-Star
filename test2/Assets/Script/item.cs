using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class item : MonoBehaviour
{
    // Start is called before the first frame update
    public string str;
    public GameObject itempanel;
    public Text word;
    [HideInInspector] public gamemanager manager;

    private void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<gamemanager>();
        // / UIManager / MainMenuCanvas / itemword
    }
    void Start()
    {
        //if (itempanel.activeSelf)
        //{
        //    itempanel.SetActive(false);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            itempanel.SetActive(false);
        }
    }

    public void itemset()
    {
        itempanel.SetActive(true);
        word.text = manager.itemword[str];
    }
}
