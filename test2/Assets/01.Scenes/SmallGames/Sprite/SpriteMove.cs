using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMove : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<GameChange>().GameState == "Game1")
        {
            if (Input.GetKey(KeyCode.A))
                transform.localPosition -= speed * new Vector3(1, 0, 0) * Time.deltaTime;
            if (Input.GetKey(KeyCode.D))
                transform.localPosition += speed * new Vector3(1, 0, 0) * Time.deltaTime;
            if (Input.GetKey(KeyCode.W))
                transform.localPosition += speed * new Vector3(0, 1, 0) * Time.deltaTime;
            if (Input.GetKey(KeyCode.S))
                transform.localPosition -= speed * new Vector3(0, 1, 0) * Time.deltaTime;
        }
        if(GetComponent<GameChange>().GameState=="Game2")
        {
            if (Input.GetKey(KeyCode.A))
                transform.localPosition -= speed * new Vector3(-4, 6, 0) * Time.deltaTime/7;
            if (Input.GetKey(KeyCode.D))
                transform.localPosition += speed * new Vector3(-4, 6, 0) * Time.deltaTime/7;
            if (Input.GetKey(KeyCode.W))
                transform.localPosition += speed * new Vector3(-2.38f, -0.61f, 0) * Time.deltaTime/2.6f;
            if (Input.GetKey(KeyCode.S))
                transform.localPosition -= speed * new Vector3(-2.38f, -0.61f, 0) * Time.deltaTime/2.6f;
        }
        if (GetComponent<GameChange>().GameState == "Game3")
        {
            if (Input.GetKey(KeyCode.A))
                transform.localPosition -= speed * new Vector3(-5.5f, -7, 0) * Time.deltaTime / 8.5f;
            if (Input.GetKey(KeyCode.D))
                transform.localPosition += speed * new Vector3(-5.5f, -7, 0) * Time.deltaTime / 8.5f;
            if (Input.GetKey(KeyCode.W))
                transform.localPosition += speed * new Vector3(2.1f,-1.39f, 0) * Time.deltaTime / 2.45f;
            if (Input.GetKey(KeyCode.S))
                transform.localPosition -= speed * new Vector3(2.1f, -1.39f, 0) * Time.deltaTime / 2.45f;
        }
    }
}
