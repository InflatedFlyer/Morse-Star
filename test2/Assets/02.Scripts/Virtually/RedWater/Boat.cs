using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {

    public float moveSpeed=-1f;

    private WaterManager water;
    private float boat_size;

	// Use this for initialization
	void Start () {
        water = GameObject.FindWithTag("RedWater").GetComponent<WaterManager>();
        Transform boat=transform.GetChild(0);
        boat_size=boat.GetComponent<SpriteRenderer>().size.x * boat.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
        Adjust();
        SlowMove();
	}

    //调整船的高度、角度
    private void Adjust()
    {
        //船的左右位置
        float left_pos = transform.position.x - boat_size / 2;
        float right_pos = transform.position.x + boat_size / 2;

        //调整高度和角度
        float water_level = water.WaterLevel(left_pos, right_pos);
        float angle = water.WaterAngle(left_pos, right_pos);
        
        transform.position = new Vector3(transform.position.x, water_level, transform.position.z);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle*0.5f));
        
    }

    //船慢慢前进
    private void SlowMove()
    {
        //船的左右位置
        float left_pos = transform.position.x - boat_size / 2;
        float right_pos = transform.position.x + boat_size / 2;
        
        //判断船是否越过水面
        if(!water.isInRange(left_pos)||!water.isInRange(right_pos))
        {
            return;
        }

        transform.Translate(Time.deltaTime* moveSpeed,0,0);
    }
}
