using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Burn ability", menuName = "ScriptableObjects/Abilities/BurnAbility")]
public class BurnAbility : AbilityAbstract
{
    public override void TurnOffAbility()
    {


        TemperatureModuleController.instance.TurnOffTemperature();
    }
    public override void TurnOnAbility()
    {
       
        TemperatureModuleController.instance.TurnOnTemperature();
        
    }

    public override void UseAbility()
    {
      if (TemperatureModuleController.TurnedOn)
        {
            TemperatureModuleController.instance.TurnOffTemperature();
        }
        else
        {
            TemperatureModuleController.instance.TurnOnTemperature();
        }
    }
}
