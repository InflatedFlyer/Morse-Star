using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Character")
        {
            Vector3 collide_direct = collision.contacts[0].normal;
            Debug.Log(collide_direct);
            if(collide_direct.y>0)
            {
                collision.gameObject.GetComponent<VirtuallyWalk>().Die();
            }
        }
    }
}
