using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float rateOfFire = 2.0f;
    public bool isRanged;
    public bool isMelee;

    public GameObject bullet;
    public Transform target;

    float minDist = 3.0f;
    float maxDist = 5.0f;

    // Update is called once per frame
    void Update()
    {
        // rotate to face player
        transform.LookAt(target);

        // move object within specified range
        if (Vector3.Distance(transform.position, target.position) >= minDist)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, minDist * Time.deltaTime);

            // when inside that specified range
            if (Vector3.Distance(transform.position, target.position) <= maxDist)
            {
                // check if ranged or melee 
                if (isRanged)
                {
                    minDist = Random.Range(5.0f, 10.0f);
                    StartCoroutine(Shoot());
                }
                else if (isMelee)
                {
                    minDist = 1.5f;
                    Attack();
                }
            }
        }
        
    }

    IEnumerator Shoot()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
        bullet.transform.position = Vector3.MoveTowards(bullet.transform.position, target.position, 330);

        yield return new WaitForSeconds(rateOfFire);
    }

    void Attack()
    {
        // stabby stabby stab stab
    }
}
