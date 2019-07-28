using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageItemActive : MonoBehaviour
{
    private Transform white_light;
    private float speed=5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        white_light = transform.Find("white_light");
        StartCoroutine(WhiteLight());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator WhiteLight()
    {
        float start_x = -165;
        white_light.localPosition = new Vector3(start_x, 0, 0);
        while(true)
        {
            white_light.Translate(Vector3.right*speed * Time.deltaTime);
            if(white_light.localPosition.x>-start_x)
            {
                white_light.localPosition = new Vector3(start_x, 0, 0);
            }
            yield return null;
        }
    }
}
