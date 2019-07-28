using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIsManager : MonoBehaviour
{
    public GameObject canvas;
    //主菜单
    public GameObject mainMenu;
    private MainMenuController mainMenuCtrl;

    //设置菜单
    public GameObject setMenu;
    private SetMenuController setMenuCtrl;

    public GameObject sceneTitle;
    private SceneNoteController sceneNoteCtrl;

    public GameObject talkDialogPrefab;
    public GameObject itemNotePrefab;

    public Sprite sprite;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");

        mainMenuCtrl = mainMenu.GetComponent<MainMenuController>();
        setMenuCtrl = setMenu.GetComponent<SetMenuController>();
        sceneNoteCtrl = sceneTitle.GetComponent<SceneNoteController>();

        //ShowItemNote(sprite, "哈哈哈哈哈哈哈哈哈哈或或或");
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Escape))
        //{ 
        //    if(mainMenuCtrl.gameObject.activeSelf)
        //    {
        //        HideMainMenu();
        //    }
        //    else
        //    {
        //        ShowMainMenu();
        //    }
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (sceneNoteCtrl.isShow())
            {
                CloseSceneNote();
            }
            else
            {
                ShowSceneNote("新的回忆录");
            }
        }
    }

    public void ShowMainMenu()
    {
        mainMenuCtrl.Show();
    }

    public void HideMainMenu()
    {
        setMenuCtrl.Hide();
        mainMenuCtrl.Hide();
    }

    public void ShowSceneNote(string text)
    {
        ShowSceneNote(text,Vector3.zero);
    }

    public void ShowSceneNote(string text,Vector3 pos)
    {
        sceneNoteCtrl.SetText(text);
        sceneNoteCtrl.ShowInPos(pos);
    }

    public void CloseSceneNote()
    {
        sceneNoteCtrl.Hide();
    }

    public void ShowTalkDialog(int expression,string content,Vector3 pos)
    {
        GameObject go = Instantiate(talkDialogPrefab, pos,Quaternion.identity,canvas.transform);
        TalkDialogController talk_dialog = go.GetComponent<TalkDialogController>();
        if (talk_dialog != null)
        {
            talk_dialog.Talk(expression, content);
        }
    }
    public void ShowItemNote(Sprite sprite, string content)
    {
        ShowItemNote(sprite, content, Vector3.zero);
    }

    public void ShowItemNote(Sprite sprite,string content,Vector3 pos)
    {
        GameObject go = Instantiate(itemNotePrefab, pos, Quaternion.identity, canvas.transform);
        ItemNoteController item_note = go.GetComponent<ItemNoteController>();
       
        if (item_note!=null)
        {
            Debug.Log("1");
            item_note.SetContent(sprite, content);
        }
    }

    public void OnSetBtnClick()
    {
        //Debug.Log(setMenuCtrl.isShow());
        if(!setMenuCtrl.isShow())
        {
            setMenuCtrl.Show();
        }
        else
        {
            setMenuCtrl.Hide();
        }
    }

    public void OnContinueBtnClick()
    {
        mainMenuCtrl.Hide();
    }

    public void OnExitBtnClick()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
