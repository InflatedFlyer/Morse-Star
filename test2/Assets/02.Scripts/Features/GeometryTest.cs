using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryTest : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        TestLineCross(new Vector3(1, 0, 0), new Vector3(2, 0, 0)
            , new Vector3(1.5f, 0.5f, 0), new Vector3(1.5f, -0.5f, 0));

        TestLineCross(new Vector3(1, 0, 0), new Vector3(0, 1, 0)
            , new Vector3(1, 1, 0), new Vector3(2, 1, 0));
        Debug.Log("test triangles");

        Triangle t1 = new Triangle(new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0));
        Triangle t2 = new Triangle(new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0));
        Triangle t3 = new Triangle(new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(2, 1, 0));
        Triangle t4 = new Triangle(new Vector3(0, 0, 0), new Vector3(5, 5, 0), new Vector3(5, 0, 0));
        Triangle t5 = new Triangle(new Vector3(3, 5, 0), new Vector3(4, 0, 0), new Vector3(3, 0, 0));

        //TestTriangles(t1, t2);
        //TestTriangles(t2, t3);
        //TestTriangles(t4, t5);
        List<Vector3> p_list=new List<Vector3>();

        p_list.Add(new Vector3(0, 3));

        p_list.Add(new Vector3(-3, 0));
        p_list.Add(new Vector3(3, 0));
        for (int i = 0; i < p_list.Count; i++)
        {
            Debug.Log(p_list[i]);
        }
        PointOperate.clockSort(ref p_list);
        for(int i=0;i<p_list.Count;i++)
        {
            Debug.Log(p_list[i]);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TestLineCross(Vector2 p1,Vector2 p2, Vector2 p3, Vector2 p4)
    {
        Line l1 = new Line(p1, p2);
        Line l2 = new Line(p3, p4);
        Vector3 point;
        bool isCross = l1.CrossPoint(l2, out point);
        Debug.Log(isCross);
        if (isCross)
        {
            Debug.Log(point);
        }
    }

    void TestTriangles(Triangle t1,Triangle t2)
    {
        Triangle[] tris = t1.CrossTriangle(t2);
        if(tris==null)
        {
            Debug.Log("false");
        }
        else
        {
            for(int i=0;i<tris.Length;i++)
            {
                Debug.Log(i+":"+tris[i].p1+" "+ tris[i].p2+" "+tris[i].p3);
            }
        }
    }
}
