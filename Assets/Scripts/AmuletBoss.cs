using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AmuletBoss : MonoBehaviour
{
    private PlayerMovement _player;
    private Animator _animator;
    private Transform _spawnPoints;
    private Transform _bossEnemies;
    private IEnumerator _wave;
    private int _hitCount;

    public GameObject Skeleton;
    public GameObject Ghost;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _spawnPoints = transform.parent.parent.Find("Spawn Points");
        _bossEnemies = transform.parent.parent.Find("Boss Enemies");
        _player = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerMovement>();
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
        _player.enabled = false;
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
        _player.enabled = true;
    }
    private IEnumerator SpawnWave2()
    {
        _player.enabled = false;
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
        _player.enabled = true;
    }
    private IEnumerator SpawnWave3()
    {
        _player.enabled = false;
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
        _player.enabled = true;
    }
    private IEnumerator SpawnWave4()
    {
        _player.enabled = false;
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
        _player.enabled = true;
    }

    private void SetState(GameObject enemy, bool state)
    {
        var enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController) enemyController.enabled = state;
        else enemy.GetComponent<MeleeController>().enabled = state;
        enemy.GetComponent<NavMeshAgent>().enabled = state;
        enemy.GetComponent<PlayerTracker>().enabled = state;
    }

    private void WinGame()
    {
        Debug.Log("You win!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_bossEnemies.childCount == 0)
        {
            if (++_hitCount >= 3)
            {
                _wave.MoveNext();
                _hitCount = 0;
            }
        }
    }
}
