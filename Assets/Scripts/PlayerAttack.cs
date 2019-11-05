using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject meleeCollider;
    private ScreenEffects cameraShake;

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
            cameraShake.StartShake(0.02f, 0.025f);
            StartCoroutine(MeleeAttack());
        }
    }

    void Start()
    {
        cameraShake = Camera.main.GetComponent<ScreenEffects>();
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
