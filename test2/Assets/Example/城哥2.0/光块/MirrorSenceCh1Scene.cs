using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorSenceCh1Scene : BaseScene
{

    GameObject mirrorliming;
    float mirrorlimingx;
    GameObject mlm1;
    GameObject mlm2;
    GameObject mlm3;
    bool ismlmshow = false;
    Color mlmcolor = new Color(1, 1, 1, 0);
    double mx, my, mz;

    //镜中人的transform
    Transform mirrorx;
    //黎茗的transfrom
    Transform limingx;

    protected override void Awake()
    {

        base.Awake();
        _name = "MirrorScene";
        mirrorliming = GameObject.Find("mirrorliming");
        mirrorlimingx = mirrorliming.transform.position.x;
        mlm1 = GameObject.Find("后腿（右侧翻转在前");
        mlm2 = GameObject.Find("手后");
        mlm3 = GameObject.Find("袖子前");
        mlm1.GetComponent<MeshRenderer>().material.color = mlmcolor;
        mlm2.GetComponent<MeshRenderer>().material.color = mlmcolor;
        mlm3.GetComponent<MeshRenderer>().material.color = mlmcolor;
        mirrorliming.SetActive(true);
        
    }
    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        base.Load(init_pos, isAsync);
    }




    public override void SceneFlow(Operate.Command operate)
    {
        //父类构造函数重写
        base.SceneFlow(operate);
        //镜中人的transform
        mirrorx = mirrorliming.transform;
        //黎茗的transfrom
        limingx = GameObject.Find("黎茗").transform;
        Debug.Log(mirrorlimingx);

        //当黎茗的x到达镜子的时候，使镜中人可见
        if (limingx.position.x < mirrorlimingx)
        {
            mirrorliming.SetActive(true);
            ismlmshow = true;
        }

        //调整镜中人的位置
        
        mz = -5;
        Vector3 mv = new Vector3(mirrorx.position.x, mirrorx.position.y, (float)mz);
        mirrorliming.transform.position = mv;

        //如果镜中人的X大于黎明的X
        //加大黎明的X
        if (mirrorx.position.x >= limingx.position.x)
        {
            double mx = limingx.position.x+1;
            Vector3 v = new Vector3((float)mx, mirrorx.position.y, mirrorx.position.z);
            GameObject.Find("mirrorliming").transform.position = v;

        }

        if (ismlmshow)
        {
            if (mlmcolor.a < 0.5)
            {
                mlmcolor.a += 0.01f;
            }
            mlm1.GetComponent<MeshRenderer>().material.color = mlmcolor;
            mlm2.GetComponent<MeshRenderer>().material.color = mlmcolor;
            mlm3.GetComponent<MeshRenderer>().material.color = mlmcolor;
        }


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
        liming.Walk(new Vector3(-50, -1.799f, -3));
        liming.LoseControl();
        
    }
}
