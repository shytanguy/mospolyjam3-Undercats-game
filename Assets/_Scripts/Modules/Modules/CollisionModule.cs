using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionModule : MonoBehaviour
{
    private Collider2D _collider;
    [SerializeField] private bool _TurnedOffOnStart = false;
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
    private void Start()
    {
        if (_TurnedOffOnStart)
        {
            TurnOff();
        }
    }
    private void OnEnable()
    {
        CollisionModuleController.OnActivated += TurnOn;
        CollisionModuleController.OnDeactivated += TurnOff;
    }
    private void OnDisable()
    {
        CollisionModuleController.OnActivated -= TurnOn;
        CollisionModuleController.OnDeactivated -= TurnOff;
    }
    private void TurnOff()
    {
        _collider.enabled = false;
       
    }
    private void TurnOn()
    {
        _collider.enabled = true;
    }
}
