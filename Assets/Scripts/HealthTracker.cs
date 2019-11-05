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
    
    static private GameObject whiteScreen;
    static private GameObject redScreen;
    private bool whiteScreenOn;
    private bool redScreenOn;

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
                whiteScreen.SetActive(false);
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
        //whiteScreen = transform.Find("ScreenBlinkEnemy");

        cameraEffects = Camera.main.GetComponent<ScreenEffects>();

        CurrentHealth = MaxHealth;

        healthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Image>();

        if (gameObject.tag == "Player")
        {
            isPlayer = true;
            Debug.Log("PLAYER HEALTH DETECTED");
        }
    }

    private void Update()
    {

    }

    private void Start()
    {
        if (isPlayer)
        {
            whiteScreen = transform.Find("UI/ScreenBlinkEnemy").gameObject;
            redScreen = transform.Find("UI/ScreenBlinkPlayer").gameObject;
        }
    }

	
    public void ReduceHealth(float reduction)
    {

        CurrentHealth -= reduction;

        if (isPlayer == true)
        {
            if (redScreenOn == false)
            {
                percentileHP = CurrentHealth / MaxHealth;
                healthBar.fillAmount = percentileHP;
                cameraEffects.StartShake(0.04f, 0.08f);
                StartCoroutine(ScreenBlinkPlayer());
            }
        }
        else
        {
            if (whiteScreenOn == false)
            {
                cameraEffects.StartShake(0.1f, 0.1f);
                Debug.Log(whiteScreen);
                StartCoroutine(ScreenBlinkEnemy());
            }
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

    IEnumerator ScreenBlinkPlayer()
    {
        //Debug.Log(gameObject.name);
        redScreen.SetActive(true);
        redScreenOn = true;
        yield return new WaitForSeconds(0.02f);
        redScreenOn = false;
        redScreen.SetActive(false);
        yield return new WaitForSeconds(3f);
        redScreen.SetActive(false);
        //whiteScreen.SetActive(true);
        //yield return new WaitForSeconds(0.025f);
        //whiteScreen.SetActive(false);
    }

    IEnumerator ScreenBlinkEnemy()
    {
        whiteScreen.SetActive(true);
        whiteScreenOn = true;
        yield return new WaitForSeconds(0.02f);
        whiteScreenOn = false;
        whiteScreen.SetActive(false);
        //yield return new WaitForSeconds(0.01f);
        //whiteScreen.SetActive(true);
        //yield return new WaitForSeconds(0.025f);
        //whiteScreen.SetActive(false);
    }
}
