using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MirrorCtr : MonoBehaviour
{
    public bool begin = false;
    private  bool B = false;
    public GameObject obj;
    private VideoPlayer video;
    // Start is called before the first frame update
    void Awake()
    {
        video =this. GetComponent<VideoPlayer>();
        video.Play();
        video.Pause();
    }
    // Update is called once per frame
    void Update()
    {
        if(begin&&!B)
        {
            A();
        }
    }
    private void Disappear()
    {
        this.gameObject.SetActive(false);
    }
    
    private void C()
    {
        obj.SetActive(false);
    }

    private void A()
    {
        B = true;
        GetComponent<VideoPlayer>().Play();
        //this.GetComponent<VideoPlayer>().enabled  = true;
        Invoke("Disappear", 8f);
        Invoke("C", 1f);
    }
}
