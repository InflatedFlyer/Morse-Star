using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimePoint
{
    public TimePoint(float time,int direct=0)
    {
        this.time = time;
        this.direct = direct;
    }
    public float time;
    public int direct=0;
}

public class MusicGame : MonoBehaviour {

    //路径参数
    public TimePoint[] time_points;
    public GameObject path_prefab;
    private float path_width = 0.5f;
    
    //路径变量
    private List<float> x_positions;
    private List<float> y_positions;
    private List<Triangle> triangles;
    private GameObject[] mesh_objs;
    private Mesh[] meshs;
    private int cornerMeshCount = 5;
    
    //圆块移动速度
    public float horizon_speed = 5f;
    public float vertical_speed = 5f;
    private int vertical_direct = 0;

    //圆块
    Transform ball;

    public bool gameStart = false;
    public bool gameOver = false;

    //游戏时间节奏控制参数
    private float start_time;   //游戏开始时间
    private float current_time; //当前游戏时间
    private float unit_time=0.1f;//单位时间
    private float score_time = 0;//在区域内的时间，计算游戏得分
    private bool isOperated = false;//这次单位时间内是否操作
    private bool isScored = false;//这次单位时间内是否得分
    
    // Use this for initialization
    void Start () {
        ball = GameObject.Find("ball").transform;
        BuildPath();
    }
	
	// Update is called once per frame
	void Update () {
        
        //按S让游戏开始
        if (Input.GetKey(KeyCode.S))
        {
            gameStart = true;
            start_time = Time.time;
            current_time = start_time;
        }

        if (!gameOver&&gameStart)
        {
            gameOver = isGameOver();
            if(gameOver)
            {
                float score = 100*score_time / time_points[time_points.Length - 1].time;
                if(score>98)
                {
                    score = 100;
                }
                Debug.Log("score:" + score);
            }

            //下一个时间单位
            if (Time.time - current_time > unit_time)
            {
                current_time += unit_time;
                isOperated = false;
                isScored = false;
            }
            else
            {
                //在区域内就加分
                if (!isScored&&IsInPath())
                {
                    score_time += unit_time;
                    isScored = true;
                }

                if (!isOperated)
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        vertical_direct = 1;
                        isOperated = true;
                    }
                    else if (Input.GetKey(KeyCode.DownArrow))
                    {
                        vertical_direct = -1;
                        isOperated = true;
                    }
                    else
                    {
                        vertical_direct = 0;
                    }
                }
            }
            Vector3 current = ball.position;
            ball.position = current + new Vector3(horizon_speed, vertical_direct * vertical_speed, 0) * Time.deltaTime;
        }
    }

    private void BuildPath()
    {
        //计算每次需要旋转的角度
        float angle = Mathf.Atan(vertical_speed / horizon_speed);

        x_positions = new List<float>();
        y_positions = new List<float>();
        triangles = new List<Triangle>();

        x_positions.Add(0);
        y_positions.Add(path_width / 2);
        x_positions.Add(0);
        y_positions.Add(-path_width / 2);

        for (int i=0;i<time_points.Length;i++)
        {
            if (i == time_points.Length - 1)
            {
                float horizon_distance = horizon_speed * time_points[i].time;
                x_positions.Add(horizon_distance);
                x_positions.Add(horizon_distance);

                float vertical_distance = vertical_speed * (time_points[i].time - time_points[i - 1].time) * time_points[i - 1].direct;
                y_positions.Add(y_positions[y_positions.Count - 2] + vertical_distance);
                y_positions.Add(y_positions[y_positions.Count - 2] + vertical_distance);
            }
            else
            {
                if(time_points[i].direct==0)
                {
                    if (time_points[i - 1].direct == 1)
                    {
                        float base_x = time_points[i].time * horizon_speed + Mathf.Sin(angle / 2) * path_width * 1.5f;
                        float base_y = y_positions[y_positions.Count - cornerMeshCount * 2 - 2] + time_points[i - 1].direct * vertical_speed * (time_points[i].time - time_points[i - 1].time) - 2 * path_width;

                        for (int j = cornerMeshCount; j >= 0; j--)
                        {
                            float mesh_angle = (float)j / cornerMeshCount;

                            x_positions.Add(base_x - Mathf.Sin(mesh_angle) * 2 * path_width);
                            x_positions.Add(base_x - Mathf.Sin(mesh_angle) * path_width);

                            y_positions.Add(base_y + Mathf.Cos(mesh_angle) * 2 * path_width);
                            y_positions.Add(base_y + Mathf.Cos(mesh_angle) * path_width);
                        }
                    }
                    else if(time_points[i-1].direct==-1)
                    {
                        float base_x = time_points[i].time * horizon_speed + Mathf.Sin(angle / 2) * path_width * 1.5f;
                        float base_y = y_positions[y_positions.Count - cornerMeshCount * 2 - 2] + time_points[i - 1].direct * vertical_speed * (time_points[i].time - time_points[i - 1].time) + path_width;

                        for (int j = cornerMeshCount; j >= 0; j--)
                        {
                            float mesh_angle = (float)j / cornerMeshCount;

                            x_positions.Add(base_x - Mathf.Sin(mesh_angle) * path_width);
                            x_positions.Add(base_x - Mathf.Sin(mesh_angle) * 2 * path_width);

                            y_positions.Add(base_y - Mathf.Cos(mesh_angle) * path_width);
                            y_positions.Add(base_y - Mathf.Cos(mesh_angle) * 2 * path_width);
                        }
                    }
                }
                else if(time_points[i].direct==1)
                { 
                    float base_x = time_points[i].time*horizon_speed-Mathf.Sin(angle / 2) * path_width * 1.5f ;
                    float base_y = y_positions[y_positions.Count - 2] + path_width;
                    

                    for (int j=0;j<=cornerMeshCount;j++)
                    {
                        float mesh_angle = (float)j / cornerMeshCount;

                        x_positions.Add(base_x + Mathf.Sin(mesh_angle) * path_width);
                        x_positions.Add(base_x + Mathf.Sin(mesh_angle) * 2 * path_width);

                        y_positions.Add(base_y - Mathf.Cos(mesh_angle) * path_width);
                        y_positions.Add(base_y - Mathf.Cos(mesh_angle) * 2 * path_width);
                    }
                }
                else if(time_points[i].direct==-1)
                {
                    float base_x = time_points[i].time * horizon_speed - Mathf.Sin(angle / 2) * path_width * 1.5f;
                    float base_y = y_positions[y_positions.Count - 2] -2* path_width;


                    for (int j = 0; j <= cornerMeshCount; j++)
                    {
                        float mesh_angle = (float)j / cornerMeshCount;

                        x_positions.Add(base_x + Mathf.Sin(mesh_angle) * 2 * path_width);
                        x_positions.Add(base_x + Mathf.Sin(mesh_angle) * path_width);

                        y_positions.Add(base_y + Mathf.Cos(mesh_angle) * 2 * path_width);
                        y_positions.Add(base_y + Mathf.Cos(mesh_angle) * path_width);
                    }
                }
            }
        }

        mesh_objs = new GameObject[x_positions.Count / 2 - 1];
        meshs = new Mesh[x_positions.Count / 2 - 1];

        Vector2[] UVs = new Vector2[4];
        UVs[0] = new Vector2(0, 1);
        UVs[1] = new Vector2(0, 0);
        UVs[2] = new Vector2(1, 1);
        UVs[3] = new Vector2(1, 0);

        int[] tris = new int[6] { 0, 2, 3, 3, 1, 0 };

        for (int i = 0; i < meshs.Length; i++)
        {

            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(x_positions[i * 2], y_positions[i * 2]);
            Vertices[1] = new Vector3(x_positions[i * 2 + 1], y_positions[i * 2 + 1]);
            Vertices[2] = new Vector3(x_positions[i * 2 + 2], y_positions[i * 2 + 2]);
            Vertices[3] = new Vector3(x_positions[i * 2 + 3], y_positions[i * 2 + 3]);

            triangles.Add(new Triangle(Vertices[tris[0]], Vertices[tris[1]], Vertices[tris[2]]));
            triangles.Add(new Triangle(Vertices[tris[3]], Vertices[tris[4]], Vertices[tris[5]]));

            meshs[i] = new Mesh();

            meshs[i].vertices = Vertices;
            meshs[i].uv = UVs;
            meshs[i].triangles = tris;

        
            mesh_objs[i] = Instantiate(path_prefab, Vector3.zero, Quaternion.identity);
            mesh_objs[i].GetComponent<MeshFilter>().mesh = meshs[i];
            mesh_objs[i].transform.parent = transform;
        }
    }

    private bool IsInPath()
    {
        for(int i=0;i<triangles.Count;i++)
        {
            if(triangles[i].IsInTriangle(ball.transform.position))
            {
                return true;
            }
        }
        return false;
    }

    private bool isGameOver()
    {
        return ball.position.x >= horizon_speed * time_points[time_points.Length - 1].time;
    }
}
