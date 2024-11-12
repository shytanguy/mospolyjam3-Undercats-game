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

    [SerializeField] private AudioClip _activateSound;
    [SerializeField] private AudioClip _deactivateSound;

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

      
        StartCoroutine(SpawnProjectiles());
        SendMessage(_brokenMessage);
    }
    private IEnumerator SpawnProjectiles()
    {
        while (TurnedOn)
        {

            yield return new WaitForSeconds(_TimeBetweenSpawns);
            CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.5f);
            AudioManager.audioManager.PlaySound(_activateSound);
            OnActivated?.Invoke(_prefabSpawning, _spawnOffset);
        }
    }
    public void TurnOffChaos()
    {
        TurnedOn = false;
        CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.3f);
        AudioManager.audioManager.PlaySound(_deactivateSound);
        OnDeactivated?.Invoke();
        SendMessage(_fixedMessage);
       
    }
}
