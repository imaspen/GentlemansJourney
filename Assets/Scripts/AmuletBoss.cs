using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletBoss : MonoBehaviour
{
    private Animator animator;
    private HealthTracker playerHP;

    [SerializeField]
    private float health;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Amulet hit");
        playerHP.ScreenBlinkEnemy();
        health = health - damage;
    }
}
