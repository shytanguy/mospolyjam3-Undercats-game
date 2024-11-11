using UnityEngine;

public class EnemyEscapeState : EnemyAbstractState
{
    [SerializeField] private float _escapeSpeed = 3f;
    [SerializeField] private float _triggerEscapeDistance = 2f; 
    [SerializeField] private float _attackDistance = 5f; 
    [SerializeField] private float _obstacleAvoidanceRange = 1f;
    [SerializeField] private LayerMask _obstacleLayer;

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        _enemyComponents.EnemyRigidbody.velocity = Vector2.zero; 
    }

    public override void UpdateState()
    {
     
    }

    public override void FixedUpdateState()
    {
        MoveAwayFromPlayer();
        TurnAroundForPlayer();
    }

    private void MoveAwayFromPlayer()
    {
        Transform playerTransform = _enemyComponents.sightScript.GetPlayerTransform();

     
        if (playerTransform == null)
        {
            _statesManager.SwitchState(EnemyStatesManager.EnemyStates.idle);
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

    
        if (distanceToPlayer < _triggerEscapeDistance)
        {
           
            if (distanceToPlayer >= _attackDistance)
            {
                _statesManager.SwitchState(EnemyStatesManager.EnemyStates.attack);
                return;
            }

           
            Vector2 escapeDirection = (transform.position - playerTransform.position).normalized;

          
            RaycastHit2D hit = Physics2D.Raycast(transform.position, escapeDirection, _obstacleAvoidanceRange, _obstacleLayer);
            if (hit.collider != null)
            {
              
                Vector2 avoidDirection = Vector2.Perpendicular(escapeDirection).normalized;
                escapeDirection += avoidDirection;
            }

            _enemyComponents.EnemyRigidbody.velocity = escapeDirection.normalized * _escapeSpeed;
        }
        else
        {
           
            _enemyComponents.EnemyRigidbody.velocity = Vector2.zero;
            _statesManager.SwitchState(EnemyStatesManager.EnemyStates.idle);
        }
    }
}
