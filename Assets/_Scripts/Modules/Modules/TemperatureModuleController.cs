using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureModuleController : ModuleControllerAbstract
{
    public static TemperatureModuleController instance;

    public static event Action<float, float, GameObject> OnDeactivated;

    [SerializeField] private GameObject _effect;

    public static event Action OnActivated;
    public static bool TurnedOn=false;

    [SerializeField] private float _burnPercent = 0.05f;

    [SerializeField] private float _cooldown = 1f;
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
