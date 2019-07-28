using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Triangle
{
    public Triangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        this.p1 = p1;
        this.p2 = p2;
        this.p3 = p3;
    }

    public bool IsInTriangle(Vector3 point)
    {
        Vector3 p12 = p2 - p1;
        Vector3 p13 = p3 - p1;
        Vector3 p21 = p1 - p2;
        Vector3 p23 = p3 - p2;
        Vector3 p1p = point - p1;
        Vector3 p2p = point - p2;
        if (Vector3.Dot(Vector3.Cross(p12, p1p), Vector3.Cross(p12, p13)) >= 0 &&
            Vector3.Dot(Vector3.Cross(p13, p1p), Vector3.Cross(p13, p12)) >= 0 &&
            Vector3.Dot(Vector3.Cross(p23, p2p), Vector3.Cross(p23, p21)) >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Triangle[] CrossTriangle(Triangle tri)
    {
        List<Vector3> cross_points = new List<Vector3>();

        Vector3[] points_a = this.points, points_b = tri.points;
        Line[] lines_a = this.lines, lines_b = tri.lines;
        for (int i=0;i<points_a.Length;i++)
        {
            if(tri.IsInTriangle(points_a[i]))
            {
                if (!cross_points.Exists(item=>Vector3.Distance(item,points_a[i])<0.01f))
                {
                    cross_points.Add(points_a[i]);
                }
            }

            if(this.IsInTriangle(points_b[i]))
            {
                if (!cross_points.Exists(item => Vector3.Distance(item, points_b[i]) < 0.01f))
                {
                    cross_points.Add(points_b[i]);
                }
            }

            for(int j=0;j<lines_b.Length;j++)
            {
                Vector3 cross;
                bool isCross=lines_a[i].CrossPoint(lines_b[j], out cross);
                if(isCross)
                {
                    if (!cross_points.Exists(item => Vector3.Distance(item, cross) < 0.01f))
                    {
                        cross_points.Add(cross);
                    }
                }
            }
        }
        if (cross_points.Count < 3)
        {
            return null;
        }
        else
        {
            PointOperate.clockSort(ref cross_points);
            Triangle[] cross_triangles = new Triangle[cross_points.Count - 2];
            for (int i=0;i<cross_triangles.Length;i++)
            {
                cross_triangles[i] = new Triangle(cross_points[0], cross_points[i + 1], cross_points[i + 2]);   
            }
            return cross_triangles;
        }
    }
    public Vector3 p1, p2, p3;

    public Vector3[] points
    {
        get
        {
            return new Vector3[] { p1, p2, p3 };
        }
    }
    public Line[] lines
    {
        get
        {
            return new Line[] { new Line(p1, p2), new Line(p2, p3), new Line(p3, p1) };
        }
    }
}

public class Line
{
    public Line(Vector3 p1, Vector3 p2)
    {
        this.p1 = p1;
        this.p2 = p2;
    }

    public bool CrossPoint(Line l, out Vector3 cross_point)
    {
        if(l.p1==l.p2||this.p1==this.p2)
        {
            cross_point = Vector3.zero;
            return false;
        }

        Vector3 p12 = this.p2 - this.p1;
        Vector3 pL1L2 = l.p2 - l.p1;

        Vector3 p1L1 = l.p1 - this.p1;

        if (Mathf.Abs(Cross(p12,pL1L2))<1e-6f)
        {
            cross_point = Vector3.zero;
            return false;
        }
        
        float det = -Cross(p12,pL1L2);

        float lamda, mu;
        lamda =Cross(pL1L2, p1L1) / det;
        mu = Cross(p12, p1L1) / det;
        if (lamda >= 0 && lamda <= 1 && mu >= 0 && mu <= 1)
        {
            cross_point = new Vector3(p1.x + lamda * (p2.x - p1.x), p1.y + lamda * (p2.y - p1.y));
            return true;
        }
        else
        {
            cross_point = Vector3.zero;
            return false;
        }
    }

    private float Cross(Vector3 v1,Vector3 v2)
    {
        return v1.x * v2.y - v1.y * v2.x;
    }
    public Vector3 p1, p2;
}

public static class PointOperate
{
    //若A大于B，返回true
    private static bool pointCMP(Vector3 A, Vector3 B, Vector3 center)
    {
        Vector3 vector_a = A - center, vector_b = B - center;
        if(vector_a.y*vector_b.y<0)
        {
            return vector_a.y < 0;
        }
        else if(vector_a.y*vector_b.y==0)
        {
            if(vector_a.y==0&&vector_b.y==0)
            {
                return vector_a.x > vector_b.x;
            }
            else if(vector_a.y==0&&vector_a.x<=0)
            {
                return false;
            }
            else if(vector_b.y==0&&vector_b.x<=0)
            {
                return true;
            }
        }
        float det =(A.x - center.x) * (B.y - center.y) - (B.x - center.x) * (A.y - center.y);
        if (det < 0) return false;
        if (det > 0) return true;
        double d1 = (A.x - center.x) * (A.x - center.x) + (A.y - center.y) * (A.y - center.y);
        double d2 = (B.x - center.x) * (B.x - center.y) + (B.y - center.y) * (B.y - center.y);
        return d1 > d2;
    }

    public static void clockSort(ref List<Vector3> s)
    {
        float x = 0f, y = 0f, z = 0f;
        foreach (Vector3 p in s)
        {
            x += p.x;
            y += p.y;
            z += p.z;
        }
        Vector3 gPoint = new Vector3(x / s.Count, y / s.Count, z / s.Count);
        for (int i = 0; i < s.Count; i++)
        {
            for (int j = 0; j < s.Count - i - 1; j++)
                if (pointCMP(s[j], s[j + 1], gPoint))
                {
                    Vector3 temp = s[j]; s[j] = s[j + 1]; s[j + 1] = temp;
                }
        }
    }

}
