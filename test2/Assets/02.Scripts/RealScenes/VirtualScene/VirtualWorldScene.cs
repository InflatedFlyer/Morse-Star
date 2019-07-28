using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;
public class VirtualWorldScene : VirtualBaseScene
{

    public override void Load(Vector3 init_pos,bool isAsync = true)
    {
        base.Load(init_pos,isAsync);
    }

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

    public void Rotation(float angle)
    {
        ProCamera2D.Instance.transform.localRotation = Quaternion.Euler(0, 0, angle);
        virtual_liming.Rotation(angle);
    }
}
