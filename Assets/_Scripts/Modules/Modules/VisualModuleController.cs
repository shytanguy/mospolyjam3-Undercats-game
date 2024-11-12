using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VisualModuleController : ModuleControllerAbstract
{
    public static VisualModuleController instance;
    public static event Action OnDeactivated;

    public static event Action OnActivated;

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
    public  void TurnOnGraphics()
    {
        OnActivated?.Invoke();
        CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.3f);
        AudioManager.audioManager.PlaySound(_deactivateSound);
        SendMessage(_fixedMessage);
    }
    public  void TurnOffGraphics()
    {
        OnDeactivated?.Invoke();
        CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.3f);
        AudioManager.audioManager.PlaySound(_activateSound);
        SendMessage(_brokenMessage);
    }
}
