using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    HealthTracker playerHealth;

    [SerializeField]
    private float _healthBonus;

    public float HealthBonus
    {
        get { return _healthBonus; }
        set { _healthBonus = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthTracker>();
    }

    private void OnTriggerEnter(Collider other)
    {

            Debug.Log(playerHealth.CurrentHealth);
            playerHealth.AddHealth(HealthBonus);
            Debug.Log(playerHealth.CurrentHealth);
            Destroy(gameObject);
    }
}
