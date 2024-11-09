using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerNormalStateAbstract
{

    [SerializeField] private float _timeInDash;
    [SerializeField] private float _dashSpeed;
    public override void EnterState()
    {
        base.EnterState();

        Vector2 direction = _componentsManager.playerInput.actions["Move"].ReadValue<Vector2>();

        if (direction != Vector2.zero)
        {
            _componentsManager.playerRigidbody.velocity = _dashSpeed * direction;
        }
        else
        {
            _componentsManager.playerRigidbody.velocity = _dashSpeed * transform.right;
        }

        StartCoroutine(SwitchStateDelay());
    }

    private IEnumerator SwitchStateDelay()
    {
        yield return new WaitForSeconds(_timeInDash);

        _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.idle);
    }
    public override void FixedUpdateState()
    {
      
    }

    public override void UpdateState()
    {
        
    }

    
}
