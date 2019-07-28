using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Picture
{
    public int pic_type;            //图片类型 空白0，起点1，直线2，直角3，三边4
    public int init_x, init_y;      //初始化位置 x:0-3,y:0-3
    public int target_x, target_y;  //目标位置 x:0-3,y:0-3 没有目标位置就填0
    public int direct;              //图片转向 0不旋转, 1顺时针90, 2顺时针180, 3逆时针90
}

public class JigsawGame : MonoBehaviour {

    public Transform background;//将背景图拖入其中
    public GameObject img_space; //空白图片
    public GameObject img_start; //起点
    public GameObject img_line;  //直线
    public GameObject img_corner;//直角
    public GameObject img_tri;   //三边

    public Picture[] pictures;  //初始化图片信息
    private ImageFlip[] images;

    public static float background_size=10;   //保存背景图片的大小
    public static float picture_size;         //图片大小
    private int row_picture_count=4;    //每行有几张图片

    public static bool[,] map;  //保存地图信息

    private bool isGameOver = false;
    // Use this for initialization
	void Start () {
        ChangeBackgroundScale();
        picture_size = background_size / row_picture_count;
        map = new bool[row_picture_count, row_picture_count];
        images = new ImageFlip[pictures.Length];
        InitMap();
	}
	
	// Update is called once per frame
	void Update () {
        isGameOver = GameOver();
	}

    //初始化地图信息
    public void InitMap()
    {
        for (int i=0;i<map.GetLength(0);i++)
        {
            for(int j=0;j<map.GetLength(1); j++)
            {
                map[i, j] = false;
            }
        }
        for(int i=0;i<pictures.Length;i++)
        {
            map[pictures[i].init_x, pictures[i].init_y] = true;
            GameObject temp_img=null;
            switch (pictures[i].pic_type)
            {
                case 0:
                    temp_img = Instantiate(img_space,Vector3.zero,Quaternion.identity);
                    break;
                case 1:
                    temp_img = Instantiate(img_start, Vector3.zero, Quaternion.identity);
                    break;
                case 2:
                    temp_img = Instantiate(img_line, Vector3.zero, Quaternion.identity);
                    break;
                case 3:
                    temp_img = Instantiate(img_corner, Vector3.zero, Quaternion.identity);
                    break;
                case 4:
                    temp_img = Instantiate(img_tri, Vector3.zero, Quaternion.identity);
                    break;
                default:
                    break;
            }

            if (temp_img != null)
            {
                ImageFlip img_flp = temp_img.GetComponent<ImageFlip>();
                img_flp.Init(pictures[i].init_x, pictures[i].init_y, pictures[i].target_x, pictures[i].target_y);
                img_flp.transform.rotation = Quaternion.Euler(0,0,-pictures[i].direct * 90);
                images[i] = img_flp;
            }
                
        }
    }

    //获取x,y点的坐标位置
    public static Vector2 GetPosition(int x,int y)
    {
        float pos_x = x * picture_size + picture_size / 2 - 5;
        float pos_y = -y * picture_size - picture_size / 2 + 5;
        return new Vector2(pos_x, pos_y);
    }

    //检查是否游戏结束
    public bool GameOver()
    {
        for(int i=0;i<images.Length;i++)
        {
            if(!images[i].IsDone())
            {
                return false;
            }
        }
        Debug.Log("game over");
        return true;
    }

    //将背景图片的大小改变为background_size*background_size
    private void ChangeBackgroundScale()
    {
        Vector2 current_size = background.gameObject.GetComponent<SpriteRenderer>().size;
        background.localScale = new Vector3(background_size / current_size.x, background_size / current_size.y,1);

        ////在四周加上碰撞器
        //int bound_size = 1;
        //BoxCollider boungd_left = background.gameObject.AddComponent<BoxCollider>();
        //boungd_left.center = new Vector3((-background_size / 2 - bound_size / 2) / background.localScale.x
        //    , background.position.y, background.position.z);
        //boungd_left.size = new Vector3(bound_size / background.localScale.x, background_size / background.localScale.y, 50);

        //BoxCollider boungd_right = background.gameObject.AddComponent<BoxCollider>();
        //boungd_right.center = new Vector3((background_size / 2 + bound_size / 2) / background.localScale.x
        //    , background.position.y, background.position.z);
        //boungd_right.size = new Vector3(1 / background.localScale.x, background_size / background.localScale.y, 50);

        //BoxCollider boungd_up = background.gameObject.AddComponent<BoxCollider>();
        //boungd_up.center = new Vector3(background.localScale.x
        //    , (background_size / 2 + bound_size / 2) / background.localScale.y, background.position.z);
        //boungd_up.size = new Vector3(1 / background.localScale.x, background_size / background.localScale.y, 50);

        //BoxCollider boungd_down = background.gameObject.AddComponent<BoxCollider>();
        //boungd_down.center = new Vector3(background.localScale.x
        //    , (-background_size / 2 - bound_size / 2) / background.localScale.y, background.position.z);
        //boungd_down.size = new Vector3(1 / background.localScale.x, background_size / background.localScale.y, 50);

    }
}
