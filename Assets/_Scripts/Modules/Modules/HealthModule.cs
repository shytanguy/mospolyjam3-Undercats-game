using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModule : MonoBehaviour
{
    private HealthScript _healthScript;

    private OverlaySpriteScript _overlay;

    [SerializeField] private AudioClip _healSound;
    [SerializeField] private bool _TurnedOnOnStart = false;
    private bool _activeHeal;

    [SerializeField] private float _additionalHeal;
    private void Awake()
    {
        _healthScript = GetComponent<HealthScript>();
        _overlay = GetComponentInChildren<OverlaySpriteScript>();
    }
    private void Start()
    {
        if (_TurnedOnOnStart)
        {
            TurnOff(HealthModuleController.instance._healPercent,HealthModuleController.instance._cooldown,HealthModuleController.instance._effect);
        }
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
        _activeHeal = false;
        StopAllCoroutines();
    }
    private void TurnOff(float heal,float cooldown, GameObject prefabEffect)
    {
      
        StartCoroutine(HealRepeatedly(heal, cooldown,prefabEffect));
    }
    private IEnumerator HealRepeatedly(float healPercent,float cooldown, GameObject prefabEffect)
    {
        if (_activeHeal) { yield break; }
        _activeHeal = true;
        while (true)
        {
            yield return new WaitForSeconds(cooldown);
            _healthScript.Heal((healPercent+_additionalHeal) * _healthScript._maxHealth);
            _overlay.OverlayColorGreen();
            if (_healSound != null)
            {
                AudioManager.audioManager.PlaySound(_healSound);
            }
            if (prefabEffect != null)
            {
                Instantiate(prefabEffect, transform.position, Quaternion.identity);
            }
        }
    }
}
