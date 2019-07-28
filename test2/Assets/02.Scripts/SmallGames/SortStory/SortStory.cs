using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortStory : MonoBehaviour
{
    public Transform scroll_menu;
    public Transform sort_page;

    //图片详细信息对象
    public Transform detail_page;
    //图片区对象
    public Transform image_area;
    //文本区对象
    public Transform text_area;
    //完成拼图特效对象
    public Transform complete_effect;

    //16个格子的位置
    public static List<Vector3> positions;
    //16张图片
    private static ImageMove[] images;

    //控制特效的脚本
    private CompleteEffect[] complete_effects;

    //目前完成数量
    private int level;
    private bool isGameOver=false;
    void Awake()
    {
        level = 0;
        

        

        images = image_area.GetComponentsInChildren<ImageMove>();

        complete_effects = complete_effect.GetComponentsInChildren<CompleteEffect>();
        for(int i=0;i<complete_effects.Length;i++)
        {
            complete_effects[i].gameObject.SetActive(false);
        }
    }



    void Update()
    {
     
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    for(int i=0;i<positions.Count;i++)
        //    {
        //        Debug.Log(i + ":" + positions[i]);
        //    }
        //}

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            HideDetail();
        }
    }

    public void ReSortImage(int start,int end, int direct)
    {
        for(int i=start;i<=end;i++)
        {
            images[i].current_index += direct;
        }
        ReSortChildAndMove();
        Invoke("CheckState", 0.5f);
    }

    public void ReSortChildAndMove()
    {
        for(int i=0;i<images.Length;i++)
        {
            images[i].transform.SetSiblingIndex(images[i].current_index);
        }

        images = image_area.GetComponentsInChildren<ImageMove>();

        for(int i=0;i<images.Length;i++)
        {
            StartCoroutine(images[i].MoveToPoint());
        }
    }

    public bool CheckState()
    {
        //检查是否完成
        int last_index = ((level + 1) * 4 > images.Length ? images.Length : (level + 1) * 4);
        for (int i = level * 4; i < last_index; i++)
        {
            if (images[i].current_index != images[i].target_index
                || images[i].current_index != images[i].transform.GetSiblingIndex())
            {
                return false;
            }
        }

        //如果完成，播放特效
        
        for (int i = level * 4; i < last_index; i++)
        {
            images[i].isComplete = true;
        }
        complete_effects[level].StartEffect();

        level++;
        //检查下一排是否完成
        if (level < 4)
        {
            Invoke("CheckState",1f);
        }
        else
        {
            Invoke("GameOver", 2f);
        }
        return true;
    }

    public void ShowDetail(Sprite image,string description)
    {
        text_area.gameObject.SetActive(false);
        detail_page.gameObject.SetActive(true);
        detail_page.GetComponent<DetailPage>().SetContent(image, description);
    }

    public void HideDetail()
    {
        detail_page.gameObject.SetActive(false);
        text_area.gameObject.SetActive(true);
    }
    

    public void ShowScrollMenu()
    {

        positions = new List<Vector3>();
        
        
        scroll_menu.gameObject.SetActive(true);
    }

    public void ChangeToSortPage()
    {
        int img_count = image_area.childCount;

        for (int i = 0; i < img_count; i++)
        {
            positions.Add(image_area.GetChild(i).transform.position);
        }
        scroll_menu.gameObject.SetActive(false);
        sort_page.gameObject.SetActive(true);
    }

    public void GameOver()
    {
        gameObject.SetActive(false);
    }
}