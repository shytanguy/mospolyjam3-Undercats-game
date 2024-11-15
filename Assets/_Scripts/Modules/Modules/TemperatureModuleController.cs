using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureModuleController : ModuleControllerAbstract
{
    public static TemperatureModuleController instance;

    public static event Action<float, float, GameObject> OnDeactivated;

    [field: SerializeField] public GameObject _effect { get; private set; }

    public static event Action OnActivated;
    public static bool TurnedOn=false;

    [field:SerializeField] public float _burnPercent { get; private set; } = 0.05f;

    [field: SerializeField] public float _cooldown { get; private set; } = 1f;

    [SerializeField] private AudioClip _activateSound;
    [SerializeField] private AudioClip _deactivateSound;
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
    public void TurnOffTemperature()
    {
        TurnedOn = false;
        OnActivated?.Invoke();
        AudioManager.audioManager.PlaySound(_deactivateSound);
        CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.3f);
        SendMessage(_fixedMessage);
    }
    public void TurnOnTemperature()
    {
       
        TurnedOn = true;
        CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.3f);
        AudioManager.audioManager.PlaySound(_activateSound);
        OnDeactivated?.Invoke(_burnPercent, _cooldown, _effect);
        SendMessage(_brokenMessage);
    }
}
