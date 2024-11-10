using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthModuleController : MonoBehaviour
{
    public static HealthModuleController instance;

    public static event Action<float, float, GameObject> OnDeactivated;

    [SerializeField] private GameObject _effect;

    public static event Action OnActivated;

    [SerializeField] private float _healPercent=0.05f;

    [SerializeField] private float _cooldown = 1f;
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
        OnActivated?.Invoke();
    }
    public void TurnOffHeal()
    {
        OnDeactivated?.Invoke(_healPercent, _cooldown,_effect);
        FindFirstObjectByType<ErrorText>().SetText("ERROR HEALTH MODULE");
    }
}
