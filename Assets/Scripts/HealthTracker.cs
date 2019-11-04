﻿using System.Collections;
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
            if (value <= 0) Destroy(gameObject);
            _currentHealth = Mathf.Min(value, MaxHealth);
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
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void ReduceHealth(float reduction)
    {
        Debug.Log(CurrentHealth);
        CurrentHealth = CurrentHealth - reduction;
        Debug.Log(CurrentHealth);
    }

    public void AddHealth(float hp)
    {
        CurrentHealth = CurrentHealth + hp;
    }
}