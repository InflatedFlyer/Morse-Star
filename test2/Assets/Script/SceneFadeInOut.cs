using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;
    public bool sceneStarting = true,sceneEnding;
    private RawImage rawImage;
    public int scenenum;
    void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (sceneStarting)
            StartScene();
        if (sceneEnding)
            EndScene(scenenum);
    }

    private void FadeToClear()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    private void FadeToBlack()
    {
        rawImage.color = Color.Lerp(rawImage.color, Color.black, fadeSpeed * Time.deltaTime);
    }

    void StartScene()
    {
        FadeToClear();
        if (rawImage.color.a < 0.01f)
        {
            rawImage.color = Color.clear;
            rawImage.enabled = false;
            sceneStarting = false;
        }
    }

    public void EndScene(int x)
    {
        rawImage.enabled = true;
        FadeToBlack();
        if (rawImage.color.a > 0.99f)
        {
            SceneManager.LoadScene(x);
            sceneEnding = false;
            sceneStarting = true;
        }
    }

    void OnDestroy()
    {

    }
}
