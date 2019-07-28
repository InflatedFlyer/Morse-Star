using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.transform.tag == "MoveCube")
        {
            GetComponent<VirtuallyWalk>().ChangeWind(new Vector2(collision.transform.GetComponent<RotAB>().PSpeed.x, 0));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "MoveCube")
        {
            GetComponent<VirtuallyWalk>().ChangeWind(Vector2.zero);
        }
    }
}
