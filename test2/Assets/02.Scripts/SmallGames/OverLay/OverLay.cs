using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using PathologicalGames;
using Newtonsoft.Json;

[System.Serializable]
public class ShapeData
{
    public List<Triangle> tris; //最左边的点为起始点2
}


public class OverLay : MonoBehaviour {
    
    private List<ShapeData> init_shapes;//所有起始形状的设置
    private List<Shapes> raw_shapes;//初始形状

    private List<ShapeData> end_shapes;//终止状态
    private List<Shapes> end_shape;//终止状态

    //[HideInInspector]
    //public List<Shapes> cross_shapes;//跟其他图形相交的图形

    public static SpawnPool pool;//对象池对象

    public GameObject overlayMenu;//菜单对象
    public GameObject pointsImg;//点阵对象
    private float pointsImg_width;
    private float pointsImg_height;

    private bool isStart = false;//是否开始游戏了
    private bool isMove
    {
        get
        {
            foreach(Shapes raw_shape in raw_shapes)
            {
                if(raw_shape.isDrag||!raw_shape.isAdjust)
                {
                    return true;
                }
            }
            return false;
        }
    }//是否在拖拽

    //起始状态和终止状态的json文件
    private string init_state = "Assets/12.Texts/Overlay/锁/开始图.json";
    private string end_state = "Assets/12.Texts/Overlay/锁/结束图.json";

    
	// Use this for initialization
	void Start () {
        GameStart();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetMouseButtonDown(0)&&isStart)
        {
            foreach(Shapes s in raw_shapes)
            {
                if(s.IsInShape(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
                {
                    
                    StartCoroutine(s.MouseDown());
                    StartCoroutine(s.UpdateShapes());
                    //StartCoroutine(UpdateCrossShapes());
                    break;
                }
            }
        }

        if(isStart&&CheckGraph())
        {
            Invoke("GameOver", 1f);
        }
	}

    public IEnumerator GameStart()
    {

        overlayMenu.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        pointsImg_width = pointsImg.GetComponent<RectTransform>().rect.size.x * pointsImg.transform.lossyScale.x-0.3f;
        pointsImg_height = pointsImg.GetComponent<RectTransform>().rect.size.y * pointsImg.transform.lossyScale.y-0.3f;
        
        pool = GameObject.Find("ShapePool").GetComponent<SpawnPool>();

        LoadInitState();
        LoadEndState();

        DrawShapes();

        //Debug.Log(raw_shapes[0].transform.GetSiblingIndex());
        //raw_shapes[0].Print();

        isStart = true;
    }

    public void GameOver()
    {
        gameObject.SetActive(false);
    }

    public void LoadInitState()
    {
        init_shapes = new List<ShapeData>();
        raw_shapes = new List<Shapes>();

        if (File.Exists(init_state))
        {
            string json = File.ReadAllText(init_state);
            Newtonsoft.Json.Linq.JArray input = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(json);
            foreach (Newtonsoft.Json.Linq.JArray shape in input)
            {
                ShapeData s = new ShapeData();
                s.tris = new List<Triangle>();
                foreach (Newtonsoft.Json.Linq.JArray tri in shape)
                {
                    List<Vector3> points = new List<Vector3>();
                    points.Add(new Vector2(tri.Value<int>(0), tri.Value<int>(1)));
                    points.Add(new Vector2(tri.Value<int>(2), tri.Value<int>(3)));
                    points.Add(new Vector2(tri.Value<int>(4), tri.Value<int>(5)));
                    s.tris.Add(new Triangle(points[0], points[1], points[2]));
                }
                init_shapes.Add(s);
            }
            foreach (ShapeData sd in init_shapes)
            {
                Triangle[] tri_vertice = new Triangle[sd.tris.Count];
                for (int i = 0; i < sd.tris.Count; i++)
                {
                    List<Vector3> p_list = new List<Vector3>();
                    p_list.Add(GetPos(sd.tris[i].p1));
                    p_list.Add(GetPos(sd.tris[i].p2));
                    p_list.Add(GetPos(sd.tris[i].p3));
                    PointOperate.clockSort(ref p_list);
                    tri_vertice[i] = new Triangle(p_list[0], p_list[1], p_list[2]);
                }
                Shapes new_shape = NewShape(tri_vertice, 0);
                new_shape.Print();
                if (new_shape != null)
                {
                    raw_shapes.Add(new_shape);
                }
            }
            Shapes.SortShapes(ref raw_shapes);
        }
    }//加载图形（位置）
    public void LoadEndState()
    {
        end_shapes = new List<ShapeData>();//shapedata的终止状态
        end_shape = new List<Shapes>();//shape的终止状态

        string json = File.ReadAllText(end_state);
        if (File.Exists(end_state))
        {
            Newtonsoft.Json.Linq.JArray input = JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(json);
            foreach (Newtonsoft.Json.Linq.JArray shape in input)
            {
                ShapeData s = new ShapeData();
                s.tris = new List<Triangle>();
                foreach (Newtonsoft.Json.Linq.JArray tri in shape)
                {
                    List<Vector3> points = new List<Vector3>();
                    points.Add(new Vector2(tri.Value<int>(0), tri.Value<int>(1)));
                    points.Add(new Vector2(tri.Value<int>(2), tri.Value<int>(3)));
                    points.Add(new Vector2(tri.Value<int>(4), tri.Value<int>(5)));
                    s.tris.Add(new Triangle(points[0], points[1], points[2]));
                }
                end_shapes.Add(s);
            }

            foreach (ShapeData sd in end_shapes)
            {
                Triangle[] tri_vertice = new Triangle[sd.tris.Count];
                for (int i = 0; i < sd.tris.Count; i++)
                {
                    List<Vector3> p_list = new List<Vector3>();
                    p_list.Add(GetPos(sd.tris[i].p1));
                    p_list.Add(GetPos(sd.tris[i].p2));
                    p_list.Add(GetPos(sd.tris[i].p3));
                    PointOperate.clockSort(ref p_list);
                    tri_vertice[i] = new Triangle(p_list[0], p_list[1], p_list[2]);
                }
                Shapes new_shape = NewShape(tri_vertice, 0);
                if (new_shape != null)
                {
                    end_shape.Add(new_shape);
                }
            }
            Shapes.SortShapes(ref end_shape);
        }
    }//设置结束时形状、位置

    public void SortShapes()
    {
        Shapes.SortShapes(ref raw_shapes);
    }

    public bool CheckGraph()
    { 
        if(raw_shapes.Count!=end_shape.Count||!end_shape[0].Equals(raw_shapes[0]))
        {
            return false;
        }
        Vector3 first_delta = end_shape[0].GetAllPoints()[0] - raw_shapes[0].GetAllPoints()[0];

        for(int i=1;i<raw_shapes.Count;i++)
        {
            Vector3 delta= end_shape[i].GetAllPoints()[0] - raw_shapes[i].GetAllPoints()[0];
            if(!end_shape[i].Equals(raw_shapes[i])||Vector3.Distance(first_delta,delta)>0.1f)
            {
                return false;
            }
        }
        return true;
    }//判断形状是否为结束形状

    public void DrawShapes()
    {
        for (int i = 0; i < raw_shapes.Count; i++)
        {
            raw_shapes[i].DrawShape();
        }

        //for (int i = 0; i < raw_shapes.Count; i++)
        //{
        //    end_shape[i].DrawShape();
        //}
    }

    public Shapes NewShape(Triangle[] triangles,int zbuffer)
    {
        if (pool==null)
        {
            Debug.Log("null");
        }
        Shapes shape_obj=pool.Spawn("Shape",Vector3.zero, Quaternion.identity).GetComponent<Shapes>();
        if (shape_obj==null)
        {
            return null;
        }
        shape_obj.SetVertice(triangles, zbuffer);
        return shape_obj;
    }

    public Vector2 GetPos(int x, int y)
    {
        float map_x = pointsImg.transform.position.x + pointsImg_width * x / 16;
        float map_y = pointsImg.transform.position.y + pointsImg_height * y / 16;

        return new Vector2(map_x, map_y);
    }

    public Vector2 GetPos(Vector2 pos)
    {
        return GetPos((int)pos.x, (int)pos.y);
    }

    public Vector2 GetClosestPoint(Vector2 pos)
    {
        float delta_x = pos.x - pointsImg.transform.position.x;
        float delta_y = pos.y - pointsImg.transform.position.y;

        int x = (int)Mathf.Round(delta_x *10/ pointsImg_width);
        int y = (int)Mathf.Round(delta_y *10/ pointsImg_height);

        return GetPos(x, y);

    }

}
