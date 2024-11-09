using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollisionModuleController 
{
    public static event Action OnDeactivated;

    public static event Action OnActivated;

    public static void TurnOnCollision()
    {
        OnActivated?.Invoke();
    }
    public static void TurnOffCollision()
    {
        OnDeactivated?.Invoke();
    }
}
