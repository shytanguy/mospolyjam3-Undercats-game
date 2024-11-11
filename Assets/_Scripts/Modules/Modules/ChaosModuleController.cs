using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosModuleController : ModuleControllerAbstract
{
    [SerializeField] private GameObject _prefabSpawning;

    [SerializeField] private Vector3 _spawnOffset;

    [SerializeField] private float _TimeBetweenSpawns;
    public static ChaosModuleController instance;

    public static event Action OnDeactivated;

    public static event Action<GameObject, Vector3> OnActivated;

    public static bool TurnedOn = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void TurnOnChaos()
    {
        TurnedOn = true;

        OnActivated?.Invoke(_prefabSpawning, _spawnOffset);
        StartCoroutine(SpawnProjectiles());
        SendMessage(_brokenMessage);
    }
    private IEnumerator SpawnProjectiles()
    {
        while (TurnedOn)
        {
            yield return new WaitForSeconds(_TimeBetweenSpawns);

            OnActivated?.Invoke(_prefabSpawning, _spawnOffset);
        }
    }
    public void TurnOffChaos()
    {
        TurnedOn = false;
        OnDeactivated?.Invoke();
        SendMessage(_fixedMessage);
       
    }
}
