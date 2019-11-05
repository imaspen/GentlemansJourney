using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    [Tooltip("Above 1 will not home in" +
        "     Larger values will travel slower")]
    public float speed = 2f;

    public GameObject player;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        if (speed <= 1.0f)
        {
            transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        } else
        {
            transform.Translate(Vector3.forward / speed);
        }
    }

    private void FixedUpdate()
    {
    }

    void HitTarget()
    {
        Destroy(gameObject);
        player.GetComponent<HealthTracker>().ReduceHealth(10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name.Contains("Wall")) Destroy(gameObject);
    }
}
