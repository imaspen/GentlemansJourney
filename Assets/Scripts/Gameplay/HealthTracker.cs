using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthTracker : MonoBehaviour
{
    GameDirector gameDirector;

    private PlayerSounds playerSounds;

    private GhostSounds ghostSounds;

    [SerializeField]
    private GameObject ghostDeathParticle;

    public bool hasDied = false;

    private Image healthBar;
    private float percentileHP;
    private bool isPlayer = false;
    private float randomNum;
    private ScreenEffects cameraEffects;
    
    static private GameObject whiteScreen;
    static private GameObject redScreen;
    private bool whiteScreenOn;
    private bool redScreenOn;

    [SerializeField]
    private GameObject item;

    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _damageCooldown = 1.0f;
    private float _cooldown;
    private float _cooldownRatio;
    private Animator _animator;

    private KnockbackController _knockbackController;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set {
            _currentHealth = Mathf.Min(value, MaxHealth);
            if (_currentHealth <= 0)
            {
                if (gameObject.tag == "Enemy")
                {
                    if (ghostDeathParticle) Instantiate(
                        ghostDeathParticle,
                        gameObject.transform.position + new Vector3(0, 0.7f, 0),
                        Quaternion.identity
                    );
                    whiteScreen.SetActive(false);
                    DropItem();
                    OnEnemyDeath();
                }
                else if (gameObject.tag == "Player")
                {
                    _animator.SetBool("PlayerIsDead", true);
                    StartCoroutine(DeathSequence());
                }
            }
            else if (_knockbackController)
            {
                StartCoroutine(_knockbackController.Knockback());
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
    
    void Awake()
    {
        hasDied = false;

        cameraEffects = Camera.main.GetComponent<ScreenEffects>();

        CurrentHealth = MaxHealth;

        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();

        if (gameObject.tag == "Player")
        {
            isPlayer = true;
            Debug.Log("PLAYER HEALTH DETECTED");
        }

        if (gameObject.tag == "Enemy")
        {
            ghostSounds = gameObject.GetComponentInChildren<GhostSounds>();
        }
    }

    private void Start()
    {
        hasDied = false;
        playerSounds = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSounds>();
        _knockbackController = GetComponent<KnockbackController>();
        _animator = GetComponentInChildren<Animator>();

        whiteScreenOn = false;
        redScreenOn = false;

        if (isPlayer)
        {
            whiteScreen = transform.Find("UI/ScreenBlinkEnemy").gameObject;
            redScreen = transform.Find("UI/ScreenBlinkPlayer").gameObject;
        }

        _cooldown = _damageCooldown;
        _cooldownRatio = 1 / _damageCooldown;
    }

	private void Update()
    {
        _cooldown += Time.deltaTime;
    }

    public void ReduceHealth(float reduction)
    {
        if (isPlayer == true)
        {
            if (redScreenOn == false)
            {
                if (!hasDied)
                {
                    playerSounds.HurtClip();
                    cameraEffects.StartShake(0.04f, 0.08f);
                    StartCoroutine(ScreenBlinkPlayer());
                }
            }
        }
        else
        {
            if (whiteScreenOn == false)
            {
                cameraEffects.StartShake(0.1f, 0.1f);
                StartCoroutine(ScreenBlinkEnemy());
            }
        }

        CurrentHealth -= reduction * (Mathf.Min(1, _cooldown * _cooldownRatio));
        _cooldown = 0;

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

    void OnEnemyDeath()
    {
        if (CurrentHealth <= 0)
        {
            if (ghostSounds)
            {
                ghostSounds.DeathClip();
            }
            
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

    IEnumerator ScreenBlinkPlayer()
    {
        redScreen.SetActive(true);
        redScreenOn = true;
        yield return new WaitForSeconds(0.02f);
        redScreenOn = false;
        redScreen.SetActive(false);
    }

    public IEnumerator ScreenBlinkEnemy()
    {
        whiteScreen.SetActive(true);
        whiteScreenOn = true;
        playerSounds.MeleeContactClip();
        yield return new WaitForSeconds(0.02f);
        whiteScreenOn = false;
        whiteScreen.SetActive(false);
    }

    IEnumerator DeathSequence()
    {
        if (!hasDied)
        {
            Debug.Log("DEAD");
            hasDied = true;
            gameDirector = GameObject.FindGameObjectWithTag("GameDirector")
                .GetComponent<GameDirector>();
            gameDirector.DeathQuote();
            GetComponent<PlayerMovement>().IsMoveable = false;
            yield return new WaitForSeconds(3f);
            //Destroy(gameObject);
        }
    }
}
