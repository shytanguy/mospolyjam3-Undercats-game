using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedAttackState : EnemyAbstractState
{
    [SerializeField] private ProjectileAbstract _projectile;

 
    [SerializeField] private float _animationTime = 1f;


    public override void EnterState()
    {
        base.EnterState();
        _enemyComponents.EnemyRigidbody.velocity = Vector2.zero;


        StartCoroutine(SwitchStateDelay());
    }

    private IEnumerator SwitchStateDelay()
    {
       
      
    
        Instantiate(_projectile, transform.position, Quaternion.identity).SetTarget(_enemyComponents.sightScript.GetPlayerTransform());
        yield return new WaitForSeconds(_animationTime);
        _statesManager.SwitchState(EnemyStatesManager.EnemyStates.follow);
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
