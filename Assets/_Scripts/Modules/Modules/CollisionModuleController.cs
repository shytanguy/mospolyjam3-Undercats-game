using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionModuleController :ModuleControllerAbstract
{
    public static CollisionModuleController instance;

    public static event Action OnDeactivated;

    public static event Action OnActivated;

    [SerializeField] private AudioClip _activateSound;
    [SerializeField] private AudioClip _deactivateSound;
    public static bool TurnedOn=true;
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
    public  void TurnOnCollision()
    {
        TurnedOn = true;
        OnActivated?.Invoke();
        CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.3f);
        AudioManager.audioManager.PlaySound(_deactivateSound);
        SendMessage(_fixedMessage);
    }
    public  void TurnOffCollision()
    {
        TurnedOn = false;
        OnDeactivated?.Invoke();
        CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.3f);
        AudioManager.audioManager.PlaySound(_activateSound);
        StartCoroutine(TurnCollisionBack());
        SendMessage(_brokenMessage);
    }
    private IEnumerator TurnCollisionBack()
    {
        yield return new WaitForSeconds(1f);
        if (TurnedOn) yield break;
        TurnOnCollision();
    }
}
