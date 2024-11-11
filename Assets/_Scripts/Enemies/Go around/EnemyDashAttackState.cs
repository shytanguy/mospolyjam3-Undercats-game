using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDashAttackState : EnemyAbstractState
{
    [SerializeField] private HitBoxScript _hitBoxPrefab;

    [SerializeField] private float _animationTime = 1f;

    [SerializeField] private float _dashSpeed = 8f;
    public override void EnterState()
    {
        base.EnterState();
        _enemyComponents.EnemyRigidbody.velocity =_dashSpeed*(_enemyComponents.sightScript.GetPlayerTransform().position-transform.position).normalized;
        
        Instantiate(_hitBoxPrefab, transform);

        StartCoroutine(SwitchStateDelay());
    }

    private IEnumerator SwitchStateDelay()
    {
        yield return new WaitForSeconds(_animationTime);
        _statesManager.SwitchState(EnemyStatesManager.EnemyStates.follow);
    }
    public override void ExitState()
    {
        _enemyComponents.EnemyRigidbody.velocity = Vector2.zero;

    }

    public override void FixedUpdateState()
    {
      
    }

    public override void UpdateState()
    {
       
    }
}
