using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    private HealthScript _healthScript;

    private OverlaySpriteScript _overlay;

    [SerializeField] private AudioClip _healSound;
    private void Awake()
    {
        _healthScript = GetComponent<HealthScript>();
        _overlay = GetComponentInChildren<OverlaySpriteScript>();
    }
    private void OnEnable()
    {
        HealthModuleController.OnActivated += TurnOn;

        HealthModuleController.OnDeactivated += TurnOff;
    }

    private void OnDisable()
    {
        HealthModuleController.OnActivated -= TurnOn;

        HealthModuleController.OnDeactivated -= TurnOff;
    }
    private void TurnOn()
    {
        StopAllCoroutines();
    }
    private void TurnOff(float heal,float cooldown)
    {
        StartCoroutine(HealRepeatedly(heal, cooldown));
    }
    private IEnumerator HealRepeatedly(float healPercent,float cooldown)
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            _healthScript.Heal(healPercent * _healthScript._maxHealth);
            _overlay.OverlayColorGreen();
            if (_healSound != null)
            {
                AudioManager.audioManager.PlaySound(_healSound);
            }
        }
    }
}
