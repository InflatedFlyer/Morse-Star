using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xunishijiexuanzhuankaiguan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject .tag  == "WorldRotarySwitch")
        {
            GameObject.Find("GameObject").GetComponent<RotAB>().RC = true;
            GameObject.Find("GameObject").GetComponent<RotAB>().RTarget = new Vector3( 0, 0, 90f);
            GameObject.Find("GameObject").GetComponent<RotAB>().RSpeed = new Vector3(0, 0, 1f);
        }
    }
}
