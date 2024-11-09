using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAbstractState : BaseStateAbstract<PlayerStatesManager.PlayerStates>
{
    protected PlayerComponentsManager _componentsManager;

    protected PlayerStatesManager _StatesManager;

    public enum StateType
    {
        normal,
        attack,
        special
        
    }
    [field: SerializeField] public StateType stateType { get; private set; }
    private void Awake()
    {
        _componentsManager = GetComponent<PlayerComponentsManager>();

        _StatesManager = GetComponent<PlayerStatesManager>();
    }
   
    
 
 
}
