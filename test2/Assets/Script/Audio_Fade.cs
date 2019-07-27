using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Fade : MonoBehaviour
{
    private AudioSource audio1;
    private bool fin = false;
    private bool fout = false;

    public float step=0.16f;

    // Start is called before the first frame update
    void Start()
    {
        audio1 = this.gameObject.GetComponent<AudioSource>();
        audio1.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (fin)
        {
            for (; audio1.volume < 1; audio1.volume += step)
            {
                if (audio1.volume > 1)
                    audio1.volume = 1;
            }
            fin = false;
        }

        if (fout)
        {
            for (; audio1.volume > 0; audio1.volume -= step)
            {
                if (audio1.volume < 0)
                    audio1.volume = 0;
            }
            fout = false;
        }
    }

    public void FadeIn(string str)
    {
        fin = true;
    }

    public void FadeOut(string str)
    {
        fout = true;
    }
}
