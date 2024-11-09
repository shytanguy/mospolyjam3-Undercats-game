using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityAbstract : ScriptableObject
{
    public Sprite Icon { get; private set; }

    public string Name { get; private set; }

    public bool AbilityOn { get; private set; } = false;


    public virtual void TurnOnAbility()
    {
        if (AbilityOn) return;
        AbilityOn = true;
       
    }
   
    public virtual void TurnOffAbility()
    {
        if (!AbilityOn) return;
        AbilityOn = false;
    }

}
