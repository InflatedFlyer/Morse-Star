using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mousechange : MonoBehaviour
{
    // Start is called before the first frame update
    public Texture2D cursor;
    public Texture2D cursor_Drag;
    private bool isclick = false;
    public CursorMode c = CursorMode.Auto;//渲染形式，auto为平台自适应显示
    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, c);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Cursor.SetCursor(cursor_Drag, Vector2.zero, c);
        //}
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(cursor_Drag, Vector2.zero, c);
        }
            if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursor, Vector2.zero, c);
        }

    }
}
