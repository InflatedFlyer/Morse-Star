using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTest : MonoBehaviour
{
    public Vector3 BeginPosition;
    public Vector3 BeginRotation;
    public Vector3 BeginScale;

    public Vector3 TargetPosition;
    public Vector3 TargetRotation;
    public Vector3 TargetScale;

    public float PositionChangeSpeed;
    public float RotationChangeSpeed;
    public float ScaleChangeSpeed;

    private Vector3 DeltaPosition;
    private Vector3 DeltaRotation;
    private Vector3 DeltaScale;

    public float ProgressPosition;
    public float ProgressRotation;
    public float ProgressScale;
    // Use this for initialization
    void Start()
    {
        
        BeginPosition = new Vector3(0, 0, 0);
        BeginRotation = new Vector3(0, 0, 0);
        BeginScale = new Vector3(0, 0, 0);

        TargetPosition = new Vector3(8, 0, 0);
        TargetRotation = new Vector3(0, 0, 0);
        TargetScale = new Vector3(0, 0, 0);

        PositionChangeSpeed = 0;
        RotationChangeSpeed = 0;
        ScaleChangeSpeed = 0;

        ProgressPosition = 0;
this.transform.localPosition = BeginPosition;

        ProgressRotation = 0;
        this.transform.localEulerAngles = BeginRotation;

        ProgressScale = 0;
        this.transform.localScale = BeginScale;
    }

    // Update is called once per frame
    void Update()
    {
        PositionMove();
        RotationMove();
    }
    void PositionMove()
    {
        ProgressPosition = (transform.localPosition.x - BeginPosition.x)/ (TargetPosition.x - BeginPosition.x);
        if (ProgressPosition<1)
            this.transform.localPosition += Time.deltaTime * (TargetPosition-BeginPosition).normalized * PositionChangeSpeed;
        else
        {
            this.transform.localPosition = TargetPosition;
        }
    }

    void RotationMove()
    {
        ProgressRotation = (transform.localEulerAngles.x - BeginRotation.x) / (TargetRotation.x - BeginRotation.x);
        if (ProgressRotation < 1)
            this.transform.localEulerAngles += Time.deltaTime * (TargetRotation - BeginRotation).normalized * RotationChangeSpeed;
        else
        {
            this.transform.localEulerAngles = TargetRotation;
        }
    }

    void ScaleMove()
    {
        ProgressScale = (transform.localScale.x - BeginScale.x) / (TargetScale.x - BeginScale.x);
        if (ProgressScale < 1)
            this.transform.localScale += Time.deltaTime * (TargetScale - BeginScale).normalized * ScaleChangeSpeed;
        else
        {
            this.transform.localScale = TargetScale;
        }
    }

}
