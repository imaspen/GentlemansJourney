using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockbackController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Transform _player;
    private EnemyController _enemyController;

    public float KnockbackVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _enemyController = GetComponent<EnemyController>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public IEnumerator Knockback()
    {
        _rigidbody.isKinematic = false;
        _enemyController.isStopped = true;
        _rigidbody.velocity = _player.rotation * new Vector3(0, 0, KnockbackVelocity);
        yield return new WaitForSeconds(1.0f);
        _rigidbody.isKinematic = true;
        _enemyController.isStopped = false;
    }
}
