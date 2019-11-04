using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeCollider;

    [SerializeField]
    private float _meleeCooldown = 1f;
    public float MeleeCooldown
    {
        get { return _meleeCooldown; }
        set { _meleeCooldown = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(MeleeAttack());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("AttackMelee") != 0)
        {
            Debug.Log("MELEE ATTACK!");
            StartCoroutine(MeleeAttack());
        }
    }

    IEnumerator MeleeAttack()
    {
        Debug.Log("Attack start");
        meleeCollider.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        meleeCollider.SetActive(false);
        Debug.Log("Attack end");
    }
}
