using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAXYDoor : MonoBehaviour
{
    public bool ifopen;
    public Vector3 targetPosition;

    public Vector3 originPosition;
    // Start is called before the first frame update
    void Start()
    {
        originPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (ifopen&&transform.position!=targetPosition)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
        if (!ifopen)
            transform.position = Vector3.Lerp(transform.position, originPosition, Time.deltaTime);
    }
}
