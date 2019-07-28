using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowVision : MonoBehaviour {

    public Transform monitor_obj;
    //乌鸦的视线范围
    public float vision_angle=30;   //视线角
    public float start_angle = -45; //左边界
    public float end_angle = 45;    //右边界
    public float radius = 7f;       //视角半径
    public GameObject vision_prefab;//视线预设
    public int angle_per_mesh = 5;  //每个mesh包括多大的角

    //当前看的视角
    public float angle_speed = 10f; //视角转动速度
    private float current_angle = 0;//当前视角
    private int direct = 1;         //方向 1向右，-1向左

    //mesh的参数
    private List<float> x_positions;
    private List<float> y_positions;
    private GameObject[] mesh_objs;
    private Mesh[] meshs;
 	// Use this for initialization
	void Start () {
        x_positions = new List<float>();
        y_positions = new List<float>();
        DrawVision();
        StartCoroutine(UpdateCurrentAngle());
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(IsInVision(monitor_obj.position));
	}

    private IEnumerator UpdateCurrentAngle()
    {
        float unit_time = 0.5f;
        while (true)
        {
            current_angle += direct * angle_speed*unit_time;
            if(current_angle>=end_angle)
            {
                current_angle = end_angle;
                direct = -1;
            }
            else if(current_angle<=start_angle)
            {
                current_angle = start_angle;
                direct = 1;
            }
            UpdateVision();
            yield return new WaitForSeconds(unit_time);
        }
    }

    private void DrawVision()
    {
        int mesh_count=Mathf.RoundToInt(vision_angle / angle_per_mesh);
        float left_angle = current_angle - vision_angle / 2f;
        float right_angle = current_angle + vision_angle / 2f;
        for (int i=0;i<mesh_count+1;i++)
        {
            float angle = left_angle+(float)i / mesh_count * vision_angle;
            if(angle>right_angle)
            {
                angle = right_angle;
            }
            x_positions.Add(transform.position.x + Mathf.Sin(angle*Mathf.Deg2Rad) * radius);
            y_positions.Add(transform.position.y - Mathf.Cos(angle * Mathf.Deg2Rad) * radius);
            
        }
        
        mesh_objs = new GameObject[x_positions.Count - 1];
        meshs = new Mesh[x_positions.Count - 1];

        Vector2[] UVs = new Vector2[3];
        UVs[0] = new Vector2(0, 0);
        UVs[1] = new Vector2(0, 1);
        UVs[2] = new Vector2(1, 0);

        int[] tri = new int[] { 0, 2, 1 };

        for (int i = 0; i < meshs.Length; i++)
        {
            Vector3[] Vertices = new Vector3[3];
            Vertices[0] = transform.position;
            Vertices[1] = new Vector3(x_positions[i], y_positions[i]);
            Vertices[2] = new Vector3(x_positions[i + 1], y_positions[i + 1]);
            
            meshs[i] = new Mesh();
            meshs[i].vertices = Vertices;
            meshs[i].uv = UVs;
            meshs[i].triangles = tri;

            mesh_objs[i] = Instantiate(vision_prefab, Vector3.zero, Quaternion.identity);
            mesh_objs[i].GetComponent<MeshFilter>().mesh = meshs[i];
            mesh_objs[i].transform.parent = this.transform;
        }
    }

    private void UpdateVision()
    {
        int mesh_count = Mathf.RoundToInt(vision_angle / angle_per_mesh);
        float left_angle = current_angle - vision_angle / 2f;
        float right_angle = current_angle + vision_angle / 2f;
        for (int i = 0; i < mesh_count+1; i++)
        {
            float angle = left_angle + (float)i / mesh_count * vision_angle;
            if (angle > right_angle)
            {
                angle = right_angle;
            }

            x_positions[i]=transform.position.x + Mathf.Sin(angle * Mathf.Deg2Rad) * radius;
            y_positions[i]=transform.position.y - Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        }
        for (int i = 0; i < meshs.Length ; i++)
        {
            Vector3[] Vertices = new Vector3[3];
            Vertices[0] = transform.position;
            Vertices[1] = new Vector3(x_positions[i], y_positions[i]);
            Vertices[2] = new Vector3(x_positions[i + 1], y_positions[i + 1]);
            
            meshs[i].vertices = Vertices;
        }
    }

    public bool IsInVision(Vector3 pos)
    {
        if (Vector2.Distance(pos,transform.position)<=radius)
        {
            Vector2 delta = transform.position-pos;
            float angle=Mathf.Rad2Deg*Mathf.Atan(-delta.x / delta.y);
            if(Mathf.Abs(angle-current_angle)<= vision_angle/2)
            {
                return true;
            }
        }
        return false;
    }
}
