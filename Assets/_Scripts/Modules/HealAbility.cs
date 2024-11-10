using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Heal ability", menuName = "ScriptableObjects/Abilities/HealAbility")]
public class HealAbility : AbilityAbstract
{
    public override void TurnOffAbility()
    {
      
        HealthModuleController.instance.TurnOffHeal();
    }

    public override void TurnOnAbility()
    {

        HealthModuleController.instance.TurnOnHeal();
    }

    public override void UseAbility()
    {
        if (HealthModuleController.TurnedOn)
        {
            HealthModuleController.instance.TurnOffHeal();
        }
        else
        {
            HealthModuleController.instance.TurnOnHeal();
        }
    
}
}
