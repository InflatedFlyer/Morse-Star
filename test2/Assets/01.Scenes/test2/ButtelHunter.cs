using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtelHunter : MonoBehaviour
{
    public float m_speed = 12f;
    public float m_RotSpeed = 60f;

    public GameObject m_Explosion;
    public Transform m_Target;
    public float acceleration = 20f;
    // Start is called before the first frame update
    void Start()
    {
        m_Target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = (m_Target.position - transform.position).normalized;
        float a = Vector3.Angle(transform.forward, target) / m_RotSpeed;
        if (a > 0.1 || a < -0.1f)
            transform.forward = Vector3.Slerp(transform.forward, target, Time.deltaTime / a).normalized;
        else
        {
            m_speed += acceleration * Time.deltaTime;
            transform.forward = Vector3.Slerp(transform.forward, target, 1).normalized;
        }
        transform.position += transform.forward * m_speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject .tag =="Player")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Destroy(Instantiate(m_Explosion, transform.position, Quaternion.identity), 3f);

        }
        if(other.gameObject.tag == "Protection")
        {
            Destroy(gameObject);
        }
    }
}
