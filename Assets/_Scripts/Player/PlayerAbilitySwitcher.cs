using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilitySwitcher : MonoBehaviour
{
    public List<AbilityAbstract> _playerAbilities { get; private set; } = new List<AbilityAbstract>();

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
        _currentAbility = _playerAbilities.Find(i => i == ability);
        OnSwitchingAbility?.Invoke(ability);
        OnNewAbility?.Invoke(ability);
    }
    private void SwitchAbilityInput(InputAction.CallbackContext context)
    {
        SwitchAbility();

    }
    private void Start()
    {
        if (_playerAbilities!=null&&_playerAbilities.Count>0)
        _currentAbility = _playerAbilities[0];

        SwitchAbility();
    }
    public AbilityAbstract GetNextAbility()
    {
        if (_playerAbilities.Count <= 1)
        {
            return null;
        }
        
        return _playerAbilities[(_playerAbilities.IndexOf(_currentAbility) + 1) % _playerAbilities.Count];
    }
    private void SwitchAbility()
    {

        if (_playerAbilities.Count <= 1) return;
        _currentAbility = _playerAbilities[(_playerAbilities.IndexOf(_currentAbility) + 1) % _playerAbilities.Count];

        OnSwitchingAbility?.Invoke(_currentAbility);
    }

  public void UseAbility()
    {
        if (_currentAbility == null) return;
       ParticleSystem.MainModule particle= Instantiate(_particle, transform.position, Quaternion.identity).main;
        particle.startColor = _currentAbility._color;
        _currentAbility.UseAbility();
    }
}
