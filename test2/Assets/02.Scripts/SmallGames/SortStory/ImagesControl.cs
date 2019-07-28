using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CJFinc.UItools;
using UnityEngine.UI;
public class ImagesControl : MonoBehaviour
{
    public GameObject texts;
    public ScrollRect text_scrollrect;

    private UIStateGroupControl item_group;
    private UIStateItem[] image_items;
    private UIStateItemMirror[] images_mirror;

    private UIStateItem[] texts_item;
    // Start is called before the first frame update
    void Start()
    {
        item_group = GetComponent<UIStateGroupControl>();
        image_items = GetComponentsInChildren<UIStateItem>();
        images_mirror = GetComponentsInChildren<UIStateItemMirror>();
        
        texts_item =texts.GetComponentsInChildren<UIStateItem>();

        if(images_mirror.Length!=texts_item.Length)
        {
            Debug.LogError("图片和文本数量不匹配！！！");
        }
        else
        {
            for(int i=0;i<images_mirror.Length;i++)
            {
                images_mirror[i].mirrorItems = new UIStateItem[] { texts_item[i] };
            }
        }

        item_group.Init(true);
        texts.GetComponent<UIStateGroup>().Init(true);
        item_group.SetStateForAllItems(UIStateItem.STATE_DISABLED);

        item_group.SetStateForItem(UIStateItem.STATE_ACTIVE, transform.GetChild(0).gameObject.name);
        item_group.SetStateExceptItem(UIStateItem.STATE_DISABLED, transform.GetChild(0).gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnScrollMouse(Vector2 pos)
    {
        int child_count=transform.childCount-(int)(transform.childCount * pos.y+0.5f);
        child_count=Mathf.Clamp(child_count, 0, transform.childCount - 1);

        item_group.SetItemActive(transform.GetChild(child_count).name);
        for(int i=child_count+1;i<transform.childCount;i++)
        {
            image_items[i].SetStateDefault();
        }

        text_scrollrect.verticalNormalizedPosition = pos.y;
    }

}
