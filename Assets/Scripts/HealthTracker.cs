using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    private Image healthBar;
    private float percentileHP;
    private bool isPlayer = false;

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

        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();

        if (gameObject.tag == "Player")
        {
            isPlayer = true;
            Debug.Log("PLAYER HEALTH DETECTED");
        }
    }

	
    public void ReduceHealth(float reduction)
    {

        CurrentHealth -= reduction;

        if (isPlayer == true)
        {
            percentileHP = CurrentHealth / MaxHealth;
            healthBar.fillAmount = percentileHP;
        }
    }

    public void AddHealth(float hp)
    {
        CurrentHealth += hp;

        if (isPlayer == true)
        {
            percentileHP = CurrentHealth / MaxHealth;
            healthBar.fillAmount = percentileHP;
        }
    }
}
