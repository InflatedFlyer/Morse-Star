using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    public GameObject mainMenu;

    public static UIControl instance;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    public static UIControl getInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMainMenuBtnClick()
    {
        //if (mainMenu.activeSelf)
        //{
        //    GameObject.FindWithTag("Character").GetComponent<WalkCharacter>().ReSumeLastState();
        //}
        //else
        //{
        //    GameObject.FindWithTag("Character").GetComponent<WalkCharacter>().LoseControl();
        //}
        mainMenu.SetActive(!mainMenu.activeSelf);
    }

    public void LinkToMainCamera()
    {
        if (Camera.main != null)
        {
            transform.parent = Camera.main.transform;
            transform.localPosition = Vector3.zero;
            transform.Find("MainMenuCanvas").transform.localScale = new Vector3(Camera.main.orthographicSize / 5, Camera.main.orthographicSize /5, 1);
            transform.GetComponentInChildren<Camera>().orthographicSize = Camera.main.orthographicSize;

        }
        else
        {
            transform.parent = GameObject.FindWithTag("SpawnPool").transform;
        }
    }

    public void LinkToMainManager()
    {
        transform.parent = GameObject.FindWithTag("SpawnPool").transform;
    }
    
}
