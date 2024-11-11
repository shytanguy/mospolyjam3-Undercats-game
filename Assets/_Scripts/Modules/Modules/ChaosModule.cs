using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ChaosModule : MonoBehaviour
{
    private void OnEnable()
    {
        ChaosModuleController.OnActivated += SpawnProjectile;
    }
    private void OnDisable()
    {
        ChaosModuleController.OnActivated -= SpawnProjectile;
    }

    private void SpawnProjectile(GameObject prefab, Vector3 offset)
    {
        Instantiate(prefab, transform.position + offset, Quaternion.identity);
    }
}
