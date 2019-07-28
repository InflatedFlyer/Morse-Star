using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game2punish : MonoBehaviour
{
    public GameObject zone1;
    public GameObject zone2;
    public GameObject zone3;
    public GameObject originzone1;
    public GameObject originzone2;
    public GameObject originzone3;
    bool z1, z2, z3;
    // Start is called before the first frame update
    void Start()
    {
        z1 = false;
        z2 = false;
        z3 = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "one")
            z1 = true;
        if (other.name == "two")
            z2 = true;
        if (other.name == "three")
            z3 = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (z1)
        {
            Debug.Log("111111111111111111");
            Invoke("zone1move", 2);
            Debug.Log("222222222222222222");
        }
        if (z2)
            Invoke("zone2move", 3);
        if (z3)
        {
            Debug.Log("111111111111111111");
            Invoke("zone3move", 3);
            Debug.Log("222222222222222222");
        }
    }

    void zone1move()
    {
        if (Vector3.Distance(zone1.transform.position, originzone1.transform.position) >= 0.1)
            zone1.transform.position = Vector3.Lerp(zone1.transform.position, originzone1.transform.position, Time.deltaTime);
    }

    void zone2move()
    {
        if (Vector3.Distance(zone2.transform.position, originzone2.transform.position) >= 0.1)
            zone2.transform.position = Vector3.Lerp(zone2.transform.position, originzone2.transform.position, Time.deltaTime);
    }

    void zone3move()
    {
        if (Vector3.Distance(zone3.transform.position, originzone3.transform.position) >= 0.1)
            zone3.transform.position = Vector3.Lerp(zone3.transform.position, originzone3.transform.position, Time.deltaTime);
    }
}