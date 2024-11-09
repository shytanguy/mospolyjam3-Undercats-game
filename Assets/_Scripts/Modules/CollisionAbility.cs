using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Collision ability",menuName ="ScriptableObjects/Abilities/CollisionAbility")]
public class CollisionAbility : AbilityAbstract
{
    public override void TurnOffAbility()
    {
        base.TurnOffAbility();
        CollisionModuleController.instance.TurnOnCollision();
    }

    public override void TurnOnAbility()
    {
        base.TurnOnAbility();
        CollisionModuleController.instance.TurnOffCollision();
    }
}
