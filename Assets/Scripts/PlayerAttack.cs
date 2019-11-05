﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeCollider;

    [SerializeField]
    private float _attackSpeed = 1f;
    public float AttackSpeed
    {
        get { return _attackSpeed; }
        set { _attackSpeed = Mathf.Abs(value); }
    }

    private float _cooldown = 0f;


    // Update is called once per frame
    void Update()
    {
        _cooldown -= Time.deltaTime;
        if (Input.GetAxis("AttackMelee") != 0 && _cooldown <= 0)
        {
            Debug.Log("MELEE ATTACK!");
            StartCoroutine(MeleeAttack());
        }
    }

    IEnumerator MeleeAttack()
    {
        _cooldown = AttackSpeed;
        Debug.Log("Attack start");
        meleeCollider.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        meleeCollider.SetActive(false);
        Debug.Log("Attack end");
    }
}
