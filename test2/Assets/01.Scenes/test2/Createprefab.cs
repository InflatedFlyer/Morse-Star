using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Createprefab : MonoBehaviour
{
    public GameObject prefab;
    public int numble;
    public float waittime;
    public Vector3 PrefabPosition;
    public Quaternion PrefabRotation;

    // Start is called before the first frame update

    void Start()
    {
        prefab = Resources.Load("VirtualButtle") as GameObject;
        StartCoroutine (spawnWaves());
      
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator spawnWaves()
    {
            int i = 0;
            while ( i < numble)
            {
                yield return new WaitForSeconds(waittime);
                Instantiate(prefab, PrefabPosition, PrefabRotation);
                i++;
                yield return new WaitForSeconds(1);
                
            }
    }
}
