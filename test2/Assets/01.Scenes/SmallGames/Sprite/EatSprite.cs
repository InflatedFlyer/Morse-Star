using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatSprite : MonoBehaviour
{
    public GameObject Sprite1;
    public GameObject Sprite2;
    public GameObject Sprite3;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "extraSprite1")
            Sprite1.SetActive(true);

        if (collision.gameObject.name == "extraSprite2")
            Sprite2.SetActive(true);

        if (collision.gameObject.name == "extraSprite3")
            Sprite3.SetActive(true);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
