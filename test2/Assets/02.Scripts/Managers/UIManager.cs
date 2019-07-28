using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager instance = null;
    private Dictionary<string, Transform> ui_dictionary;

    public static UIManager getInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        ui_dictionary = new Dictionary<string, Transform>();
    }

    void Start()
    {
    }

    public bool AddUI(Transform ui)
    {
        if(ui==null||ui_dictionary==null)
        {
            Debug.Log("ui page is null or ui_directionary is null");
            return false;
        }
        if(ui_dictionary.ContainsKey(ui.name))
        {
            Debug.Log(string.Format("UI Page:{0} exists already.",ui.name));
            return true;
        }
        else
        {
            ui_dictionary.Add(ui.name, ui);
            return true;
        }
    }

    public bool RemoveUI(string name)
    {
        GameObject target = getUI(name);
        if (target == null)
        {
            return false;
        }
        else
        {
            ui_dictionary.Remove(name);
            return true;
        }
    }

    public GameObject getUI(string name)
    {
        Transform result;
        if (!ui_dictionary.TryGetValue(name, out result))
        {
            Debug.LogWarning(string.Format("No the UI:{0}", name));
            return null;
        }
        return result.gameObject;
    }

    public void Show(string UIName)
    {
        GameObject target = getUI(UIName);
        if(target==null)
        {
            return;
        }
        target.SetActive(true);
    }

    public void Show(string UIName, float x,float y)
    {
        GameObject target = getUI(UIName);
        if (target == null)
        {
            return;
        }
        RectTransform ui_transform = (RectTransform)(target.transform);
        ui_transform.position = new Vector2(x, y); 
        target.SetActive(true);
    }

    public void Close(string UIName)
    {
        GameObject target = getUI(UIName);
        if (target != null)
        {
            target.SetActive(false);
        }
    }
}