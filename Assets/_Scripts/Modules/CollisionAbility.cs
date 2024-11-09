using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Collision ability",menuName ="ScriptableObjects/Abilities/CollisionAbility")]
public class CollisionAbility : AbilityAbstract
{
    public override void TurnOffAbility()
    {
        CollisionModuleController.TurnOnCollision();
    }

    public override void TurnOnAbility()
    {
        CollisionModuleController.TurnOffCollision();
    }
}
