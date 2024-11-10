using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthModuleController : ModuleControllerAbstract
{
    public static HealthModuleController instance;

    public static event Action<float, float, GameObject> OnDeactivated;

    [SerializeField] private GameObject _effect;

    public static event Action OnActivated;

    [SerializeField] private float _healPercent=0.05f;

    [SerializeField] private float _cooldown = 1f;

    public static bool TurnedOn=false;
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
    public void TurnOnHeal()
    {
        TurnedOn = true;
        OnDeactivated?.Invoke(_healPercent, _cooldown, _effect);
        SendMessage(_brokenMessage);

    }
    public void TurnOffHeal()
    {
        TurnedOn = false;
     
        OnActivated?.Invoke();
        SendMessage(_fixedMessage);
    }
}
