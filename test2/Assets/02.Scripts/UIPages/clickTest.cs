using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.Log(ray);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject target = hit.collider.gameObject;//获得点击的物体
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(target.name);
            }
        }
    }
}
