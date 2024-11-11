using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySimpleFollowState : EnemyAbstractState
{
    [SerializeField] private float _followSpeed = 3f;
    [SerializeField] private float _followDistance = 12f;
    [SerializeField] private float _obstacleAvoidanceRange = 1f;
    [SerializeField] private LayerMask _obstacleLayer;

    [SerializeField] private float _distanceToHit = 0.3f;
    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
     
    }

    public override void UpdateState()
    {
     
    }

    public override void FixedUpdateState()
    {
        MoveCloser();
        TurnAroundForPlayer();
    }

    private void MoveCloser()
    {
        if (_enemyComponents.sightScript.GetPlayerTransform() == null)
        {
            _statesManager.SwitchState(EnemyStatesManager.EnemyStates.idle);
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, _enemyComponents.sightScript.GetPlayerTransform().position);

        if (distanceToPlayer < _followDistance)
        {
            if (distanceToPlayer <= _distanceToHit)
            {
                _statesManager.SwitchState(EnemyStatesManager.EnemyStates.attack);
                return;
            }
            Vector2 direction = (_enemyComponents.sightScript.GetPlayerTransform().position - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _obstacleAvoidanceRange, _obstacleLayer);
            if (hit.collider != null)
            {
                Vector2 avoidDirection = Vector2.Perpendicular(direction).normalized;

                direction += avoidDirection;
            }

            _enemyComponents.EnemyRigidbody.velocity = direction.normalized * _followSpeed;
        }
        else
        {
            _enemyComponents.EnemyRigidbody.velocity = Vector2.zero;
            _statesManager.SwitchState(EnemyStatesManager.EnemyStates.idle);
        }
    }

  
}

