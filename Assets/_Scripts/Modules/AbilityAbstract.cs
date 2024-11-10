using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityAbstract : ScriptableObject
{
    public Sprite Icon { get; private set; }

    public string Name { get; private set; }

    public bool AbilityOn { get; private set; } = false;

    [field: SerializeField] public bool useOnceOnSpawn { get; private set; }
    [field: SerializeField] public float cooldownTime { get; private set; }
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
