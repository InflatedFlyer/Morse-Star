using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walktest : MonoBehaviour
{
    public DragonBones.UnityArmatureComponent UnityArmature;
    public float xspeed;
    // Start is called before the first frame update
    public string state;
    void Start()
    {
        UnityArmature = GetComponent<DragonBones.UnityArmatureComponent>();
        UnityArmature.animation.Play("侧面走路");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            state = "侧面走路";
            if (Input.GetKey(KeyCode.A))
            {
                transform.localScale = new Vector3(0.3060944f, 0.3060944f, 0.3060944f);
                transform.position -= new Vector3(xspeed, 0, 0)*Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.localScale = new Vector3(-0.3060944f, 0.3060944f, 0.3060944f);
                transform.position += new Vector3(xspeed, 0, 0)*Time.deltaTime;
            }
        }
        else
            state = "stop";

        if(state=="stop")
            UnityArmature.animation.Play("暂停");
        if (state == "move")
            UnityArmature.animation.Play("侧面走路");
    }
}
