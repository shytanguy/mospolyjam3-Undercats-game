using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightProjectile : ProjectileAbstract
{
   
    void Start()
    {
        _projectileRigidBody.velocity = _Speed*transform.right;
    }

    
}
