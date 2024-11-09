using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilitySwitcher : MonoBehaviour
{
    [SerializeField] private List<AbilityAbstract> _playerAbilities = new List<AbilityAbstract>();

    private AbilityAbstract _currentAbility;

    public event Action<AbilityAbstract> OnSwitchingAbility;
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
        if (_currentAbility.AbilityOn)
        {
            _currentAbility.TurnOffAbility();
        }
        else
        {
            _currentAbility.TurnOnAbility();
        }
    }
}
