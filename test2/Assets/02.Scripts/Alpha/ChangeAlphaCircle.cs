using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAlphaCircle : MonoBehaviour {


    public float Speed = 100f; // 定义图片切换的速度
    public float Alpha1 = 255.0f;
    public float Alpha2 = 0f;
    private bool judge = true;
    public int blink;
    public bool real;
    public bool wind = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (real)
        {
            if (blink == 1)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed;
                    GetComponent<SpriteRenderer>().color = new Vector4(256f, 256f, 256f, Alpha1 / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed;
                    GetComponent<SpriteRenderer>().color = new Vector4(256f, 256f, 256f, Alpha2 / 256);


                }
            }
            if (blink == 2)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed;
                    GetComponent<SpriteRenderer>().color = new Vector4(256f, 256f, 256f, (255 - Alpha1) / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed;
                    GetComponent<SpriteRenderer>().color = new Vector4(256f, 256f, 256f, (255 - Alpha2) / 256);


                }
            }
            if (blink == 3)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed * 2f;
                    GetComponent<SpriteRenderer>().color = new Vector4(256, 256, 256, Alpha1  / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed * 2f;
                    GetComponent<SpriteRenderer>().color = new Vector4(256, 256, 256,  Alpha2 / 256);


                }
            }
            if (blink == 4)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed * 2f;
                    GetComponent<SpriteRenderer>().color = new Vector4(256, 256, 256, (255 - Alpha1) / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed * 2f;
                    GetComponent<SpriteRenderer>().color = new Vector4(256, 256, 256, (255 - Alpha2) / 256);


                }
            }
            if (blink == 5)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed * 4f;
                    GetComponent<SpriteRenderer>().color = new Vector4(256, 256, 256, Alpha1 / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed * 4f;
                    GetComponent<SpriteRenderer>().color = new Vector4(256, 256, 256, Alpha2 / 256);


                }
            }
            if (blink == 6)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed * 4f;
                    GetComponent<SpriteRenderer>().color = new Vector4(256, 256, 256, (255 - Alpha1) / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed * 4f;
                    GetComponent<SpriteRenderer>().color = new Vector4(256, 256, 256, (255 - Alpha2) / 256);


                }
            }
        }

        if (!real)
        {
            if (blink == 1)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed;
                    GetComponent<SpriteRenderer>().color = new Vector4(190f, 190f, 190f, Alpha1 / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed;
                    GetComponent<SpriteRenderer>().color = new Vector4(190f, 190f, 190f, Alpha2 / 256);


                }
            }

            if (blink == 2)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed;
                    GetComponent<SpriteRenderer>().color = new Vector4(190f, 190f, 190f, (255 - Alpha1) / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed;
                    GetComponent<SpriteRenderer>().color = new Vector4(190f, 190f, 190f, (255 - Alpha2) / 256);


                }
            }
            if (blink == 3)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed * 2f;
                    GetComponent<SpriteRenderer>().color = new Vector4(190, 190, 190,  Alpha1 / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed * 2f;
                    GetComponent<SpriteRenderer>().color = new Vector4(190, 190, 190,  Alpha2  / 256);


                }
            }
            if (blink == 4)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed * 2f;
                    GetComponent<SpriteRenderer>().color = new Vector4(190, 190, 190, (255 - Alpha1) / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed * 2f;
                    GetComponent<SpriteRenderer>().color = new Vector4(190, 190, 190, (255 - Alpha2) / 256);


                }
            }
            if (blink == 5)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed * 4f;
                    GetComponent<SpriteRenderer>().color = new Vector4(190, 190, 190, Alpha1 / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed * 4f;
                    GetComponent<SpriteRenderer>().color = new Vector4(190, 190, 190, Alpha2 / 256);


                }
            }
            if (blink == 6)
            {
                if (judge)
                {
                    Alpha1 = Alpha1 - Time.deltaTime * Speed * 4f;
                    GetComponent<SpriteRenderer>().color = new Vector4(190, 190, 190, (255 - Alpha1) / 256);

                }
                else if (!judge)
                {
                    Alpha2 = Alpha2 + Time.deltaTime * Speed * 4f;
                    GetComponent<SpriteRenderer>().color = new Vector4(190, 190, 190, (255 - Alpha2) / 256);


                }
            }
        }

        if(wind)
        {
            if (judge)
            {
                Alpha1 =Alpha1 - Time.deltaTime * Speed*0.5f;
                GetComponent<SpriteRenderer>().color = new Vector4(256f, 256f, 256f, (Alpha1*25) / 32768);

            }
            else if (!judge)
            {
                Alpha2 = Alpha2 + Time.deltaTime * Speed*0.5f;
                GetComponent<SpriteRenderer>().color = new Vector4(256f, 256f, 256f, (Alpha2 * 25) / 32768);


            }
        }

        if (Alpha1 <= 0)
        {
            Alpha1 = 255.0f;
            judge = false;
        }
        if (Alpha2 >= 255)
        {
            Alpha2 = 0f;
            judge = true;

        }

       

    }
    
}
