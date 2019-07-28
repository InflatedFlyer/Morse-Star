using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Com.LuisPedroFonseca.ProCamera2D;

public class VirtualWorldMirrorCh1 : BaseScene
{
    protected override void Awake()
    {
        base.Awake();
        _name = "Virtuallyworld(Mirror)";
    }
    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        EventManager.getInstance().StartListening("镜面", MirrorTrigger);
        base.Load(init_pos, isAsync);
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
        //StartCoroutine(WakeUpAnimation());
    }

    public void MirrorTrigger()
    {
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
