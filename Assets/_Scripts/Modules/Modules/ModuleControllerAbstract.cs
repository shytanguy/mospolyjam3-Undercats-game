using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class ModuleControllerAbstract : MonoBehaviour
{
  

    public static event Action OnDeactivated;

    public static event Action OnActivated;

   
    public void TurnOn()
    {
        OnActivated?.Invoke();
    }
    public void TurnOff()
    {
        OnDeactivated?.Invoke();
    }
}
