using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
   [SerializeField] private HealthScript _playerHealth;
    private Slider _healthBar;

    private void Awake()
    {
        _healthBar = GetComponent<Slider>();
    }
    private void OnEnable()
    {
        _playerHealth.OnDamageTaken += UpdateBar;
        _playerHealth.OnHealed += UpdateBar;
    }
    private void OnDisable()
    {
        _playerHealth.OnDamageTaken -= UpdateBar;
        _playerHealth.OnHealed -= UpdateBar;
    }
    private void UpdateBar(float newHp)
    {
        _healthBar.value = newHp / _playerHealth._maxHealth;
    }
}
