using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerNormalStateAbstract : PlayerAbstractState
{
    public override void EnterState()
    {
        _componentsManager.playerInput.actions["Attack"].performed += OnAttackInput;
        _componentsManager.playerInput.actions["Dash"].performed += OnDashInput;
        _componentsManager.playerInput.actions["Special"].performed += OnSpecialInput;
    }
    public override void ExitState()
    {
        _componentsManager.playerInput.actions["Attack"].performed -= OnAttackInput;
        _componentsManager.playerInput.actions["Dash"].performed -= OnDashInput;
        _componentsManager.playerInput.actions["Special"].performed -= OnSpecialInput;
    }
    private void OnAttackInput(InputAction.CallbackContext context)
    {
        Vector2 direction = _componentsManager.playerInput.actions["Move"].ReadValue<Vector2>();
        if (direction.x > 0)
        {
            transform.rotation = Quaternion.identity;

        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        switch (direction.y)
        {
            case 0: _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.attackMiddle); break;
            case (> 0): _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.attackTop); break;
            case (< 0): _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.attackDown); break;
        }
    }
    private void OnDashInput(InputAction.CallbackContext context)
    {
        _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.dash);
    }
    private void OnSpecialInput(InputAction.CallbackContext context)
    {
        _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.special);
    }
}
