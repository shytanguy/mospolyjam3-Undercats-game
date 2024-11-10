using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VisualModuleController : ModuleControllerAbstract
{
    public static VisualModuleController instance;
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
    public  void TurnOnGraphics()
    {
        OnActivated?.Invoke();
        SendMessage(_fixedMessage);
    }
    public  void TurnOffGraphics()
    {
        OnDeactivated?.Invoke();
        SendMessage(_brokenMessage);
    }
}
