using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionModuleController :ModuleControllerAbstract
{
    public static CollisionModuleController instance;

    public static event Action OnDeactivated;

    public static event Action OnActivated;

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
        SendMessage(_fixedMessage);
    }
    public  void TurnOffCollision()
    {
        TurnedOn = false;
        OnDeactivated?.Invoke();
        CinemachineEffectsController.instance.ShakeCamera(5, 5, 0.2f);
        SendMessage(_brokenMessage);
    }
}
