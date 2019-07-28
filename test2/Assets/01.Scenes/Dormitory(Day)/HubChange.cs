using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubChange : MonoBehaviour
{
    public float left;
    public float right;
    public float ChangeSpeed;
    public GameObject target;
    public Color color1;
    public string state;
    // Start is called before the first frame update
    void Start()
    {
        color1 = target.GetComponent<SpriteRenderer>().material.color;
        color1.a = left;
        state = "state0";
        target.GetComponent<SpriteRenderer>().material.color = color1;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
            state = "state1";
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
            state = "state0";
    }
    // Update is called once per frame
    void Update()
    {
        if(state=="state1"&&color1.a<right)
        {
            color1.a += Time.deltaTime * ChangeSpeed;
            target.GetComponent<SpriteRenderer>().material.color = color1;
        }
        if (state == "state0" && color1.a > left)
        {
            color1.a -= Time.deltaTime * ChangeSpeed;
            target.GetComponent<SpriteRenderer>().material.color = color1;
        }

    }
}
