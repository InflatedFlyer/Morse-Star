using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIBackgroundBlur : MonoBehaviour
{
    private RenderTexture blur_bg;
    private Camera ui_camera;
    private RawImage rawImage;

    private void Awake()
    {
        ui_camera = GameObject.FindWithTag("UICamera").GetComponent<Camera>();
        rawImage=GetComponent<RawImage>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //BlurEffect.rawImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(ShowMenus());

    }

    private void OnDisable()
    {
    }

    private IEnumerator ShowMenus()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
       
        blur_bg = new RenderTexture(100, 100, 0);
        rawImage.texture = blur_bg;

        BlurEffect blurEffect = ui_camera.GetComponent<BlurEffect>();
        ui_camera.GetComponent<BlurEffect>().enabled = true;
        BlurEffect.rawImage = rawImage;
        
        yield return new WaitForSeconds(0.3f);

        ui_camera.GetComponent<BlurEffect>().enabled = false;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

}
