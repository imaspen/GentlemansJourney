using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmuletBoss : MonoBehaviour
{
    private Animation animator;
    private Animation forcefieldAnimator;
    private HealthTracker playerHP;

    private GameObject enemySpawn01;
    private GameObject enemySpawn02;
    private GameObject enemySpawn03;
    private GameObject enemySpawn04;
    private GameObject forcefield;

    private bool stagestartcomplete = false;
    private bool stage01complete = false;
    private bool stage02complete = false;
    private bool stage03complete = false;

    private bool hp70percent;
    private bool hp45percent;
    private bool hp20percent;

    private Transform bossEnemies;

    private bool coroutinePlaying;

    [SerializeField]
    private GameObject enemyPrefab;

    private bool isShielded;

    [SerializeField]
    private float maxHealth;

    private float health;

    // Start is called before the first frame update
    void Start()
    {
        bossEnemies = transform.Find("BossEnemies");
        coroutinePlaying = false;
        health = maxHealth;
        isShielded = false;
        animator = transform.Find("Amulet").GetComponent<Animation>();
        playerHP = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthTracker>();
        enemySpawn01 = GameObject.FindGameObjectWithTag("BossSpawn01");
        enemySpawn02 = GameObject.FindGameObjectWithTag("BossSpawn02");
        enemySpawn03 = GameObject.FindGameObjectWithTag("BossSpawn03");
        enemySpawn04 = GameObject.FindGameObjectWithTag("BossSpawn04");
        forcefield = transform.Find("Amulet/Forcefield").gameObject;
        forcefieldAnimator = forcefield.GetComponent<Animation>();
        Debug.Log(forcefield.name);
        //Debug.Log("Enemy1 :" + enemySpawn01.name);
        StartCoroutine(StartSequence());
    }

    // Update is called once per frame
    void Update()
    {
        if (stagestartcomplete)
        {
            stagestartcomplete = false;
            StartCoroutine(StartSpawning01());
        }
        
        if (stage01complete)
        {
            StartCoroutine(StartSpawning02());
        }

        if (isShielded)
        {
            forcefield.SetActive(true);
        }
        else
        {
            forcefield.SetActive(false);
        }

        if (health < (maxHealth * 0.2))
        {
            hp20percent = true;
        }
        else if (health < (maxHealth * 0.45))
        {
            hp45percent = true;
        }
        else if (health < (maxHealth * 0.7))
        {
            hp70percent = true;
        }

        if (!coroutinePlaying && stagestartcomplete && (health < (maxHealth * 0.7)))
        {
            StartCoroutine(StartSpawning01());
        }

        if (!coroutinePlaying && stage01complete && (health < (maxHealth * 0.45)))
        {
            StartCoroutine(StartSpawning02());
        }

        if (!coroutinePlaying && stage02complete && (health < (maxHealth * 0.2)))
        {

        }

        
    }

    IEnumerator StartSequence()
    {
        Debug.Log("START SEQUENCE");
        yield return new WaitForSeconds(1f);

        var temp = Instantiate(enemyPrefab, bossEnemies);
        temp.transform.position = enemySpawn01.transform.position;

        temp = Instantiate(enemyPrefab, bossEnemies);
        temp.transform.position = enemySpawn02.transform.position;

        stagestartcomplete = true;

    }

    IEnumerator StartSpawning01()
    {
        Debug.Log("STARTSPAWNING01");
        if (!stage01complete)
        {
            if (!coroutinePlaying)
            {
                coroutinePlaying = true;
                yield return new WaitForSeconds(1f);
                Instantiate(enemyPrefab, enemySpawn01.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                isShielded = true;
                Instantiate(enemyPrefab, enemySpawn02.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                Instantiate(enemyPrefab, enemySpawn03.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                Instantiate(enemyPrefab, enemySpawn04.transform.position, Quaternion.identity);
                stage01complete = true;
                coroutinePlaying = false;
            }
        }
    }

    IEnumerator StartSpawning02()
    {
        Debug.Log("STARTSPAWNING02");
        if (!stage02complete)
        {
            if (!coroutinePlaying)
            {
                coroutinePlaying = true;
                yield return new WaitForSeconds(1f);
                Instantiate(enemyPrefab, enemySpawn01.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                Instantiate(enemyPrefab, enemySpawn02.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                Instantiate(enemyPrefab, enemySpawn03.transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                Instantiate(enemyPrefab, enemySpawn04.transform.position, Quaternion.identity);
                stage02complete = true;
                coroutinePlaying = false;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isShielded)
        {
            Debug.Log("Amulet hit");
            StartCoroutine(playerHP.ScreenBlinkEnemy());
            health = health - damage;
            // FIX ANIMATION ON HIT
            animator.Play("Amulet.Hit");
            Debug.Log(health);
        }
    }
}
