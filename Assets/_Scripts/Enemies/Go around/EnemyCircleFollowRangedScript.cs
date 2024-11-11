using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleFollowRangedScript : EnemyAbstractState
{
    [SerializeField] private float _circleSpeed = 2f;
    [SerializeField] private float _circleDistance = 3f;
    [SerializeField] private float _obstacleAvoidanceRange = 1f;
    [SerializeField] private LayerMask _obstacleLayer;

    [SerializeField] private float _distanceToHit = 0.3f;

    [SerializeField] private float _TimeInCircle = 1.5f;

    private float _timer = 0;

    [SerializeField] private ProjectileAbstract _rangedPrefab;
    public override void EnterState()
    {
        base.EnterState();
        _timer = Time.time;
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
       
    }

    public override void FixedUpdateState()
    {
        MoveAroundTarget();
        TurnAroundForPlayer();
        if (Time.time - _timer >= _TimeInCircle)
        {
            _timer = Time.time;
            Instantiate(_rangedPrefab, transform.position, Quaternion.identity).SetTarget(_enemyComponents.sightScript.GetPlayerTransform());
        }
    }

    private void MoveAroundTarget()
    {
        Transform playerTransform = _enemyComponents.sightScript.GetPlayerTransform();

        if (playerTransform == null)
        {
            _statesManager.SwitchState(EnemyStatesManager.EnemyStates.idle);
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);




        if (distanceToPlayer <= _circleDistance)
        {
            Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
            Vector2 tangentDirection = Vector2.Perpendicular(directionToPlayer);


            RaycastHit2D hit = Physics2D.Raycast(transform.position, tangentDirection, _obstacleAvoidanceRange, _obstacleLayer);
            if (hit.collider != null)
            {

                tangentDirection = -tangentDirection;
            }

            _enemyComponents.EnemyRigidbody.velocity = tangentDirection * _circleSpeed;
            
        }
        else
        {
           
            Vector2 moveDirection = (playerTransform.position - transform.position).normalized;
            _enemyComponents.EnemyRigidbody.velocity = moveDirection * _circleSpeed;
        }
    }
}
