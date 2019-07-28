using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeLittleGameCamera : MonoBehaviour {

    public float CameraMoveSpeed;

    private GameObject everything;
    public float RotateSpeed;
    public float ViewSpeed;
    public float TargetView;
    public float TargetAngle;
    public GameObject Target;

    private float lastTime;
    private float curTime;

    public string movestate;
    public string rotatestate;
    public float currentY;
    private float k;

	void Start () {
        //everything = GameObject.Find("everything");
        Target = GameObject.Find("Sphere");
        CameraMoveSpeed = 20;
        RotateSpeed = 1.3f;
        movestate = "Moving";
        rotatestate = "Rotating";
        lastTime = Time.time;
        k = transform.eulerAngles.y;
        TargetAngle = k;
        TargetView = 40;// Camera.main.fieldOfView;
        ViewSpeed = 10;
    }
	
	// Update is called once per frame
	void Update () {
        ChangeEyesight();
        currentY = transform.eulerAngles.y;
       
        if (transform.position.x!= Target.transform.position.x|| transform.position.z != Target.transform.position.z)//           Mathf.Abs((transform.position - Target.transform.position).x)>=1)
            movestate = "Moving";
        if (movestate == "Moving")
        {
            curTime = Time.time;
            //if (curTime - lastTime >= 0.3)   //时间差大于x秒过后            
            {
                if (CameraMoveSpeed <= 350)
                    CameraMoveSpeed += 20 * CameraMoveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(Target.transform.position.x,0,Target.transform.position.z), CameraMoveSpeed * Time.deltaTime);
            }
        }
        if(transform.position.x==Target.transform.position.x&& transform.position.z == Target.transform.position.z)
        {
            movestate = "Stop";
            CameraMoveSpeed = 10;
            lastTime = Time.time;
        }

        if(rotatestate=="Rotating"&&transform.eulerAngles.y==TargetAngle)
        {
            rotatestate = "Stop";
        }
        if(rotatestate=="Stop")
        {
            k = transform.eulerAngles.y;
            if (transform.eulerAngles.y != TargetAngle)
                rotatestate = "Rotating";
        }
        if(rotatestate=="Rotating")
        {
            CameraRotate();
        }
    }


    void CameraRotate()
    {        

        if (Mathf.Abs(transform.eulerAngles.y - TargetAngle) <= Mathf.Abs(k - TargetAngle)/2)
        {
            RotateSpeed += Time.deltaTime * RotateSpeed;
        }
        else            if(RotateSpeed>=6) RotateSpeed -= Time.deltaTime * RotateSpeed;
        this.transform.eulerAngles += new Vector3(0, 1, 0) * RotateSpeed;
        if(Mathf.Abs(transform.eulerAngles.y - TargetAngle) <=5)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, TargetAngle, transform.eulerAngles.z);
            RotateSpeed = 1;
        }
    }

    void ChangeEyesight()
    {
        if (Camera.main.orthographicSize <= TargetView)
            Camera.main.orthographicSize += 2;
        if (Camera.main.orthographicSize >= TargetView)
            Camera.main.orthographicSize -= 2;
    }
}


