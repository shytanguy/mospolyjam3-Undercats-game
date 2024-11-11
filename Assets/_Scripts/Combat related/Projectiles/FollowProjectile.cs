using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowProjectile : ProjectileAbstract
{
    
    private void FixedUpdate()
    {
        if (!_hitbox.Reflected)
        {
            _projectileRigidBody.velocity =_Speed*(_Target.position - transform.position).normalized;
        }
    }
}
