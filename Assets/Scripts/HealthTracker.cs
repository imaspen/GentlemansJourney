using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    private Image healthBar;
    private float percentileHP;
    private bool isPlayer = false;
    private float randomNum;
    private ScreenEffects cameraEffects;

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private float _currentHealth;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set {
            _currentHealth = Mathf.Min(value, MaxHealth);
            if (_currentHealth <= 0)
            {
                DropItem();
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
        cameraEffects = Camera.main.GetComponent<ScreenEffects>();

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
        else
        {
            cameraEffects.StartShake(0.1f, 0.1f);
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

    void OnEnemyDeath()
    {
        if (CurrentHealth == 0)
        {
            Destroy(gameObject);
        }       
    }

    private void DropItem()
    {
        randomNum = Random.Range(0f, 1f);

        if (item != null)
        {
            if (randomNum >= 0.55f)
            Instantiate(item, gameObject.transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
        }
    }
}
