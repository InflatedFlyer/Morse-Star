using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigFish : MonoBehaviour {
    private Vector3[] paths;
    private int i = 1;
    public float speed=1;
	// Use this for initialization
	void Start () {
        paths=iTweenPath.GetPath("FishRoute");

        transform.position = paths[0];
        FishMove();
        //Hashtable args=new Hashtable();

        //args.Add("path", paths);
        //args.Add("easeType", iTween.EaseType.linear);
        //args.Add("speed", 5f);
        //args.Add("axis", "z");
        //args.Add("orienttopath", true);
        //iTween.MoveTo(gameObject, args);
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void RotateObject()
    {
        int i = 0;
        while(paths[i].x<transform.position.x)
        {
            i++;
        }
        if(i<=0||i>=paths.Length)
        {
            return;
        }
        else
        {
            Vector3 target = Vector3.Lerp(paths[i] , paths[i - 1],5.0f);
            Vector3 delta = target - transform.position;
            transform.rotation=Quaternion.Euler(0,0,Mathf.Rad2Deg*(Mathf.Atan(delta.y/delta.x)));
        }
    }

    private void FishMove()
    {
        Hashtable args = new Hashtable();
        if (i < paths.Length)
        {


            Vector3 go = paths[i] - paths[i - 1];//获取itweenPath两点之间的向量差
            Vector3 direct=new Vector3(1,Mathf.Tan(transform.rotation.eulerAngles.z*Mathf.Deg2Rad),0);
            Vector3 v = Quaternion.FromToRotation(direct, paths[i] - paths[i - 1]).eulerAngles;//效果：跟SetFromToRotation差不多，区别是可以返回一个Quaternion。通常用来让transform的一个轴向(例如 y轴)与toDirection在世界坐标中同步。
            v.x = 0;
            v.y = 0;


            iTween.RotateTo(gameObject, iTween.Hash("rotation", v, "time", 3));//旋转游戏物体到指定的角度

            args.Add("speed", speed );
            args.Add("position", paths[i++]);//将path[i]赋值到position 中 ，在进行自加
            args.Add("easetype", iTween.EaseType.linear);
            args.Add("oncomplete", "FishMove");//结束时再执行本函数
            args.Add("oncompletetarget", gameObject);//结束时的目标还为本物体
            iTween.MoveTo(gameObject, args);
        }
    }
}
