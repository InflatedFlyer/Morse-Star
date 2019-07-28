using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
public class ImageMove : MonoBehaviour, IDragHandler,IPointerDownHandler,IPointerUpHandler
{
    private RectTransform curRecTran;
    public bool isDrag
    {
        private set;
        get;
    }

    //正确顺序
    public int target_index;
    //当前顺序
    public int current_index;
    //是否已经是真实顺序
    public bool isComplete;


    //悬停时间
    private float stay_time;

    private SortStory sort;
    private void Awake()
    {
        target_index = transform.GetSiblingIndex();

        curRecTran = transform.GetComponent<RectTransform>();
        current_index = transform.GetSiblingIndex();
        isComplete = false;

        sort = GameObject.Find("SortStory").GetComponent<SortStory>();
    }

    
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isComplete)
            {
                Vector3 globalMousePos;
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(curRecTran, eventData.position,

                eventData.pressEventCamera, out globalMousePos))

                {
                    curRecTran.position = globalMousePos;
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isComplete)
            {
                curRecTran.SetAsLastSibling();
                isDrag = true;
            }
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            ImageItemCtrl image_info = GetComponent<ImageItemCtrl>();
            sort.ShowDetail(image_info.GetImage(), image_info.description);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (!isComplete)
            {
                isDrag = false;
                StartCoroutine(MoveToPoint());
            }
        }
    }

    //移动到对应位置
    public IEnumerator MoveToPoint()
    {
        if (!isDrag)
        {
            Vector3 target_pos = SortStory.positions[current_index];
            float current_dist = Vector3.Distance(curRecTran.position, target_pos);
            float speed = current_dist / 0.2f;
            Vector3 direct = (target_pos - curRecTran.position).normalized;
            while (true)
            {
                curRecTran.Translate(direct * speed * Time.deltaTime);
                if (Vector3.Distance(curRecTran.position, target_pos) < 1f)
                {
                    curRecTran.position = target_pos;
                    break;
                }
                yield return null;
            }
        }
        yield return null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isDrag&&!isComplete)
        {
            stay_time = 0;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(!isDrag && !isComplete)
        {
            stay_time = 0;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(!isDrag && !isComplete)
        {
            stay_time += Time.deltaTime;
            if(stay_time>0.5f)
            {
                stay_time = 0;
                int direct = 0;
                int other_index = other.GetComponent<ImageMove>().current_index;

                other.GetComponent<ImageMove>().current_index = this.current_index;

                if (other_index>this.current_index)
                {
                    sort.ReSortImage(this.current_index, other_index - 1, 1);
                }
                else if (other_index < this.current_index)
                {
                    sort.ReSortImage(other_index+1, this.current_index, -1);
                }
            }
        }
    }
}
