using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionModule : MonoBehaviour
{
    private Collider2D _collider;

    private void OnEnable()
    {
        CollisionModuleController.OnActivated += TurnOn;
        CollisionModuleController.OnDeactivated -= TurnOff;
    }
    private void OnDisable()
    {
        
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
