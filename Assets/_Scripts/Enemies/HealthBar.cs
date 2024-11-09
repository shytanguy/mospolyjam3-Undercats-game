using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
  [SerializeField]  private HealthScript _healthScript;

    private Transform _HealthBarVisual;

    private float _initialSizeX;

    private void Awake()
    {
        _HealthBarVisual = transform.GetChild(0);
        _initialSizeX = _HealthBarVisual.localScale.x;

        Debug.Log($"{_healthScript._maxHealth * _initialSizeX}");
    }
    private void OnEnable()
    {
        _healthScript.OnDamageTaken += ChangeBarSize;
        _healthScript.OnHealed += ChangeBarSize;
    }
    private void OnDisable()
    {
        _healthScript.OnDamageTaken -= ChangeBarSize;
        _healthScript.OnHealed -= ChangeBarSize;
    }
    private void ChangeBarSize(float newHp)
    {
        _HealthBarVisual.localScale = new Vector3((float)newHp / (float)_healthScript._maxHealth * _initialSizeX, _HealthBarVisual.localScale.y, 1);
    }
}
