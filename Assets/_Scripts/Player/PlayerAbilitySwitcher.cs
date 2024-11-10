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
        _currentAbility.UseAbility();
    }
}
