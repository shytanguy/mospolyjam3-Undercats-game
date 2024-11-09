using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyAbstractState
{
    [SerializeField] private HitBoxScript _hitBoxPrefab;

    [SerializeField] private Vector2 _spawnHitBoxOffset;
    [SerializeField] private float _animationTime = 1f;
    public override void EnterState()
    {
        Vector3 offset;
        if (transform.rotation.eulerAngles.y == 0)
        {
            offset = _spawnHitBoxOffset;
        }
        else
        {
            offset = new Vector3(-_spawnHitBoxOffset.x, _spawnHitBoxOffset.y);
        }
        Instantiate(_hitBoxPrefab, transform.position + offset, Quaternion.identity);
    }

    private IEnumerator SwitchStateDelay()
    {
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
