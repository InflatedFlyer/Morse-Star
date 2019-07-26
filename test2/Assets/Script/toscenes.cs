using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "toscenes")]
public class toscenes : ScriptableObject
{
    // Start is called before the first frame update
    private SceneFadeInOut fadescene;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScence(int x)
    {
        fadescene = GameObject.Find("RawImage").GetComponent<SceneFadeInOut>();
        fadescene.scenenum = x;
        fadescene.sceneEnding = true;
    }

   
}
