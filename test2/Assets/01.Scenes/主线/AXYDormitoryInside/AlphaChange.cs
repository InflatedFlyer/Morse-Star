using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaChange : MonoBehaviour
{
    public string state;//state0:left->right   state1:right->left

    public float left;
    public float right;
    public float ChangeSpeed;
    public Color color1;
    public float startAlpha;
    // Start is called before the first frame update
    void Start()
    {
        color1 = this.transform.GetComponent<SpriteRenderer>().material.color;
        color1.a = startAlpha;
        state = "state0";
    }

    // Update is called once per frame
    void Update()
    {
        {
            if (state == "state0" && color1.a < right)
            {
                color1.a += ChangeSpeed * Time.deltaTime;
                this.transform.GetComponent<SpriteRenderer>().material.color = color1;
            }
            if (state == "state0" && color1.a >= right)
                state = "state1";
            if (state == "state1" && color1.a > left)
            {
                color1.a -= ChangeSpeed * Time.deltaTime;
                this.transform.GetComponent<SpriteRenderer>().material.color = color1;
            }
            if (state == "state1" && color1.a <= left)
                state = "state0";
        }
    }
}
