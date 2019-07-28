using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotAB : MonoBehaviour
{
    public bool PC;
    public bool PLoop;
    public Vector3 PBegin;
    public Vector3 PTarget;
    private Vector3 PChange;
    public Vector3 PSpeed;


    public bool RC;
    public bool RLoop;
    public Vector3 RBegin;
    public Vector3 RTarget;
    private Vector3 RChange;
    public Vector3 RSpeed;


    public bool SC;
    public bool SLoop;
    public Vector3 SBegin;
    public Vector3 STarget;
    private Vector3 SChange;
    public Vector3 SSpeed;

    public bool AC;
    public bool ALoop;
    public float ABegin;
    public float ATarget;
    private float AChange;
    public float ASpeed;

    private Vector3 Pmid;
    private Vector3 Rmid;
    private Vector3 Smid;
    private float Amid;

    public float horizon_speed
    {
        get
        {
            return PSpeed.x * (PTarget.x - PChange.x) / Mathf.Abs(PTarget.x - PChange.x);
        }
    }

    public float vertical_speed
    {
        get
        {
            return PSpeed.y * (PTarget.y - PChange.y) / Mathf.Abs(PTarget.y - PChange.y);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PChange = PBegin = this.transform.localPosition;
        RChange = RBegin = this.transform.localEulerAngles;
        SChange = SBegin = this.transform.localScale;

      
    }

    // Update is called once per frame
    void Update()
    {
      
        if (PC)
        {
            if (Mathf.Abs(PChange.x - PTarget.x) > 0.03)
            {
                PChange.x += PSpeed.x * Time.deltaTime * (PTarget.x - PChange.x) / Mathf.Abs(PTarget.x - PChange.x);
            }
            if (Mathf.Abs(PChange.y - PTarget.y) > 0.03)
            {
                PChange.y += PSpeed.y * Time.deltaTime * (PTarget.y - PChange.y) / Mathf.Abs(PTarget.y - PChange.y);
            }

            if (Mathf.Abs(PChange.z - PTarget.z) > 0.03)
            {
                PChange.z += PSpeed.z * Time.deltaTime * (PTarget.z - PChange.z) / Mathf.Abs(PTarget.z - PChange.z);
            }
            this.transform.localPosition = PChange;


            if (PLoop & Mathf.Abs(PChange.x - PTarget.x) + Mathf.Abs(PChange.y - PTarget.y) + Mathf.Abs(PChange.z - PTarget.z) < 0.3)
            {
                Pmid = PTarget;
                PTarget = PBegin;
                PBegin = Pmid;

            }
        }

        if (RC)
        {
            if (Mathf.Abs(RChange.x - RTarget.x) > 0.03)
            {
                RChange.x += RSpeed.x * Time.deltaTime * (RTarget.x - RChange.x) / Mathf.Abs(RTarget.x - RChange.x);
            }
            if (Mathf.Abs(RChange.y - RTarget.y) > 0.03)
            {
                RChange.y += RSpeed.y * Time.deltaTime * (RTarget.y - RChange.y) / Mathf.Abs(RTarget.y - RChange.y);
            }
            if (Mathf.Abs(RChange.z - RTarget.z) > 0.03)
            {
                RChange.z += RSpeed.z * Time.deltaTime * (RTarget.z - RChange.z) / Mathf.Abs(RTarget.z - RChange.z);
            }
            this.transform.localEulerAngles = RChange;

            if (RLoop & Mathf.Abs(RChange.x - RTarget.x) + Mathf.Abs(RChange.y - RTarget.y) + Mathf.Abs(RChange.z - RTarget.z) < 0.1)
            {
                Rmid = RTarget;
                RTarget = RBegin;
                RBegin = Rmid;

            }
        }


        if (SC)
        {
            if (Mathf.Abs(SChange.x - STarget.x) > 0.03)
            {
                SChange.x += SSpeed.x * Time.deltaTime * (STarget.x - SChange.x) / Mathf.Abs(STarget.x - SChange.x);
            }
            if (Mathf.Abs(SChange.y - STarget.y) > 0.03)
            {
                SChange.y += SSpeed.y * Time.deltaTime * (STarget.y - SChange.y) / Mathf.Abs(STarget.y - SChange.y);
            }
            if (Mathf.Abs(SChange.z - STarget.z) > 0.03)
            {
                SChange.z += SSpeed.z * Time.deltaTime * (STarget.z - SChange.z) / Mathf.Abs(STarget.z - SChange.z);
            }
            this.transform.localScale = SChange;



            if (SLoop & Mathf.Abs(SChange.x - STarget.x) + Mathf.Abs(SChange.y - STarget.y) + Mathf.Abs(SChange.z - STarget.z) < 0.1)
            {
                Smid = STarget;
                STarget = SBegin;
                SBegin = Smid;
            }

        }




        if (AC)
        {
            if (Mathf.Abs(AChange - ATarget ) > 0.03)
            {
                AChange  += ASpeed  * Time.deltaTime * (ATarget - AChange ) / Mathf.Abs(ATarget - AChange );
            }

            this.GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, AChange);



            if (ALoop & Mathf.Abs(AChange - ATarget ) + Mathf.Abs(AChange  - ATarget ) + Mathf.Abs(AChange  - ATarget ) < 0.1)
            {
                Amid = ATarget;
                ATarget = ABegin;
                ABegin = Amid;
            }

        }


    }
}
