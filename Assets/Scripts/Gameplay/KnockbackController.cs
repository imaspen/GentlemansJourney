using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockbackController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _player;
    private PlayerTracker _playerTracker;

    public float KnockbackVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerTracker = GetComponent<PlayerTracker>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public IEnumerator Knockback()
    {
        _rigidbody.isKinematic = false;
        _playerTracker.isStopped = true;
        _rigidbody.velocity = _player.rotation * new Vector3(0, 0, KnockbackVelocity);
        yield return new WaitForSeconds(1.0f);
        _rigidbody.isKinematic = true;
        _playerTracker.isStopped = false;
    }
}
