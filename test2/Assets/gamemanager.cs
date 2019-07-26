using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class gamemanager : MonoBehaviour
{
    // Start is called before the first frame update
    public static gamemanager gameInstance;
    private SceneFadeInOut fadescene;
    void Awake()
    {
        if (gameInstance == null)
        {
            gameInstance = this;
        }
        else if (gameInstance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tosence(int x)
    {
        //SceneManager.LoadScene(x);
        fadescene = GameObject.Find("RawImage").GetComponent<SceneFadeInOut>();
        fadescene.scenenum = x;
        fadescene.sceneEnding = true;
    }
}
