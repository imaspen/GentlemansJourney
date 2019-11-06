using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header ("Attributes")]
    public float moveSpeed = 5.0f;
    public float rateOfFire = 2.0f;
    [Tooltip("Enemy will stay this distance from the player" +
        "     N/A for melee enemies")]
    public float maxDist = 2.0f;

    [Header ("Ranged Or Melee?")]
    public bool isRanged;
    public bool isMelee;

    [Header ("Game Objects")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    // private variables
    private float _fireCooldown = 0.0f;
    private Transform _target;
    private NavMeshAgent _agent;
    private float _navmeshCooldown;
    private GhostSounds ghostSounds;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        _target = player.transform;
        _agent = GetComponent<NavMeshAgent>();
        ghostSounds = GetComponentInChildren<GhostSounds>();
    }

    // Update is called once per frame
    void Update()
    {
        _navmeshCooldown += Time.deltaTime;
        if (true)
        {
            if (Vector3.Distance(transform.position, _target.position) < maxDist)
            {
                _agent.isStopped = true;
            }
            else
            {
                _agent.isStopped = false;
                _agent.SetDestination(_target.position);
                _navmeshCooldown -= 1;
            }
        }
        // rotate to face player
        transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));

        //if (Vector3.Distance(transform.position, _target.position) >= maxDist)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, _target.position, moveSpeed * Time.deltaTime);
        //}

        if (isRanged) // handle ranged combat
        {
            // if it is time to shoot
            if (_fireCooldown <= 0.0f)
            {
                // if within range of target
                if (Vector3.Distance(transform.position, _target.position) <= maxDist)
                {
                    ghostSounds.SpitClip();
                    Shoot();
                }
                // reset fireCountdown
                _fireCooldown = 1.0f / rateOfFire;
            }
            _fireCooldown -= Time.deltaTime;
        }
        else if (isMelee) // handle melee combat
        {
            // get close to the player
            maxDist = 0.25f;
            // stab
            Melee();
        }        
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(_target);
        }
    }

    void Melee()
    {
        // stabby stabby stab stab
    }
}
