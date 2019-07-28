using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Contents
{
    public string content;
    public Color font_color;
    public int font_size;
    public TextAnchor font_align;
}

public class blackScene : MonoBehaviour
{
    public List<Contents> texts;
    private GameObject text_go;
    private int current=-1;

    private float last_time;
    // Start is called before the first frame update
    void Awake()
    {
        text_go = GameObject.Find("note");
        last_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        last_time += Time.deltaTime;
        if(last_time>5f)
        {
            Next();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Next();
        }
    }

    public void Next()
    {
        current++;
        ShowText();
    }

    public void ShowText()
    {
        last_time = 0;
        text_go.SetActive(false);
        if (current < texts.Count)
        {
            Text current_text = text_go.GetComponent<Text>();
            current_text.color = texts[current].font_color;
            current_text.fontSize = texts[current].font_size;
            current_text.alignment = texts[current].font_align;
            StartCoroutine(AddContent(texts[current].content));
        }
        text_go.SetActive(true);
    }

    public IEnumerator AddContent(string content)
    {
        Text current_text = text_go.GetComponent<Text>();
        current_text.text = "";
        int i = 0;
        while (i < content.Length)
        {
            current_text.text += content[i++];
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
