using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
public abstract class StateMachineAbstract<StatesEnum> : MonoBehaviour where StatesEnum: Enum
{
    protected Dictionary<StatesEnum, BaseStateAbstract<StatesEnum>> States = new Dictionary<StatesEnum, BaseStateAbstract<StatesEnum>>();

    protected BaseStateAbstract<StatesEnum> currentState;

    public event Action<StatesEnum,StatesEnum> StateChangedEvent;

    protected StatesEnum previusStateKey;
    protected virtual void Awake()
    {
            foreach (var state in GetComponents<BaseStateAbstract<StatesEnum>>())
            {
                States.Add(state.stateKey, state);

                if (state.startingState) currentState = state;
            }
        
    }
    protected void Start()
    {
 
        currentState.EnterState();
        
    }
    protected void FixedUpdate()
    {


        currentState.FixedUpdateState();
    }
    protected void Update()
    {


        currentState.UpdateState();
    }

    public StatesEnum GetStateKey()
    {
        return currentState.stateKey;
    }
    public virtual void SwitchToPreviousState()
    {
         SwitchState(previusStateKey);
    }
    public virtual void SwitchState(StatesEnum key)
    {

       if (!States.ContainsKey(key))
            {
                return;
            }
            currentState.ExitState();
            previusStateKey = currentState.stateKey;
           // Debug.Log($"{gameObject.name} exiting {currentState}");
            StateChangedEvent?.Invoke(currentState.stateKey, key);
            currentState = States[key];
          //  Debug.Log($"{gameObject.name} entering {currentState}");
            currentState.EnterState();

      
    }
  
}
