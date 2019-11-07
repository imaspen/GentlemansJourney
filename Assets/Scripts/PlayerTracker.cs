using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTracker : MonoBehaviour
{
    private Transform _player;
    private NavMeshAgent _agent;

    public float FollowDistance = 1.0f;
    public bool isStopped;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isStopped)
        {
            _agent.isStopped = true;
            return;
        }

        var vectorTo = transform.position - _player.position;
        vectorTo.Normalize();
        _agent.SetDestination(_player.position);
        transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));

        _agent.isStopped = _agent.remainingDistance <= FollowDistance;
    }
}
