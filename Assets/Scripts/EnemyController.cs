using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header ("Attributes")]
    public float moveSpeed = 5.0f;
    public float rateOfFire = 2.0f;
    public float minDist = 3.0f;
    public float maxDist = 5.0f;

    [Header ("Ranged Or Melee?")]
    public bool isRanged;
    public bool isMelee;

    [Header ("Game Objects")]
    public GameObject bulletPrefab;
    public Transform target;
    public Transform firePoint;

    // private variables
    float fireCountdown = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // rotate to face player
        transform.LookAt(target);

        if (Vector3.Distance(transform.position, target.position) >= maxDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }

        if (isRanged) // handle ranged combat
        {
            // if it is time to shoot
            if (fireCountdown <= 0.0f)
            {
                // if within range of target
                if (Vector3.Distance(transform.position, target.position) <= maxDist)
                {
                    Shoot();
                }
                // reset fireCountdown
                fireCountdown = 1.0f / rateOfFire;
            }
            fireCountdown -= Time.deltaTime;
        }
            else if (isMelee) // handle melee combat
            {
                // get close to the player
                minDist = 0.25f;
                // stab
                Attack();
            }        
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    void Attack()
    {
        // stabby stabby stab stab
    }
}
