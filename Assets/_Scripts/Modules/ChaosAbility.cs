using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Chaos ability", menuName = "ScriptableObjects/Abilities/Chaos ability")]
public class ChaosAbility : AbilityAbstract
{
    public override void TurnOffAbility()
    {
       ChaosModuleController.instance.TurnOffChaos();
    }
    public override void TurnOnAbility()
    {

        ChaosModuleController.instance.TurnOnChaos();

    }

    public override void UseAbility()
    {
        if (ChaosModuleController.TurnedOn)
        {
            ChaosModuleController.instance.TurnOffChaos();
        }
        else
        {
            ChaosModuleController.instance.TurnOnChaos();
        }
    }
}
