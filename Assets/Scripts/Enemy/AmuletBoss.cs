using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AmuletBoss : MonoBehaviour
{
    private PlayerMovement _playerMovement;
    private PlayerAttack _playerAttack;
    private Animator _playerAnimator;
    private Animator _animator;
    private Transform _spawnPoints;
    private Transform _bossEnemies;
    private IEnumerator _wave;
    private int _hitCount;

    private GameDirector gameDirector;

    public GameObject Skeleton;
    public GameObject Ghost;

    // Start is called before the first frame update
    void Start()
    {
        gameDirector = GameObject.FindGameObjectWithTag("GameDirector").GetComponent<GameDirector>();
        gameDirector.SpawnBossMusic();
        var player = GameObject.FindGameObjectWithTag("Player");
        _playerMovement = player.GetComponent<PlayerMovement>();
        _playerAttack = player.GetComponent<PlayerAttack>();
        _playerAnimator = player.GetComponentInChildren<Animator>();

        _animator = GetComponent<Animator>();
        _spawnPoints = transform.parent.parent.Find("Spawn Points");
        _bossEnemies = transform.parent.parent.Find("Boss Enemies");
        _wave = SpawnWave();
        _wave.MoveNext();
    }

    private IEnumerator SpawnWave()
    {
        StartCoroutine(SpawnWave1());
        yield return null;
        StartCoroutine(SpawnWave2());
        yield return null;
        StartCoroutine(SpawnWave3());
        yield return null;
        StartCoroutine(SpawnWave4());
        yield return null;
        WinGame();
    }

    private IEnumerator SpawnWave1()
    {
        TogglePlayer(false);
        for (int i = 0; i < _spawnPoints.childCount; i++)
        {
            var spawnPoint = _spawnPoints.GetChild(i);
            var enemy = Instantiate(Ghost, _bossEnemies);
            SetState(enemy, false);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.LookAt(transform.position);
            yield return new WaitForSeconds(1.0f);
        }
        for (int i = 0; i < _bossEnemies.childCount; i++)
        {
            SetState(_bossEnemies.GetChild(i).gameObject, true);
        }
        TogglePlayer(true);
    }
    private IEnumerator SpawnWave2()
    {
        TogglePlayer(false);
        for (int i = 0; i < _spawnPoints.childCount; i += 2)
        {
            var spawnPoint = _spawnPoints.GetChild(i);
            var enemy = Instantiate(Skeleton, _bossEnemies);
            SetState(enemy, false);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.LookAt(transform.position);
            yield return new WaitForSeconds(1.0f);
        }
        for (int i = 0; i < _bossEnemies.childCount; i++)
        {
            SetState(_bossEnemies.GetChild(i).gameObject, true);
        }
        TogglePlayer(true);
    }
    private IEnumerator SpawnWave3()
    {
        TogglePlayer(false);
        for (int i = 0; i < _spawnPoints.childCount; i++)
        {
            var spawnPoint = _spawnPoints.GetChild(i);
            var enemy = Instantiate(i % 2 == 0 ? Skeleton : Ghost, _bossEnemies);
            SetState(enemy, false);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.LookAt(transform.position);
            yield return new WaitForSeconds(1.0f);
        }
        for (int i = 0; i < _bossEnemies.childCount; i++)
        {
            SetState(_bossEnemies.GetChild(i).gameObject, true);
        }
        TogglePlayer(true);
    }
    private IEnumerator SpawnWave4()
    {
        TogglePlayer(false);
        for (int i = 0; i < _spawnPoints.childCount; i++)
        {
            var spawnPoint = _spawnPoints.GetChild(i);
            var enemy = Instantiate(Skeleton, _bossEnemies);
            SetState(enemy, false);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.LookAt(transform.position);
            yield return new WaitForSeconds(1.0f);
        }
        for (int i = 0; i < _bossEnemies.childCount; i++)
        {
            SetState(_bossEnemies.GetChild(i).gameObject, true);
        }
        TogglePlayer(true);
    }

    private void SetState(GameObject enemy, bool state)
    {
        var enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController) enemyController.enabled = state;
        else enemy.GetComponent<MeleeController>().enabled = state;
        enemy.GetComponent<NavMeshAgent>().enabled = state;
        enemy.GetComponent<PlayerTracker>().enabled = state;
        var rb = enemy.GetComponent<Rigidbody>();
        if (enemy.name.Contains("Ghost"))
        {
            enemy.GetComponent<Collider>().isTrigger = state;
        }
        rb.freezeRotation = !state;
        rb.useGravity = !state;
        rb.isKinematic = state;
    }

    private void TogglePlayer(bool state)
    {
        _playerMovement.enabled = state;
        _playerAttack.enabled = state;
        _playerAnimator.SetBool("Moving", state);
    }

    private void WinGame()
    {
        gameDirector.VictoryScreen();
        Debug.Log("You win!");
        _animator.SetTrigger("Die");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_bossEnemies.childCount == 0)
        {
            _animator.SetTrigger("Hit");
            if (++_hitCount >= 3)
            {
                _wave.MoveNext();
                _hitCount = 0;
            }
        }
    }
}
