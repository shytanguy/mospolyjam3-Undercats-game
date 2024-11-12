using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthModuleController : ModuleControllerAbstract
{
    public static HealthModuleController instance;

    public static event Action<float, float, GameObject> OnDeactivated;

    [field: SerializeField] public GameObject _effect { get; private set; }

    public static event Action OnActivated;

    [field:SerializeField] public float _healPercent { get; private set; } = 0.05f;
    [SerializeField] private AudioClip _activateSound;
    [SerializeField] private AudioClip _deactivateSound;
    [field: SerializeField] public float _cooldown { get; private set; } = 1f;

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
        CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.3f);
        AudioManager.audioManager.PlaySound(_activateSound);
        OnDeactivated?.Invoke(_healPercent, _cooldown, _effect);
        SendMessage(_brokenMessage);

    }
    public void TurnOffHeal()
    {
        TurnedOn = false;
        CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.3f);
        AudioManager.audioManager.PlaySound(_deactivateSound);
        OnActivated?.Invoke();
        SendMessage(_fixedMessage);
    }
}
