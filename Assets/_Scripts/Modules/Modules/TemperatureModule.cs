using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureModule : MonoBehaviour
{
    private HealthScript _healthScript;

    private OverlaySpriteScript _overlay;

    [SerializeField] private AudioClip _burnSound;
    [SerializeField] private float _additionalDamage;

    [SerializeField] private bool _activeOnStart=false;
    private bool _isActive;
    private void Awake()
    {
        _healthScript = GetComponent<HealthScript>();
        _overlay = GetComponentInChildren<OverlaySpriteScript>();
    }
    private void Start()
    {
        if (_activeOnStart)
        {
            TurnOff(TemperatureModuleController.instance._burnPercent,TemperatureModuleController.instance._cooldown,TemperatureModuleController.instance._effect);
        }
    }
    private void OnEnable()
    {
        TemperatureModuleController.OnActivated += TurnOn;

        TemperatureModuleController.OnDeactivated += TurnOff;
    }

    private void OnDisable()
    {
        TemperatureModuleController.OnActivated -= TurnOn;

        TemperatureModuleController.OnDeactivated -= TurnOff;
    }
    private void TurnOn()
    {
        _isActive = false;
        StopAllCoroutines();
    }
    private void TurnOff(float heal, float cooldown, GameObject prefabEffect)
    {

        StartCoroutine(HealRepeatedly(heal, cooldown, prefabEffect));
    }
    private IEnumerator HealRepeatedly(float healPercent, float cooldown, GameObject prefabEffect)
    {
        if (_isActive) { yield break; }
        _isActive = true;
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            _healthScript.TakeDamage((healPercent+_additionalDamage) * _healthScript._maxHealth);
            _overlay.OverlayColorRed();
            if (_burnSound != null)
            {
                AudioManager.audioManager.PlaySound(_burnSound);
            }
            if (prefabEffect != null)
            {
                Instantiate(prefabEffect, transform.position, Quaternion.identity);
            }
        }
    }
}
