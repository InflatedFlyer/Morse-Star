using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PianoCtrL :MonoBehaviour
{
    private Queue<int> user_input = new Queue<int>();
    private AudioSource[] piano_keys;
    
    private int[] Ans = { 5,4,7,4,5,4,2,4};

    private void Awake()
    {
        
        
    }
    
    private void Start()
    {
        EventManager.getInstance().StartListening("P1", P1Trigger);
        EventManager.getInstance().StartListening("P2", P2Trigger);
        EventManager.getInstance().StartListening("P3", P3Trigger);
        EventManager.getInstance().StartListening("P4", P4Trigger);
        EventManager.getInstance().StartListening("P5", P5Trigger);
        EventManager.getInstance().StartListening("P6", P6Trigger);
        EventManager.getInstance().StartListening("P7", P7Trigger);
        EventManager.getInstance().StartListening("P8", P8Trigger);
    }

    private void OnDisable()
    {
        EventManager.getInstance().StopListening("P1", P1Trigger);
        EventManager.getInstance().StopListening("P2", P2Trigger);
        EventManager.getInstance().StopListening("P3", P3Trigger);
        EventManager.getInstance().StopListening("P4", P4Trigger);
        EventManager.getInstance().StopListening("P5", P5Trigger);
        EventManager.getInstance().StopListening("P6", P6Trigger);
        EventManager.getInstance().StopListening("P7", P7Trigger);
        EventManager.getInstance().StopListening("P8", P8Trigger);
    }

    public void StartGame()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        piano_keys = GetComponentsInChildren<AudioSource>();
    }
    
    public void P1Trigger()
    {
        UserPressKey(1);
    }
    public void P2Trigger()
    {
        UserPressKey(2);
    }
    public void P3Trigger()
    {
        UserPressKey(3);
    }
    public void P4Trigger()
    {
        UserPressKey(4);

    }
    public void P5Trigger()
    {
        UserPressKey(5);
    }

    public void P6Trigger()
    {
        UserPressKey(6);
    }
    public void P7Trigger()
    {
        UserPressKey(7);
    }
    public void P8Trigger()
    {
        UserPressKey(8);
    }

    public void UserPressKey(int key)
    {
        Debug.Log(string.Format("user press key{0}", key));
        Debug.Log(piano_keys.Length);
        if(key-1>=0&&key-1<piano_keys.Length)
        {
            piano_keys[key - 1].Play();

            user_input.Enqueue(key);
            if (user_input.Count > Ans.Length)
            {
                user_input.Dequeue();
            }

            if(DetectCorrect())
            {
                gameObject.SetActive(false);
            }
        }
    }

    public bool DetectCorrect()
    {
        int[] current_seq = user_input.ToArray();
        Debug.Log(current_seq.Length);
        Debug.Log(current_seq.ToString());
        
        if(current_seq.Length!=Ans.Length)
        {
            return false;
        }
        for (int i=0;i<Ans.Length;i++)
        {

            if(Ans[i]!=current_seq[i])
            {
                return false;
            }
        }
        return true;
    }

}