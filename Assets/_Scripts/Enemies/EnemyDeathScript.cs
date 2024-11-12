using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyDeathScript : MonoBehaviour
{
    private HealthScript _healthScript;

    [SerializeField] private GameObject _dropItemOnDeathPrefab;

    [SerializeField] private AudioClip _deathClip;
    [SerializeField] private GameObject _deathParticle;

    public UnityEvent OnDeath;
    private void Awake()
    {
        _healthScript = GetComponent<HealthScript>();
    }
    private void OnEnable()
    {
        _healthScript.OnDeath += Death;
    }

    private void Death()
    {
        OnDeath?.Invoke();
        if (_dropItemOnDeathPrefab != null)
        {
            Instantiate(_dropItemOnDeathPrefab, transform.position, transform.rotation);
        }
        if (_deathParticle != null)
        {
            Instantiate(_deathParticle, transform.position, transform.rotation);
        }
        if (_deathClip != null)
        {
            AudioManager.audioManager.PlaySound(_deathClip);
        }
        Destroy(gameObject);  
    }

    
}
