using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialAbilityState : PlayerAbstractState
{
    [SerializeField] private float _timeInState;
    public override void EnterState()
    {
        StartCoroutine(SwitchStateDelay());
        _componentsManager.playerRigidbody.velocity = Vector2.zero;
        _componentsManager.abilitySwitcher.UseAbility();
    }
    private IEnumerator SwitchStateDelay()
    {
        yield return new WaitForSeconds(_timeInState);
        if (_StatesManager.GetStateKey() == this.stateKey)
        _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.idle);
    }
    public override void ExitState()
    {
      
    }

    public override void FixedUpdateState()
    {
       
    }

    public override void UpdateState()
    {
   
    }
}
