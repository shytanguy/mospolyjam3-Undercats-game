using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ChildItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private float respawnDelay = 5f;
    private GameObject spawnedObject;

    private void Start()
    {
        SpawnObject();
    }

    private void SpawnObject()
    {
        if (prefabToSpawn != null)
        {
            spawnedObject = Instantiate(prefabToSpawn, transform.position, Quaternion.identity, transform);
            spawnedObject.GetComponent<SpawnableObject>().OnDestroyed += HandleObjectDestroyed;
        }
    }
    private void OnDisable()
    {
        spawnedObject.GetComponent<SpawnableObject>().OnDestroyed -= HandleObjectDestroyed;
    }
    private void HandleObjectDestroyed()
    {
        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnObject();
    }
}