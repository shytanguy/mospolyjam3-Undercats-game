using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnDamage : MonoBehaviour
{
    private HitBoxScript _hitBox;

    private void Awake()
    {
        _hitBox = GetComponent<HitBoxScript>();
    }
    private void OnEnable()
    {
        _hitBox.OnDamage += DestroyGameObject;
    }
    private void OnDisable()
    {
        _hitBox.OnDamage -= DestroyGameObject;
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
