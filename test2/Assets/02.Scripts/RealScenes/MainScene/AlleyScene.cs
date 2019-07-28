using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyScene : BaseScene
{
    public override void SceneFlow(Operate.Command operate)
    {
        base.SceneFlow(operate);
    }

    public override void Save(string filename, string extend_filename = null)
    {
        base.Save(filename, extend_filename);
    }

    public override void LoadSceneFile(string filename)
    {
        base.LoadSceneFile(filename);
    }

    public override void Close()
    {
        base.Close();
    }

    public override void LoadSuccess()
    {
        base.LoadSuccess();
    }
    
}
