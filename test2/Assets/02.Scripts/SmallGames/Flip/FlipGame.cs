using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Point
{
    public int x;
    public int y;

    public Point(int x,int y)
    {
        this.x = x;
        this.y = y;
    }
}

[System.Serializable]
public class FlipGame : MonoBehaviour {

    private enum Direct
    {
        Up, Down, Left, Right
    }

    public Vector2[] position;
    /*= new int[,] { {1,1 },{ 2,1}, { 3, 1 },{ 3,10}
            ,{ 4, 1 }, { 4, 2 }, { 4, 3 }, { 4, 4 },{ 4,10}
            ,{ 5,1},{5,2},{5,3},{ 5,4},{ 5,10},{ 5,11},{ 5,12},{5,13 },{ 5,14}
            ,{ 6,1},{6,2},{6,3},{ 6,4},{ 6,10},{ 6,11},{ 6,12},{6,13 },{ 6,14}
            ,{ 7,1},{7,2},{7,3},{ 7,4},{ 7,10},{ 7,11},{ 7,12},{7,13 },{ 7,14}
            ,{ 8,4},{ 8,14},{ 9,4},{ 9,14},{ 10,4},{ 10,14}
            ,{ 11,4},{ 11,7},{ 11,14},{ 12,4},{ 12,7},{ 12,14}
            ,{ 13,1},{13,2},{13,3},{ 13,4},{ 13,5},{ 13,6},{ 13,7},{13,8 },{ 13,9},{ 13,10}
            ,{ 13,11},{13,12},{13,13},{ 13,14} };
            */

    public bool[,] map;//地图
    private bool[,] hasCube;//某点上是否有方块

    public float cube_size;//单位方块大小
    public Vector2 start_pos;//（0,0)点的中心位置

    public Transform[] all_cubes;//被移动的方块
    private List<Cube> cubes;//方块的脚本
    public Transform backgound;//背景棋盘
    public Transform[] end_points;

    public Dictionary<Transform, Transform> trap;//对应的机关

    private bool isMoving;
	// Use this for initialization
	void Awake () {
        //初始化地图
        ConstructMap();

        //计算单位方块大小
        Transform back = backgound.GetChild(0);
        cube_size = back.GetComponentInChildren<SpriteRenderer>().size.x*back.localScale.x / 20;
        
        //计算(0,0)点的中心位置
        start_pos = back.position+new Vector3(-10*cube_size, 10 * cube_size);
        start_pos += new Vector2(cube_size / 2.0f, -cube_size / 2.0f);

        cubes = new List<Cube>();
        //将木块的位置按位置摆放
        foreach(Transform cube in all_cubes)
        {
            Cube c = cube.GetComponent<Cube>();
            c.transform.position = CalculatePosition(c.pos);
            cubes.Add(c);
        }
        HasCube();

        isMoving = false;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(CheckGameOver());

		if(!isMoving)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isMoving = true;
                UpdateCubeMoveState(Direct.Left);
                MoveAllLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                isMoving = true;
                UpdateCubeMoveState(Direct.Right);
                MoveAllRight();
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                isMoving = true;
                UpdateCubeMoveState(Direct.Up);

                MoveAllUp();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                isMoving = true;
                UpdateCubeMoveState(Direct.Down);
                MoveAllDown();
            }
        }
        isMoving = isStillMoving();
	}

    private bool CheckGameOver()
    {
        for(int i=0;i<end_points.Length;i++)
        {
            if(!end_points[i].GetComponent<GameOver>().success)
            {
                return false;
            }
        }
        return true;
    }

    private void MoveAllLeft()
    {
        for (int i = 0; i < map.GetLength(1); i++)
        {
            foreach (Cube c in cubes)
            {
                if (c.canMove)
                {
                    if (c.pos.y == i)
                    {
                        Point target = CalculateTarget(c.pos, Direct.Left);
                        if (target != null)
                        {
                            c.pos = target;
                            HasCube();
                            c.MoveTo(CalculatePosition(target));
                        }
                    }
                }
            }
        }
    }

    private void MoveAllRight()
    {
        for (int i = map.GetLength(1) - 1; i >= 0; i--)
        {
            foreach (Cube c in cubes)
            {
                if (c.canMove)
                {
                    if (c.pos.y == i)
                    {
                        Point target = CalculateTarget(c.pos, Direct.Right);
                        if (target != null)
                        {
                            c.pos = target;
                            HasCube();
                            c.MoveTo(CalculatePosition(target));
                        }
                    }
                }
            }
        }
    }

    private void MoveAllUp()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            foreach (Cube c in cubes)
            {
                if (c.canMove)
                {
                    if (c.pos.x == i)
                    {
                        Point target = CalculateTarget(c.pos, Direct.Up);
                        if (target != null)
                        {
                            c.pos = target;
                            HasCube();
                            c.MoveTo(CalculatePosition(target));
                        }
                    }
                }
            }
        }
    }

    private void MoveAllDown()
    {
        for (int i = map.GetLength(0) - 1; i >= 0; i--)
        {
            foreach (Cube c in cubes)
            {
                if (c.canMove)
                {
                    if (c.pos.x == i)
                    {
                        Point target = CalculateTarget(c.pos, Direct.Down);
                        if (target != null)
                        {
                            c.pos = target;

                            HasCube();
                            c.MoveTo(CalculatePosition(target));
                        }
                    }
                }
            }
        }
    }

    private bool isStillMoving()
    {
        foreach(Cube cube in cubes)
        {
            if(cube.isMoving)
            {
                return true;
            }
        }
        return false;
    }

    //构造地图
    private void ConstructMap()
    {
        map = new bool[20, 20];
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                map[i, j] = false;
            }
        }
        

        for (int i = 0; i < position.GetLength(0); i++)
        {
            map[(int)position[i].x, (int)position[i].y] = true;
        }
    }
    
    //更新hasCube数组
    private void HasCube()
    {
        hasCube = new bool[20, 20];
        for (int i = 0; i < hasCube.GetLength(0); i++)
        {
            for (int j = 0; j < hasCube.GetLength(1); j++)
            {
                hasCube[i, j] = false;
            }
        }

        foreach (Cube cube in cubes)
        {
            hasCube[cube.pos.x, cube.pos.y] = true;
        }
    }

    //计算每个物块能否移动
    private void UpdateCubeMoveState(Direct direct)
    {
        foreach(Cube c in cubes)
        {
            c.canMove = ifCubeCanMove(c.pos, direct);
        }
    }

    private bool ifCubeCanMove(Point pos,Direct direct)
    {
        Point target=CalculateTarget(pos, direct);
        if (target != null && !(target.x==pos.x&&target.y== pos.y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private Vector2 CalculatePosition(int x,int y)
    {
        if (!isLegalPos(new Point(x,y)))
        {
            return Vector2.zero;
        }
        return start_pos + new Vector2( y * cube_size, -x * cube_size);
    }

    private Vector2 CalculatePosition(Point pos)
    {
        return CalculatePosition(pos.x, pos.y);
    }

    private Point CalculateTarget(Point cube_pos, Direct direct)
    {
        Point ret_pos=null;
        switch(direct)
        {
            case Direct.Up:
                ret_pos=SearchForVertical(cube_pos,-1);
                break;
            case Direct.Down:
                ret_pos = SearchForVertical(cube_pos, 1);
                break;
            case Direct.Left:
                ret_pos = SearchForHorizon(cube_pos, -1);
                break;
            case Direct.Right:
                ret_pos = SearchForHorizon(cube_pos, 1);
                break;
            default:
                break;
        }
        return ret_pos;
    }

    private Point SearchForHorizon(Point pos,int direct)
    {
        if(!isLegalPos(pos))
        {
            return null;
        }
        else 
        {
            int i = pos.x, j = pos.y+direct;
            while (map[i, j]&&!hasCube[i,j])
            {
                j += direct;
            }
            j -= direct;
            return new Point(i, j);
            
        }
    }

    private Point SearchForVertical(Point pos,int direct)
    {
        if (!isLegalPos(pos))
        {
            return null;
        }
        else
        {
            int i = pos.x+direct, j = pos.y;
            while (map[i, j]&&!hasCube[i,j])
            {
                i += direct;
            }
            i -= direct;
            return new Point(i, j);
        }
    }

    private bool isLegalPos(Point pos)
    {
        if (pos.x >= 0 && pos.x < map.GetLength(0) && pos.y >= 0 && pos.y < map.GetLength(1))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
