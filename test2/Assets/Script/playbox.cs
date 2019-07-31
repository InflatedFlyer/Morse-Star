using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playbox : MonoBehaviour
{
    // Start is called before the first frame update
    public gamemanager manager;
    public GameObject[] itemx;
    public Button k;
    Vector3 zero,right,down,ans;
    private void Awake()
    {
        zero  = itemx[0].transform.position;
        right = itemx[1].transform.position - itemx[0].transform.position;
        down = itemx[5].transform.position - itemx[0].transform.position;
        for (int i = 0; i < 15; i++)
        {
            itemx[i].SetActive(false);
        }
    }
    void Start()
    {
        sort();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sort()
    {
        int num = 0;
        for (int i = 0; i <15; i++)
        {
            if (manager.itemflag[i])
            {
                itemx[i].SetActive(true);
                num++;
                int y = num % 5;
                int x = ((num - y) / 5) + 1;
                if (y == 0)
                {
                    y = 5;
                    x = x - 1;
                }
                
                ans= zero+(x-1)* down + (y-1)* right;
                itemx[i].transform.position = ans;
            }
        }
    }
}
