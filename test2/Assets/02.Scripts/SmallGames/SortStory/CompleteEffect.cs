using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteEffect : MonoBehaviour
{
    private Vector3 init_pos;
    private RectTransform rect;
    private Vector3 target_pos;
    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        target_pos = transform.GetChild(0).GetComponent<RectTransform>().position;
        if(rect==null)
        {
            Debug.Log(1);
        }
        init_pos = rect.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(rect.position);
        }
    }

    public void StartEffect()
    {
        gameObject.SetActive(true);
        StartCoroutine(MoveMask());
    }

    public IEnumerator MoveMask()
    { 
        while(rect.position.x!=target_pos.x)
        {
            float dist = 10 * Time.deltaTime;
            rect.Translate(Vector3.right * dist);
            
            rect.GetChild(0).Translate(Vector3.left * dist);
            

            if (Mathf.Abs(rect.position.x - target_pos.x) < 0.1f)
            {
                rect.position = new Vector3(target_pos.x, rect.position.y, rect.position.z);
                rect.GetChild(0).localPosition= Vector3.zero;
                break;
            }
            yield return null;
        }

    }
}
