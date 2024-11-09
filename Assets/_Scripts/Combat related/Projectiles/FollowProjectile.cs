using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : ProjectileAbstract
{
    private Transform _Target;

    public void SetTarget(Transform newTarget)
    {
        _Target = newTarget;
    }
    private void FixedUpdate()
    {
        if (!_hitbox.Reflected)
        {
            _projectileRigidBody.velocity =_Speed*(_Target.position - transform.position).normalized;
        }
    }
}
