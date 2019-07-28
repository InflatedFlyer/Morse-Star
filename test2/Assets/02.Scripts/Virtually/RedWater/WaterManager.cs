using UnityEngine;
using System.Collections;
using System;
public class WaterManager : MonoBehaviour
{
    #region 调参部分
    //水波参数
    public float wave_length=100f;//波长
    public float cycle=1.0f;//周期
    public float swing=0.1f;//振幅
    public float top=-2;//水面基础高度
    public float bottom=-4.5f;//水底
    #endregion

    //Our renderer that'll make the top of the water visible
    private LineRenderer Body;

    //Our physics arrays
    float[] xpositions;
    float[] ypositions;
    float[] phases;

    //Our meshes and colliders
    GameObject[] meshobjects;
    Mesh[] meshes;

    //Our particle system
    public GameObject splash;

    //The material we're using for the top of the water
    public Material mat;

    //The GameObject we're using for a mesh
    public GameObject watermesh;

    //All our constants
    const float springconstant = 0.02f;
    const float damping = 0.018f;
    const float spread = 0.1f;
    float z;

    //The properties of our water
    float baseheight;
    float left;
    float width;

    void Start()
    {
        //Spawning our water
        z = transform.position.z;
        SpawnWater(-10, 20, top, bottom);
    }

    //填充水
    public void SpawnWater(float Left, float Width, float Top, float Bottom)
    {

        //Calculating the number of edges and nodes we have
        int edgecount = Mathf.RoundToInt(Width) * 5;
        int nodecount = edgecount + 1;

        //Add our line renderer and set it up:
        Body = gameObject.AddComponent<LineRenderer>();
        Body.material = mat;
        Body.material.renderQueue = 1000;
        Body.SetVertexCount(nodecount);
        Body.SetWidth(0.1f, 0.1f);

        //Declare our physics arrays
        xpositions = new float[nodecount];
        ypositions = new float[nodecount];
        phases = new float[nodecount];

        //Declare our mesh arrays
        meshobjects = new GameObject[edgecount];
        meshes = new Mesh[edgecount];

        //Set our variables
        baseheight = Top;
        bottom = Bottom;
        left = Left;
        width = Width;

        //For each node, set the line renderer and our physics arrays
        for (int i = 0; i < nodecount; i++)
        {
            ypositions[i] = Top;
            xpositions[i] = Left + Width * i / edgecount;
            phases[i] = (i * width * cycle) / wave_length;
            Body.SetPosition(i, new Vector3(xpositions[i], Top, z));
            
        }

        //Setting the meshes now:
        for (int i = 0; i < edgecount; i++)
        {
            //Make the mesh
            meshes[i] = new Mesh();

            //Create the corners of the mesh
            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
            Vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], z);
            Vertices[2] = new Vector3(xpositions[i], bottom, z);
            Vertices[3] = new Vector3(xpositions[i + 1], bottom, z);

            //Set the UVs of the texture
            Vector2[] UVs = new Vector2[4];
            UVs[0] = new Vector2(0, 1);
            UVs[1] = new Vector2(1, 1);
            UVs[2] = new Vector2(0, 0);
            UVs[3] = new Vector2(1, 0);

            //Set where the triangles should be.
            int[] tris = new int[6] { 0, 1, 3, 3, 2, 0 };

            //Add all this data to the mesh.
            meshes[i].vertices = Vertices;
            meshes[i].uv = UVs;
            meshes[i].triangles = tris;

            //Create a holder for the mesh, set it to be the manager's child
            meshobjects[i] = Instantiate(watermesh, Vector3.zero, Quaternion.identity) as GameObject;
            meshobjects[i].GetComponent<MeshFilter>().mesh = meshes[i];
            meshobjects[i].transform.parent = transform;
        }
    }

    //Same as the code from in the meshes before, set the new mesh positions
    private void UpdateMeshes()
    {
        for (int i = 0; i < meshes.Length; i++)
        {

            Vector3[] Vertices = new Vector3[4];
            Vertices[0] = new Vector3(xpositions[i], ypositions[i], z);
            Vertices[1] = new Vector3(xpositions[i + 1], ypositions[i + 1], z);
            Vertices[2] = new Vector3(xpositions[i], bottom, z);
            Vertices[3] = new Vector3(xpositions[i + 1], bottom, z);

            meshes[i].vertices = Vertices;
        }
    }

    //Called regularly by Unity
    private void FixedUpdate()
    {
        float t = Time.time;
        //Here we use the Euler method to handle all the physics of our springs:
        for (int i = 0; i < xpositions.Length; i++)
        {
            ypositions[i] =baseheight+swing*Mathf.Sin(Mathf.PI * 2 / cycle * t + phases[i]);
            Body.SetPosition(i, new Vector3(xpositions[i], ypositions[i], z));
        }

        UpdateMeshes();
    }

    //start到end段水面高度
    public float WaterLevel(float start,float end)
    {
        if(start>end)
        {
            float middle = start;
            start = end;
            end = middle;
        }
        int start_index = getIndex(start);
        int end_index = getIndex(end);
        float total=0;
        for (int i=start_index;i<=end_index;i++)
        {
            total += ypositions[i];
        }
        float average = total / (end_index - start_index + 1);
        return average;
    }

    //start到end段睡眠角度
    public float WaterAngle(float start,float end)
    {
        if (start > end)
        {
            float middle = start;
            start = end;
            end = middle;
        }
        int start_index = getIndex(start);
        int end_index = getIndex(end);

        float radian = Mathf.Atan((ypositions[end_index] - ypositions[start_index]) / (xpositions[end_index] - xpositions[start_index]));
        return radian*180/Mathf.PI;
    }

    //是否在水面上
    public bool isInRange(float xpos)
    {
        return xpos >= xpositions[0] && xpos <= xpositions[xpositions.Length - 1];
    }

    //xpos处对应的index
    private int getIndex(float xpos)
    {
        int index = Mathf.RoundToInt((xpositions.Length - 1) * (xpos - xpositions[0]) / width);
        if (index < 0)
        {
            return 0;
        }
        if(index>xpositions.Length-1)
        {
            return xpositions.Length - 1;
        }
        return index;
    }
}
