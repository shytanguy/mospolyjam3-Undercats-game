using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerNormalStateAbstract : PlayerAbstractState
{
    public override void EnterState()
    {
        _componentsManager.playerInput.actions["Attack"].performed += OnAttackInput;
    }
    public override void ExitState()
    {
        _componentsManager.playerInput.actions["Attack"].performed -= OnAttackInput;
    }
    private void OnAttackInput(InputAction.CallbackContext context)
    {
        Vector2 direction = _componentsManager.playerInput.actions["Move"].ReadValue<Vector2>();

        switch (direction.y)
        {
            case 0: _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.attackMiddle); break;
            case (> 0): _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.attackTop); break;
            case (< 0): _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.attackDown); break;
        }
    }
}
