using UnityEngine;
using System.Collections;

public class RandomMove : MonoBehaviour
{
    public int random_seed;

    private Vector3 direct;

    private Vector3 center;
    public float radius;

    private float speed=1f;
    void Awake()
    {
        direct = Vector3.zero;
        center = transform.position;
        StartCoroutine(Move());
    }
    void FixedUpdate()
    {
        transform.Translate(direct);
    }

    private IEnumerator Move()
    {
        while(true)
        {
            Vector3 delta = center - transform.position;

            float range = speed * Time.deltaTime;

            float dx = Random.Range(-1f,1f);
            float dy = Mathf.Sqrt(1 - dx * dx);
            Vector3 random_direct = new Vector3(dx, dy);
            Debug.Log(random_direct);
            
            Vector3 new_direct = Vector3.Lerp(random_direct*range, delta.normalized*range, delta.magnitude/radius);
            direct = Vector3.Lerp(direct, new_direct, 5f);
            yield return new WaitForSeconds(Random.Range(1f,3f));
        }
    }
}