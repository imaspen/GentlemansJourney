using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeColliderScript : MonoBehaviour
{

    [SerializeField]
    private GameObject playerObject;
    private float enemyHealth;
    private HealthTracker enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyObject = other.GetComponent<HealthTracker>();
            enemyObject.ReduceHealth(20f);
        }
    }
}
