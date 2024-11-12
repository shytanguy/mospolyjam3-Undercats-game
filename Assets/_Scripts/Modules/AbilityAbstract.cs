using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityAbstract : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }

    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public Color _color { get; private set; }


    [field: SerializeField] public bool useOnceOnSpawn { get; private set; }
    [field: SerializeField] public float cooldownTime { get; private set; }
    public abstract void TurnOnAbility();


    public abstract void TurnOffAbility();
   
    public abstract void UseAbility();
}
