using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionModuleController :MonoBehaviour
{
    public static CollisionModuleController instance;

    public static event Action OnDeactivated;

    public static event Action OnActivated;

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
        OnActivated?.Invoke();
    }
    public  void TurnOffCollision()
    {
        OnDeactivated?.Invoke();
    }
}
