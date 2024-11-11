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

        SendMessage(_fixedMessage);
    }
    public void TurnOnTemperature()
    {
       
        TurnedOn = true;
        OnDeactivated?.Invoke(_burnPercent, _cooldown, _effect);
        SendMessage(_brokenMessage);
    }
}
