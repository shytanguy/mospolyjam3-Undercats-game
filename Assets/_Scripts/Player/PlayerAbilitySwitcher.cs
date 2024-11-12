using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilitySwitcher : MonoBehaviour
{
    [SerializeField] private List<AbilityAbstract> _playerAbilities = new List<AbilityAbstract>();

    private AbilityAbstract _currentAbility;

    public event Action<AbilityAbstract> OnSwitchingAbility;

    private PlayerComponentsManager _componentsManager;

    public event Action<AbilityAbstract> OnNewAbility;

    [SerializeField] private ParticleSystem _particle;

    private void Awake()
    {
        _componentsManager = GetComponent<PlayerComponentsManager>();
    }
    private void OnEnable()
    {
        _componentsManager.playerInput.actions["Switch Ability"].performed += SwitchAbilityInput;
    }
    private void OnDisable()
    {
        _componentsManager.playerInput.actions["Switch Ability"].performed -= SwitchAbilityInput;
    }
    public void AddAbility(AbilityAbstract ability)
    {
        if (_playerAbilities.Contains(ability)) return;
        _playerAbilities.Add(ability);
        OnNewAbility?.Invoke(ability);
    }
    private void SwitchAbilityInput(InputAction.CallbackContext context)
    {
        SwitchAbility();

    }
    private void Start()
    {
        if (_playerAbilities!=null)
        _currentAbility = _playerAbilities[0];

        SwitchAbility();
    }
    public AbilityAbstract GetNextAbility()
    {
        return _playerAbilities[(_playerAbilities.IndexOf(_currentAbility) + 1) % _playerAbilities.Count];
    }
    private void SwitchAbility()
    {

        
        _currentAbility = _playerAbilities[(_playerAbilities.IndexOf(_currentAbility) + 1) % _playerAbilities.Count];

        OnSwitchingAbility?.Invoke(_currentAbility);
    }

  public void UseAbility()
    {
       ParticleSystem.MainModule particle= Instantiate(_particle, transform.position, Quaternion.identity).main;
        particle.startColor = _currentAbility._color;
        _currentAbility.UseAbility();
    }
}
