using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PathologicalGames;
using System;
using PixelCrushers.DialogueSystem;
public class AXYControl : MonoBehaviour
{
    public GameObject AXYBody;
    public DragonBones.UnityArmatureComponent UnityArmature;
    public float xspeed = 0.0f;
    public float yspeed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        AXYBody.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        
            this.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
            this.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().localPosition.x - xspeed * Time.deltaTime, this.GetComponent<Transform>().localPosition.y, this.GetComponent<Transform>().localPosition.z);
            AXYBody.GetComponent<Transform>().localScale = new Vector3(0.2f, 0.2f, 0.2f);
            AXYBody.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().localPosition.x - xspeed * Time.deltaTime, this.GetComponent<Transform>().localPosition.y, this.GetComponent<Transform>().localPosition.z);
       
        if (Input.GetKey(KeyCode.D))
        {
            this.GetComponent<Transform>().localScale = new Vector3(-0.2f, 0.2f, 0.2f);
            this.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().localPosition.x + xspeed * Time.deltaTime, this.GetComponent<Transform>().localPosition.y, this.GetComponent<Transform>().localPosition.z);
            AXYBody.GetComponent<Transform>().localScale = new Vector3(0.2f, 0.2f, 0.2f);
            AXYBody.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().localPosition.x - xspeed * Time.deltaTime, this.GetComponent<Transform>().localPosition.y, this.GetComponent<Transform>().localPosition.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            this.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().localPosition.x, this.GetComponent<Transform>().localPosition.y + yspeed * Time.deltaTime, this.GetComponent<Transform>().localPosition.z);
            AXYBody.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().localPosition.x, this.GetComponent<Transform>().localPosition.y + yspeed * Time.deltaTime, this.GetComponent<Transform>().localPosition.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().localPosition.x, this.GetComponent<Transform>().localPosition.y - yspeed * Time.deltaTime, this.GetComponent<Transform>().localPosition.z);
            AXYBody.GetComponent<Transform>().localPosition = new Vector3(this.GetComponent<Transform>().localPosition.x, this.GetComponent<Transform>().localPosition.y - yspeed * Time.deltaTime, this.GetComponent<Transform>().localPosition.z);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            UnityArmature.animation.Play("侧面走路");
            AXYBody.SetActive(false);
        }
        if (Input.GetKey(KeyCode.X))
        {
            UnityArmature.animation.Play("侧面拿枪打下面");
            AXYBody.SetActive(false);
        }
        if (Input.GetKey(KeyCode.C))
        {
            UnityArmature.animation.Play("拖动(尸体)");
            AXYBody.SetActive(true);
        }
        if (Input.GetKey(KeyCode.V))
        {
            UnityArmature.animation.Play("侧面暂停");
            AXYBody.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            UnityArmature.animation.Play("侧面上楼梯（纯）");
            AXYBody.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Alpha6))
        {
            UnityArmature.animation.Play("侧面拿枪");
            AXYBody.SetActive(false);
        }
    }
}
