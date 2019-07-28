using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewNoteTrail : TrailEffect
{
    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {

            Debug.Log(parent_rect.rect);
            Debug.Log(parent_rect.lossyScale);
        }
    }

    public override void InitPaths()
    {
        base.InitPaths();

        Vector3 center = parent_rect.position;

        Debug.Log(center);
        Debug.Log(parent_width);
        Debug.Log(parent_height);

        paths[0] = new Vector3[3];
        paths[0][0] = center + new Vector3(-parent_width / 2 - 4, parent_height / 2 + 1);
        paths[0][1] = center + new Vector3(-parent_width / 2 - 4, -parent_height / 2 - 4);
        paths[0][2] = center + new Vector3(parent_width / 2 + 1, -parent_height / 2 - 4);

        paths[1] = new Vector3[3];
        paths[1][0] = center + new Vector3(parent_width / 2 + 4, -parent_height / 2 - 1);
        paths[1][1] = center + new Vector3(parent_width / 2 + 4, parent_height / 2 + 4);
        paths[1][2] = center + new Vector3(-parent_width / 2 - 1, parent_height / 2 + 4);
    }
}
