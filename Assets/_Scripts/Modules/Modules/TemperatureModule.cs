using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureModule : MonoBehaviour
{
    private HealthScript _healthScript;

    private OverlaySpriteScript _overlay;

    [SerializeField] private AudioClip _burnSound;
    private void Awake()
    {
        _healthScript = GetComponent<HealthScript>();
        _overlay = GetComponentInChildren<OverlaySpriteScript>();
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
        StopAllCoroutines();
    }
    private void TurnOff(float heal, float cooldown, GameObject prefabEffect)
    {
        StartCoroutine(HealRepeatedly(heal, cooldown, prefabEffect));
    }
    private IEnumerator HealRepeatedly(float healPercent, float cooldown, GameObject prefabEffect)
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            _healthScript.TakeDamage(healPercent * _healthScript._maxHealth);
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
