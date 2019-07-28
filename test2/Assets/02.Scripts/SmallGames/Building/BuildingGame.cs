using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGame : MonoBehaviour {

    //方块的预设
    public GameObject build_prefab;
    private BuildMove building=null;

    //方块的生成位置
    private float current_left, current_right;
    private float generate_height;
    private float building_height;
    private float building_width;

    //屏幕移动参数
    private bool ScreenMove = false;
    private Transform camera;
    private float base_height;
    private float current_height
    {
        get
        {
            float height_count = success_count > ScreenMoveCount ? ScreenMoveCount : success_count;
            return base_height + building_height * height_count;
        }
    }
    private float ScreenMoveSpeed = 3f;
    private float ScreenMoveCount = 3;

    //成功方块
    private int target = 1000;
    private int success_count = 0;
    private bool isCount = false;

    private bool gameOver = false;
	// Use this for initialization
	void Start () {
        camera = GameObject.FindWithTag("MainCamera").transform;
        GameObject plane = GameObject.Find("Plane");
        base_height = plane.GetComponent<BuildMove>().height / 2 + plane.transform.position.y;

        current_left = BuildMove.left_bound;
        current_right = BuildMove.right_bound;
        building = Instantiate(build_prefab, new Vector3(0, generate_height, 0), Quaternion.identity).GetComponent<BuildMove>();
        if(building!=null)
        {
            building_height = building.height;
            building_width = building.width;
            Destroy(building.gameObject);
            building = null;
        }
        generate_height = base_height + building_height/2;
    }
	
	// Update is called once per frame
	void Update () {
        if(success_count>=target)
        {
            gameOver = true;
        }
        if (!gameOver)
        {
            CheckBuild();
            if (ScreenMove)
            {
                float distance = ScreenMoveSpeed * Time.deltaTime;
                camera.Translate(0, distance, 0);
                base_height += distance;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Release();
                }
            }
        }
	}

    private void GenerateCube()
    {
        building = Instantiate(build_prefab, new Vector3(0,generate_height,0), Quaternion.identity).GetComponent<BuildMove>();
        building.ChangeShape(BuildMove.left_bound, BuildMove.left_bound+building_width);
    }

    private void StartMove()
    {
        if (building != null)
        {
            building.StartMove();
        }
    }

    private void Release()
    {
        if (building != null)
        {
            building.StopMove();
        }
    }
    

    private void CheckBuild()
    {
        if(building==null)
        {
            GenerateCube();
            StartMove();
        }
        else
        {
            if (building.state==0)
            {
                if (building.right-current_left < 0.1f||current_right-building.left<0.1f)
                {
                    Destroy(building.gameObject);
                    building = null;
                }
                else
                {
                    if (!ScreenMove)
                    {
                        if (building.left < current_left)
                        {
                            current_right = building.right;
                        }
                        else if (building.right > current_right)
                        {
                            current_left = building.left;
                        }
                        else
                        {
                            current_left = building.left;
                            current_right = building.right;
                        }
                        building.ChangeShape(current_left, current_right);
                        building_width = building.width;

                        success_count++;
                        BuildMove.move_time *= 0.95f;
                    }

                    float height = building_height / 2 + building.transform.position.y;
                    if (height > base_height + ScreenMoveCount * building_height)
                    {
                        ScreenMove = true;
                    }
                    else
                    { 
                        ScreenMove = false;
                    }
                    
                    generate_height = current_height + building_height / 2;
                    if(!ScreenMove)
                    {
                        building = null;
                    }
                }
            }
           
        }
    }

}
