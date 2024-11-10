using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VisualModuleController : MonoBehaviour
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
    }
    public  void TurnOffGraphics()
    {
        OnDeactivated?.Invoke();
        FindFirstObjectByType<ErrorText>().SetText("ERROR VISUAL MODULE");
    }
}
