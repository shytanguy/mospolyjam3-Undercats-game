using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnMode
    {
        SpawnRandom,
        SpawnAndRemove
    }

  
    [SerializeField] private GameObject[] _enemyPrefabs;       

   
  [SerializeField]  private SpawnMode _spawnMode = SpawnMode.SpawnRandom;   
  public  event Action onAllEnemiesDestroyed; 

    private List<GameObject> _activeEnemies = new List<GameObject>();

    public event Action OnEnemyDefeated;
    public bool SpawnRandomEnemy()
    {
       
        if (_enemyPrefabs.Length == 0 ) return false;

       
        GameObject enemyPrefab = _enemyPrefabs[UnityEngine.Random.Range(0, _enemyPrefabs.Length)];
       

        if (_spawnMode == SpawnMode.SpawnRandom)
        {
          
            GameObject spawnedEnemy = Instantiate(_enemyPrefabs[0], transform.position, Quaternion.identity);
            spawnedEnemy.GetComponent<HealthScript>().OnDeath += (() => OnEnemyDestroyed(spawnedEnemy));
            _activeEnemies.Add(spawnedEnemy);

        }
        else if (_spawnMode == SpawnMode.SpawnAndRemove)
        {

            GameObject spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            spawnedEnemy.GetComponent<HealthScript>().OnDeath+=(() =>OnEnemyDestroyed(spawnedEnemy));
            _activeEnemies.Add(spawnedEnemy);

        }
        return true;
    }


    private void OnEnemyDestroyed(GameObject destroyedEnemy)
    {
        if (_activeEnemies.Contains(destroyedEnemy))
        {
            destroyedEnemy.GetComponent<HealthScript>().OnDeath -= (() => OnEnemyDestroyed(destroyedEnemy));

            _activeEnemies.Remove(destroyedEnemy);
            OnEnemyDefeated?.Invoke();
       
            if (_activeEnemies.Count == 0)
            {
                onAllEnemiesDestroyed.Invoke();
            }
        }
    }
}
