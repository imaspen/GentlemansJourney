using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTracker : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set {
            _currentHealth = Mathf.Min(value, MaxHealth);
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    [SerializeField]
    private float _maxHealth;

    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    // Start is called before the first frame update
    void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    void Update()
    {
        
    }
	
    public void ReduceHealth(float reduction)
    {
        CurrentHealth -= reduction;
    }

    public void AddHealth(float hp)
    {
        CurrentHealth += hp;
    }
}
