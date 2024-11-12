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

  
    [SerializeField] private List<GameObject> _enemyPrefabs=new List<GameObject>();       

   
  [SerializeField]  private SpawnMode _spawnMode = SpawnMode.SpawnRandom;   
  public  event Action onAllEnemiesDestroyed;

    private int enemies;

    public event Action OnEnemyDefeated;


    public bool SpawnRandomEnemy()
    {
       
        if (_enemyPrefabs.Count == 0 ) return false;

        int index = UnityEngine.Random.Range(0, _enemyPrefabs.Count);
        GameObject enemyPrefab = _enemyPrefabs[index];
        
        if (_spawnMode == SpawnMode.SpawnRandom)
        {

            GameObject spawnedEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            spawnedEnemy.GetComponent<HealthScript>().OnDeath += (OnEnemyDestroyed);
            _enemyPrefabs.RemoveAt(index);

            enemies++;

        }
        else if (_spawnMode == SpawnMode.SpawnAndRemove)
        {
            GameObject spawnedEnemy = Instantiate(_enemyPrefabs[0], transform.position, Quaternion.identity);
    
            spawnedEnemy.GetComponent<HealthScript>().OnDeath+=(OnEnemyDestroyed);
            _enemyPrefabs.RemoveAt(index);

            enemies++;

        }
        return true;
    }


    private void OnEnemyDestroyed()
    {
       
           

        enemies--;

        OnEnemyDefeated?.Invoke();

        if (enemies <= 0)
        {
            onAllEnemiesDestroyed.Invoke();

        }
    }
}
