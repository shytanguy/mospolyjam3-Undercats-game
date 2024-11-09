using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{

    [SerializeField] private float _maxHealth = 100;
    [HideInInspector] public float _currentHealth { get; private set; }

    [Tooltip("Если включено, то после получения урона, у персонажа будет какое-то время с неуязвимостью")]
    [SerializeField] private bool _damageCoolDownEnabled = false;

    [SerializeField] private float _invincibilityDelay = 0.5f;
    private bool _isInvincible;

    public event Action<float> OnDamageTaken; 
    public event Action<float> OnHealed;     
    public event Action OnDeath;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (_isInvincible || _currentHealth <= 0) return;

        _currentHealth -= damage;

        OnDamageTaken?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }
        else if (_damageCoolDownEnabled)
        {
            
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    public void Heal(float healAmount)
    {
        if (_currentHealth <= 0) return;

        _currentHealth += healAmount;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        OnHealed?.Invoke(_currentHealth);
    }

    private void Die()
    {
        OnDeath?.Invoke();
       
    }

    private IEnumerator InvincibilityCoroutine()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibilityDelay);
        _isInvincible = false;
    }

    public void SetMaxHealth(float newMaxHealth)
    {
        _maxHealth = newMaxHealth;

        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public bool IsAlive()
    {
        return _currentHealth > 0;
    }
}


