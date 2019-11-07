using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeController : MonoBehaviour
{
    private Animator _animator;
    private Transform _playerTransform;
    private PlayerTracker _playerTracker;
    private Collider _playerCollider;
    private HealthTracker _playerHealth;
    private Collider _meleeCollider;
    private bool _isAttacking;
    private float _attackTimer;
    private bool _hit;

    public float AttackDistance = 0.5f;
    public float AttackDuration = 2f;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerTracker = GetComponent<PlayerTracker>();
        var player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = player.transform;
        _playerCollider = player.GetComponent<Collider>();
        _playerHealth = player.GetComponent<HealthTracker>();
        _meleeCollider = transform.Find("Melee Collider")
            .GetComponent<Collider>();
        Debug.Log(_meleeCollider);
        _isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isAttacking)
        {
            if (Vector3.Distance(transform.position, _playerTransform.position) 
                <= AttackDistance)
            {
                _playerTracker.isStopped = true;
                _isAttacking = true;
                _attackTimer = 0.0f;
                _animator.SetBool("Slashing", true);
                _hit = false;
            }
        }
        else
        {
            _attackTimer += Time.deltaTime;
            if (_attackTimer >= AttackDuration 
                || Vector3.Distance(_playerTransform.position, transform.position)
                >= 2 * AttackDistance)
            {
                _playerTracker.isStopped = false;
                _isAttacking = false;
                _animator.SetBool("Slashing", false);
            }
            if (!_hit && _attackTimer > AttackDuration / 2)
            {
                if (_meleeCollider.bounds.Intersects(_playerCollider.bounds))
                {
                    _playerHealth.ReduceHealth(30);
                }
                _hit = true;
            }
        }
    }
}
