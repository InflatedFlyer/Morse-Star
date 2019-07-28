using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFollow : MonoBehaviour
{
    //public float followspeed;
    //public float limitdistance;
    //GameObject MainSprite;
    //// Start is called before the first frame update
    //void Start()
    //{
    //    MainSprite = GameObject.Find("MainSprite");
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Vector3.Distance(MainSprite.transform.position, transform.position) > limitdistance)
    //        follow();
    //}

    //void follow()
    //{
    //    transform.position = Vector3.Lerp(transform.position, MainSprite.transform.position, Time.deltaTime * followspeed);
    //}
    //宠物跟随的目标
    public Transform target;
    public float limitdistance;
    public string state;
    private void Start()
    {
        state = "state0";
        target = GameObject.Find("MainSprite").transform;
    }
    void LateUpdate()
    {
        if (Vector3.Distance(target.transform.position, transform.position) > limitdistance * 2)
            state = "state1";
        if(state=="state1"&& Vector3.Distance(target.transform.position, transform.position) > limitdistance)
        { 
            //改变宠物的位置，让宠物移动
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
        }
        if (Vector3.Distance(target.transform.position, transform.position) <= limitdistance)
            state = "state0";

    }
}
