using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrailEffect : MonoBehaviour {

    //尾迹对象
    private Transform[] son_trails;
    private Vector3[] start_positions;
    
    //移动速度
    private float base_speed=1f;
    //尾迹宽度
    private float trail_width = 1f;

    private float start_time;
    private float last_time;

    //移动路径
    protected Vector3[][] paths;
    protected RectTransform parent_rect
    {
        get
        {
            return parent.GetComponent<RectTransform>();
        }
    }
    protected float parent_width
    {
        get
        {
            return parent_rect.rect.width
            * parent_rect.lossyScale.x;
        }
    }
    protected float parent_height
    {
        get
        {
            return parent_rect.rect.height
            * parent_rect.lossyScale.y;
        }
    }

    //尾迹对象移动的目标点
    private int[] points_count;

    private bool isInit = false;
    private enum EffectState
    {
        Ready,
        Appearing,
        Appeared,
        Disappearing
    }
    private EffectState state;

    private Transform parent
    {
        get
        {
            return transform.parent;
        }
    }
    private float width_bargin = 0.5f;
    private float height_bargin= 0.5f;
    
    private bool isStopped
    {
        get
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                if (points_count[i] < paths[i].Length)
                {
                    return false;
                }
            }
            return true;
        }
    }

    // Use this for initialization
	public virtual void Start () {
        StartCoroutine(StartCheckInit());
    }

    // Update is called once per frame
    public virtual void Update () {
	    
    }

    private void OnEnable()
    {
        if (isInit)
        {
            StartDialog();
        }
    }

    private void OnDisable()
    {
        if (isInit)
        {
            CloseImediately();
        }
    }
    public IEnumerator StartCheckInit()
    {
        while(!isInit)
        {
            if(parent_width!=0&&parent_height!=0)
            {
                Init();
            }
            yield return null;
        }
        
    }

    public void Init()
    {
        isInit = true;

        state = EffectState.Ready;

        InitPaths();
        PrintPath();

        //初始化尾迹
        son_trails = new Transform[transform.childCount];
        start_positions = new Vector3[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            son_trails[i] = transform.GetChild(i);
            son_trails[i].position = paths[i][0];

            start_positions[i] = paths[i][0];

            son_trails[i].GetComponent<TrailRenderer>().startWidth = trail_width;
            son_trails[i].GetComponent<TrailRenderer>().endWidth = trail_width;
        }

        //初始化目标点
        points_count = new int[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            points_count[i] = 0;
        }
    }

    public virtual void InitPaths()
    {
        //初始化移动路径
        paths = new Vector3[transform.childCount][];
        
        
        
    }

    private void StartDialog()
    {
        if(state!=EffectState.Ready)
        {
            CloseImediately();
        }
        StartCoroutine(TrailMove());
    }

    private IEnumerator TrailMove()
    {
        yield return new WaitForSeconds(0.1f);

        start_time = Time.time;

        for (int i = 0; i < transform.childCount; i++)
        {
            son_trails[i].GetComponent<TrailRenderer>().time = 1000f;
        }

        state = EffectState.Appearing;
        while (state == EffectState.Appearing)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (points_count[i] < paths[i].Length)
                {
                    Vector3 direct = paths[i][points_count[i]] - son_trails[i].position;
                    float delta_time = Time.deltaTime;

                    Vector3 temp_pos = son_trails[i].position + direct.normalized * base_speed * delta_time;

                    if (Vector3.Distance(temp_pos, paths[i][points_count[i]]) < 0.1f)
                    {
                        son_trails[i].position = paths[i][points_count[i]++];
                    }
                    else
                    {
                        son_trails[i].Translate(direct.normalized * base_speed * delta_time);
                    }
                }
                else if(isStopped)
                {
                    last_time = Time.time - start_time;
                    state = EffectState.Appeared;
                }
            }
            yield return null;
        }
    }

    private void CloseDialog()
    {
        if(state== EffectState.Appeared)
        {
            StartCoroutine(TrailDisappear());
        }
        else
        {
            CloseImediately();
        }
    }

    private IEnumerator TrailDisappear()
    {
        state = EffectState.Disappearing;
        for (int i = 0; i < transform.childCount; i++)
        {
            son_trails[i].GetComponent<TrailRenderer>().time = Time.time - start_time;
        }

        yield return new WaitForSeconds(last_time);

        CloseImediately();
        yield return null;
    }

    private void CloseImediately()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            son_trails[i].GetComponent<TrailRenderer>().time = 0;
            son_trails[i].GetComponent<TrailRenderer>().enabled = false;

            son_trails[i].position = start_positions[i];
            points_count[i] = 0;

            son_trails[i].GetComponent<TrailRenderer>().enabled = true;
        }
        state = EffectState.Ready;
    }

    private void PrintPath()
    {
        for(int i=0;i<2;i++)
        {
            for(int j = 0; j < paths[i].Length; j++)
            {
                Debug.Log(paths[i][j]);
            }
        }
    }
}
