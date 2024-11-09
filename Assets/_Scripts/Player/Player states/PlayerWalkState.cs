using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerWalkState : PlayerNormalStateAbstract
{
    [SerializeField] private float _speed;
  
    public override void FixedUpdateState()
    {
        CheckState();
        MovePlayer();
    }

    public override void UpdateState()
    {
      
    }

    private void MovePlayer()
    {
        Vector2 direction =_speed* _componentsManager.playerInput.actions["Move"].ReadValue<Vector2>();

        if (direction.x > 0)
        {
            transform.rotation = Quaternion.identity;

        }
        else if (direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        _componentsManager.playerRigidbody.velocity = direction;
    }
    private void CheckState()
    {
        if (_componentsManager.playerInput.actions["Move"].ReadValue<Vector2>()== Vector2.zero)
        {
            _StatesManager.SwitchState(PlayerStatesManager.PlayerStates.idle);
        }
    }
}
