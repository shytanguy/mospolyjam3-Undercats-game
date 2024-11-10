using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaController : MonoBehaviour
{
    [SerializeField] private List<EnemySpawner> _spawners=new List<EnemySpawner>();

    [SerializeField] private float _spawnDelay=0.4f;

    [SerializeField] private int _maxSpawned;

    private int _spawned;
    public void StartSpawning()
    {
        foreach(var spawner in _spawners)
        {
            spawner.onAllEnemiesDestroyed += (() => RemoveSpawner(spawner));
            spawner.OnEnemyDefeated += StartSpawning;
        }
        StartCoroutine(SpawnDelayCoroutine());
    }
    private void OnDisable()
    {
        foreach (var spawner in _spawners)
        {
            spawner.onAllEnemiesDestroyed -= (() => RemoveSpawner(spawner));
            spawner.OnEnemyDefeated -= StartSpawning;
        }
    }
    private void RemoveSpawner(EnemySpawner spawner)
    {
        spawner.onAllEnemiesDestroyed -= (() => RemoveSpawner(spawner));
        spawner.OnEnemyDefeated -= StartSpawning;
        _spawners.Remove(spawner);
        Destroy(spawner);
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
