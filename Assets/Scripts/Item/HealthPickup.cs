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
    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthTracker>();
        Debug.Log("Health is: " + playerHealth.CurrentHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit the potion");
        if (playerHealth.CurrentHealth < playerHealth.MaxHealth)
        {
            Debug.Log(playerHealth.CurrentHealth);
            playerHealth.CurrentHealth = playerHealth.CurrentHealth + HealthBonus;
            Debug.Log(playerHealth.CurrentHealth);
            Destroy(gameObject);
        }
    }
}
