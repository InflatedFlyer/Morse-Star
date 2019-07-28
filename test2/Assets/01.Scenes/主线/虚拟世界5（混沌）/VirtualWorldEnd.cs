using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PixelCrushers.DialogueSystem;
public class VirtualWorldEnd : BaseScene 
{
    // Start is called before the first frame update
    public bool hasE = false;
    public DragonBones.UnityArmatureComponent UnityArmature;

    protected override void Awake()
    {
        base.Awake();
        _name = "VirtualWorldEnd";
    }

    public override void Load(Vector3 init_pos, bool isAsync = true)
    {
        EventManager.getInstance().StartListening("HUD",HUDTrigger);
        base.Load(init_pos, isAsync);
    }

    public override void SceneFlow(Operate.Command operate)
    {
        base.SceneFlow(operate);
    }

    public override void Close()
    {
        base.Close();
        EventManager.getInstance().StartListening("HUD",HUDTrigger);
    }
    public override void LoadSuccess()
    {
        base.LoadSuccess();
     
    }
    public void HUDTrigger()
    {
        StartCoroutine(HUD());
    }

    private IEnumerator HUD()
    {
        liming.LoseControl();
        OverLay overlay = GameObject.Find("OverLay").GetComponent<OverLay>();
        while(overlay .gameObject .activeSelf )
        {
            yield return null;
        }
        AudioSource audiosource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        audiosource.Play();
        liming.GetControl();
        yield return new WaitForSeconds(270f);
        GameObject obj = GameObject.Find("礼服黎茗");
        obj.transform.position = new Vector3(liming.transform.position.x + 25,liming.transform .position .y,liming.transform .position .z);
        obj.gameObject.SetActive(true);
        UnityArmature.animation.Play("走路1");
     }
}
