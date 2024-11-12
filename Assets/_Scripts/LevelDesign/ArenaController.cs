using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArenaController : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _spawners=new List<EnemySpawner>();

    [SerializeField] private float _spawnDelay=0.4f;

    [SerializeField] private int _maxSpawned;

    private int _spawned;

    public UnityEvent OnAllDefeated;
    public void StartSpawning()
    {
        foreach(var spawner in _spawners)
        {
            spawner.onAllEnemiesDestroyed += (() => RemoveSpawner(spawner));
            spawner.OnEnemyDefeated += SpawnEnemy;
        }
        StartCoroutine(SpawnDelayCoroutine());
    }
    private void OnDisable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.onAllEnemiesDestroyed -= (() => RemoveSpawner(spawner));
            spawner.OnEnemyDefeated -= SpawnEnemy;
        }
    }
    private void RemoveSpawner(EnemySpawner spawner)
    {
        spawner.onAllEnemiesDestroyed -= (() => RemoveSpawner(spawner));
        spawner.OnEnemyDefeated -= SpawnEnemy;
        _spawners.Remove(spawner);
        Destroy(spawner);
        if (_spawners.Count == 0)
        {
            OnAllDefeated?.Invoke();
        }
    }
    private void SpawnEnemy()
    {
        _spawners[UnityEngine.Random.Range(0, _spawners.Count)].SpawnRandomEnemy();
    }
    private IEnumerator SpawnDelayCoroutine()
    {
        

      foreach(var spawner in _spawners)
        {
            if (spawner.SpawnRandomEnemy())
            {
                yield return new WaitForSeconds(_spawnDelay);
                _spawned++;
                if (_spawned >= _maxSpawned)
                {
                    break;
                }
            }
            else
            {
                continue;
            }
        }
        
    }
}
