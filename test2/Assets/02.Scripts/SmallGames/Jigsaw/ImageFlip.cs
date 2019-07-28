using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageFlip : MonoBehaviour {

    private int current_x, current_y;   //当前位置
    private int target_x, target_y;     //期望位置
    private int[,] d = new int[,] { { -1, 0 }, { 0, -1 }, { 1, 0 }, { 0, 1 } };
    public int move_direct  //可移动方向，-1代表不能移动，左0，上1，右2，下3
    {
        get
        {
            for(int i=0;i<4;i++)
            {
                if(CheckDirect(i))
                {
                    return i;
                }
            }
            return -1;
            
        }
    }

    private bool isDrag, isAjusted;
	// Use this for initialization
	void Start () {
        isAjusted = true;
        isDrag = false;
	}
	
	// Update is called once per frame
	void Update () {
        //if(!isDrag&&Input.GetMouseButton(0))
        //{
        //    Vector3 mouse_pos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    mouse_pos = new Vector3(mouse_pos.x, mouse_pos.y, transform.position.z);
        //    if(Vector3.Distance(mouse_pos
        //        ,transform.position)<JigsawGame.picture_size/2)
        //    {
        //        StartCoroutine(MouseDown());
        //    }
        //}
	}

    //初始化每张图片的信息
    public void Init(int init_x,int init_y,int target_x,int target_y)
    {
        //设置位置
        this.current_x = init_x;
        this.current_y = init_y;
        this.target_x = target_x;
        this.target_y = target_y;

        transform.position = JigsawGame.GetPosition(current_x, current_y);

        //改变图片大小
        Vector2 current_size = GetComponent<SpriteRenderer>().size;
        float picture_size = JigsawGame.picture_size;
        transform.localScale = new Vector3(picture_size / current_size.x*0.99f, picture_size / current_size.y*0.99f, 50);
        
    }

    public bool IsDone()
    {
        if(target_x==-1||target_y==-1)
        {
            return true;
        }
        return current_x == target_x && current_y == target_y;
    }
    
    private IEnumerator OnMouseDown()
    {
        isDrag = true;
        isAjusted = false;
        JigsawGame.map[current_x, current_y] = false;
        Vector3 offset = transform.position-Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 last_mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        while (Input.GetMouseButton(0))
        {
            int close_x = (int)(Mathf.Round((transform.position.x + 3.75f) / JigsawGame.picture_size));
            int close_y = (int)(Mathf.Round((-transform.position.y + 3.75f) / JigsawGame.picture_size));
            if(Vector3.Distance(JigsawGame.GetPosition(close_x,close_y),transform.position)<0.1f)
            {
                current_x = close_x;
                current_y = close_y;
            }
            int direct = move_direct;
            Vector3 current_mouse= Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 move=current_mouse - last_mouse;
            
            float move_x, move_y;
            if(direct==-1)
            {
                move = Vector3.zero;
            }
            else
            {
                if(move.x*d[direct,0]<=0)
                {
                    move_x = 0;
                }
                else
                {
                    move_x = move.x;
                }
                if(move.y*d[direct,1]>=0)
                {
                    move_y = 0;
                }
                else
                {
                    move_y = move.y;
                }
                move = new Vector3(move_x, move_y);
                
            }
            transform.position = transform.position+move.normalized*0.1f;
            last_mouse = current_mouse;
            yield return new WaitForFixedUpdate();
        }
        current_x = (int)(Mathf.Round((transform.position.x + 3.75f) / JigsawGame.picture_size));
        current_y = (int)(Mathf.Round((-transform.position.y + 3.75f) / JigsawGame.picture_size));
        JigsawGame.map[current_x, current_y] = true;
        
        isDrag = false;
        StartCoroutine(AdjustPosition());
    }

    private IEnumerator AdjustPosition()
    {
        //期望位置
        Vector3 target = JigsawGame.GetPosition(current_x, current_y);

        //移动方向
        Vector3 direct = target - transform.position;
        while (!isDrag && !isAjusted)
        {
            if (Vector3.Distance(transform.position,target)<0.1f)
            {
                transform.position = target;
                isAjusted = true;
            }
            else
            {
                transform.position = transform.position + direct * 0.1f;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private bool CheckDirect(int direct)
    {
        int pos_x = current_x + d[direct, 0];
        int pos_y = current_y + d[direct, 1];
        if(pos_x<0||pos_x>=4||pos_y<0||pos_y>=4)
        {
            return false;
        }
        return !JigsawGame.map[pos_x, pos_y];
        //Ray ray = new Ray(transform.position, new Vector3(d[direct, 0], d[direct, 1]));
        //RaycastHit hit;
        //bool isHit=Physics.Raycast(ray,out hit);
        //if(!isHit)
        //{
        //    return false;
        //}
        //return hit.distance > JigsawGame.picture_size / 2;
    }
}
