using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityScript;
using UnityEngine.UI;
using PathologicalGames;
using System;
using PixelCrushers.DialogueSystem;
public class LimingInvert : MonoBehaviour
{
    public GameObject Liming;
    private DragonBones.UnityArmatureComponent unityArmature1;
    private DragonBones.UnityArmatureComponent unityArmature2;
    public string playinganimation1;
    public string playinganimation2;
    // Start is called before the first frame update
    void Start()
    {
        unityArmature1 = Liming.GetComponent<DragonBones.UnityArmatureComponent>();
        unityArmature2 = this.GetComponent<DragonBones.UnityArmatureComponent>();
        playinganimation1 = unityArmature1.animationName;
        unityArmature2.animation.Play(playinganimation1);
    }

    // Update is called once per frame
    void Update()
    {
        playinganimation2 = playinganimation1;
        playinganimation1 = unityArmature1.animationName;
        if(playinganimation1!=playinganimation2)
        unityArmature2.animation.Play(playinganimation1);

    }
}
