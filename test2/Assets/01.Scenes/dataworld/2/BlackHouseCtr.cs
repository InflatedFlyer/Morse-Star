using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BlackHouseCtr : MonoBehaviour
{
    public bool begin = false;
    private bool isShowed=false;
    public GameObject dormitory_go;
    private VideoPlayer video;
    // Start is called before the first frame update
    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if(begin&&!isShowed)
        {

            Begin();
        }
        
    }

    private void Disappear()
    {
        gameObject.SetActive(false);
    }

    private void Begin()
    {
        isShowed = true;
        dormitory_go.SetActive(false);
        GetComponent<VideoPlayer>().Play();
        //this.GetComponent<VideoPlayer>().enabled  = true;
        Invoke("Disappear", 7f);
    }
}
