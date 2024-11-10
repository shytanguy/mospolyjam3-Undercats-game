using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModuleControllerAbstract : MonoBehaviour
{
    [SerializeField] protected string _fixedMessage;
    [SerializeField] protected string _brokenMessage;

    public event Action<string> OnActivationMessage;

    protected void SendMessage(string message)
    {
        OnActivationMessage?.Invoke(message);
    }
}
