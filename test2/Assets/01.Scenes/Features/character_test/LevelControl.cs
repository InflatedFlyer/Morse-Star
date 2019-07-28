using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class LevelControl : MonoBehaviour {

    private LevelManager level_manager;

    public Transform video_obj;
    private VideoPlayer video;

    private float startTime;
    
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        level_manager = LevelManager.getInstance();
        video = video_obj.GetComponent<VideoPlayer>();
        StartCoroutine(LevelChange());
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    private IEnumerator LevelChange()
    {
        video.Play();
        Invoke("HideVideo", (float)(video.clip.length) + 0.1f);
        level_manager.LoadSceneAsync(5,false);
        yield return new WaitForSeconds(2.5f);
        level_manager.AllowActivation();
    }

    private void HideVideo()
    {
        video_obj.parent.gameObject.SetActive(false);
    }
}
