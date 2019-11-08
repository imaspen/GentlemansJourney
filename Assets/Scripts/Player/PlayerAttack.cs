using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private PlayerSounds playerSounds;

    [SerializeField]
    private GameObject meleeCollider;
    private ScreenEffects cameraShake;

    [SerializeField]
    private float _attackSpeed = 1f;

    private HealthTracker playerHealth;

    private Animator _anim;
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
            if (!playerHealth.hasDied)
            {
                cameraShake.StartShake(0.02f, 0.025f);
                StartCoroutine(MeleeAttack());
            }
        }
    }

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthTracker>();
        cameraShake = Camera.main.GetComponent<ScreenEffects>();
        playerSounds = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSounds>();
        _anim = GetComponentInChildren<Animator>();
    }

    IEnumerator MeleeAttack()
    {
        int randomAnim = Random.Range(0, 2);
        _cooldown = AttackSpeed;
        Debug.Log("Attack start");
        if (randomAnim == 0)
        {
            _anim.SetBool("RightPunch", true);
        } else if (randomAnim == 1)
        {
            _anim.SetBool("LeftPunch", true);
        }
        meleeCollider.SetActive(true);
        playerSounds.HitClip();
        playerSounds.SwingClip();
        yield return new WaitForSeconds(0.3f);
        meleeCollider.SetActive(false);
        Debug.Log("Attack end");
        if (randomAnim == 0)
        {
            _anim.SetBool("RightPunch", false);
        }
        else if (randomAnim == 1)
        {
            _anim.SetBool("LeftPunch", false);
        }
    }
}
