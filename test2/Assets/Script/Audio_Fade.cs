using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Fade : MonoBehaviour
{
    private AudioSource audio1;
    private AudioClip[] clip;
    private bool fin = false;
    private bool fout = false;
    [HideInInspector] public Dictionary<string, int> list;
    public float step=0.01f;
    public float max = 1;
    // Start is called before the first frame update
    void Start()
    {
        list = new Dictionary<string, int>();
        list.Add("手机", 1);
        list.Add("拖鞋", 2);
        list.Add("车站",4);
        audio1 = this.gameObject.GetComponent<AudioSource>();
        audio1.volume = 0;
        clip=Resources.LoadAll<AudioClip>("Music");
        
}

    // Update is called once per frame
    void Update()
    {
        if (fin)
        {
            audio1.volume += step;
            if (audio1.volume >= max)
            {
                audio1.volume = max;
                fin = false;
            }
        }

        if (fout)
        {
            audio1.volume -= step;
            if (audio1.volume<= 0)
            {
                audio1.volume = 0;
                fout = false;
                audio1.Stop();
            }
        }
    }


    public void SetMax(string x)
    {
        max = int.Parse(x)*0.1f;
    }
    public void Setv(string x)
    {
        audio1.volume = int.Parse(x) * 0.01f;
    }
    public void FadeIn(string str)
    {
        int x;
        x = list[str];
        audio1.clip = clip[x];
        fin = true;
        audio1.Play();
        
    }

    public void FadeOut(string str)
    {
        fout = true;
    }


}
