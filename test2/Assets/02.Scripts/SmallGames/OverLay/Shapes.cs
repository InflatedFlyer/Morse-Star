using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shapes : MonoBehaviour {
    
    private Triangle[] tris_vertice;//输入的三角形点的真实坐标
    private Triangle[] base_tris_vertice;//保存三角形的起始点
    
    public GameObject black_prefab; //绘制的黑色预设

    public bool isDrag = false;    //物体是否在被拖拽
    public bool isAdjust = true;  //物体是否在自动调整位置
    
    [HideInInspector]
    public int z_buffer = 0;        //图形的叠加次数
    [HideInInspector]
    public List<int> parent_shapes;
    public OverLay game;
	// Use this for initialization
	void Start () {
        parent_shapes = new List<int>();
        game = GameObject.Find("OverLay").GetComponent<OverLay>();
        isAdjust = true;
        isDrag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(gameObject.name + ":" + transform.position);
        }
    }
    public void SetVertice(Triangle[] tris,int z_buffer)
    {
        tris_vertice = new Triangle[tris.Length];

        base_tris_vertice = new Triangle[tris.Length];

        this.z_buffer = z_buffer;
        for(int i=0;i<tris.Length;i++)
        {
            Vector2 p1 = tris[i].p1;
            Vector2 p2 = tris[i].p2;
            Vector2 p3 = tris[i].p3;

            tris_vertice[i] = new Triangle(new Vector3(p1.x, p1.y), new Vector3(p2.x, p2.y), new Vector3(p3.x, p3.y));
            base_tris_vertice[i] = new Triangle(new Vector3(p1.x, p1.y), new Vector3(p2.x, p2.y), new Vector3(p3.x, p3.y));
        }
    }

    //鼠标拖动物体
    public IEnumerator MouseDown()
    {
        isDrag = true;
        isAdjust = false;
        Vector3 offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        while (Input.GetMouseButton(0))
        {
            transform.position = offset + Camera.main.ScreenToWorldPoint(Input.mousePosition);
            yield return new WaitForFixedUpdate();
        }
        isDrag = false;
        StartCoroutine(MoveToPoint());
    }

    //结束拖拽后，自动移动到最近单元点
    public IEnumerator MoveToPoint()
    {
        if (tris_vertice[0] != null)
        {
            //现在的位置
            Vector3 current_pos = tris_vertice[0].p1;

            //目标位置
            Vector3 target = game.GetClosestPoint(current_pos);
            while (!isAdjust && !isDrag)
            {
                current_pos = tris_vertice[0].p1;
                Vector3 direct = target - current_pos;
                if (Vector3.Distance(tris_vertice[0].p1, target) < 0.1f)
                {
                    Vector3 offset = target - base_tris_vertice[0].p1;
                    transform.position = new Vector3(offset.x, offset.y, transform.position.z);
                    isAdjust = true;
                }
                else
                {
                    transform.Translate(direct * 0.1f);
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }


    //通过绘制三角形，最终绘制出图形
    public void DrawShape()
    {
        for(int i=0;i<tris_vertice.Length;i++)
        {
            DrawTriangle(tris_vertice[i]);
        }
    }

    //利用mesh绘制三角形
    private void DrawTriangle(Triangle tri)
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[] { tri.p1, tri.p2, tri.p3 };
        Vector2[] uvs = new Vector2[] { new Vector2(0, 0),new Vector2(0, 1), new Vector2(1, 0) };
        int[] indice = new int[] { 0, 1, 2 };
        
        mesh.vertices = vertices;
        Debug.Log(vertices[0] + "--" + vertices[1] + "--" + vertices[2]);
        mesh.uv = uvs;
        mesh.triangles = indice;

        GameObject shape = OverLay.pool.Spawn(black_prefab,new Vector3(0,0,-8.5f),Quaternion.identity).gameObject;
        Debug.Log(shape.transform.position);
        shape.GetComponent<MeshFilter>().mesh = mesh;
        switch(z_buffer)
        {
            case 0:
                shape.GetComponent<MeshRenderer>().material.color = Color.white;
                break;
            case 1:
                shape.GetComponent<MeshRenderer>().material.color = Color.white;
                break;
            case 2:
                shape.GetComponent<MeshRenderer>().material.color = Color.yellow;
                break;
            default:
                shape.GetComponent<MeshRenderer>().material.color =Color.black;
                break;
        }

        shape.transform.parent = transform;
    }

    private void UpdateTriangleVertice()
    {
        for(int i=0;i<tris_vertice.Length;i++)
        {
            tris_vertice[i].p1 = base_tris_vertice[i].p1 + transform.position;
            tris_vertice[i].p2 = base_tris_vertice[i].p2 + transform.position;
            tris_vertice[i].p3 = base_tris_vertice[i].p3 + transform.position;
        }
    }

    //判断pos点是否在本图形内
    public bool IsInShape(Vector3 pos)
    {
        for(int i=0;i<tris_vertice.Length;i++)
        {
            if(tris_vertice[i].IsInTriangle(pos))
            {
                return true;
            }
        }
        return false;
    }

    public Triangle[] CrossShape(Shapes other)
    {
        List<Triangle> cross_tris = new List<Triangle>();
        for(int i=0;i<this.tris_vertice.Length;i++)
        {
            for(int j=0;j<other.tris_vertice.Length;j++)
            {
                Triangle[] tris = this.tris_vertice[i].CrossTriangle(other.tris_vertice[j]);
                if(tris!=null)
                {
                    cross_tris.AddRange(tris);
                }
            }
        }
        if(cross_tris.Count==0)
        {
            return null;
        }
        else
        {
            return cross_tris.ToArray();
        }
    }

    public IEnumerator UpdateShapes()
    {
        while (isDrag || !isAdjust)
        {
            UpdateTriangleVertice();
            yield return new WaitForFixedUpdate();
        }
        //总会少对齐一帧，补上
        UpdateTriangleVertice();
        game.SortShapes();
    }

    public List<Vector3> GetAllPoints()
    {
        List<Vector3> points = new List<Vector3>();
        if (tris_vertice != null)
        {
            for (int i = 0; i < tris_vertice.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!points.Exists(item => Vector3.Distance(item, tris_vertice[i].points[j])<0.1f))
                    {
                        points.Add(tris_vertice[i].points[j]);
                    }
                }
            }
            PointOperate.clockSort(ref points);
        }
        return points;
    }

    public bool Equals(Shapes other)
    {
        List<Vector3> this_points = this.GetAllPoints();
        List<Vector3> other_points = other.GetAllPoints();
        if(this_points.Count!=other_points.Count)
        {
            return false;
        }
        else
        {
            Vector3 first_delta = other_points[0] - this_points[0];
            for(int i=1;i<other_points.Count;i++)
            {
                if(Vector3.Distance(first_delta,other_points[i]-this_points[i])>0.1f)
                {
                    return false;
                }
            }
            return true;
        }
    }

    public bool LowerThan(Shapes other)
    {
        List<Vector3> this_points = this.GetAllPoints();
        List<Vector3> other_points = other.GetAllPoints();
        if (this_points.Count != other_points.Count)
        {
            return this_points.Count<other_points.Count;
        }
        else
        {
            if(Equals(other))
            {
                Vector3 first_delta = other_points[0] - this_points[0];
                if(first_delta.y==0)
                {
                    return first_delta.x > 0;
                }
                else
                {
                    return first_delta.y < 0;
                }
            }
            for (int i = 1; i < other_points.Count; i++)
            {
                Vector3 this_vector = this_points[i] - this_points[0];
                Vector3 other_vector = other_points[i] - other_points[0];
                Vector3 delta = other_vector - this_vector;
                if(delta.y==0)
                {
                    if (delta.x == 0) continue;
                    return delta.x > 0;
                }
                else
                {
                    return delta.y < 0;
                }
            }
            return false;
        }

    }

    public static void SortShapes(ref List<Shapes> shapes)
    {
        for(int i=shapes.Count-1;i>0;i--)
        {
            for(int j=0;j<i;j++)
            {
                if(shapes[j+1].LowerThan(shapes[j]))
                {
                    Shapes temp = shapes[j + 1];
                    shapes[j + 1] = shapes[j];
                    shapes[j] = temp;
                }
            }
        }
    }

    public void Print()
    {
        Debug.Log("Shape------------------------------------");
        List<Vector3> point = GetAllPoints();
        for (int i = 0; i < point.Count; i++)
        {
            Debug.Log(point[i]);
        }
        Debug.Log("-----------------------------------------");

    }
}
