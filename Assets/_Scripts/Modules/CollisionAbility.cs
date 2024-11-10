using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Collision ability",menuName ="ScriptableObjects/Abilities/CollisionAbility")]
public class CollisionAbility : AbilityAbstract
{
    public override void TurnOffAbility()
    {
       
        CollisionModuleController.instance.TurnOnCollision();
    }

    public override void TurnOnAbility()
    {
       
        CollisionModuleController.instance.TurnOffCollision();
    }

    public override void UseAbility()
    {
       if (CollisionModuleController.TurnedOn)
        {
            CollisionModuleController.instance.TurnOffCollision();
           
        }
        else
        {
            CollisionModuleController.instance.TurnOnCollision();
        }
    }
}
