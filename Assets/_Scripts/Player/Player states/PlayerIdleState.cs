using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState :PlayerNormalStateAbstract
{
    public override void EnterState()
    {
        base.EnterState();
        _componentsManager.playerRigidbody.velocity = Vector2.zero;
     //   _componentsManager.playerInput.actions["Attack"].performed += OnAttackInput;
    }

    public override void ExitState()
    {
        base.ExitState();
        //  _componentsManager.playerInput.actions["Attack"].performed -= OnAttackInput;
    }

    public override void FixedUpdateState()
    {
       
    }

    public override void UpdateState()
    {
        CheckWalkState();
    }
    private void CheckWalkState()
    {
        if (_componentsManager.playerInput.actions["Move"].ReadValue<Vector2>() != Vector2.zero)
        {
            _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.walk);
        }
    }
   
   
}

