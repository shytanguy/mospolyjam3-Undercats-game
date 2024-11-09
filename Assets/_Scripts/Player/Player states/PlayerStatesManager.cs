using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerStatesManager : StateMachineAbstract<PlayerStatesManager.PlayerStates>
{
  public enum PlayerStates
    {
        idle,
        walk,
        attackMiddle,
        attackTop,
        attackDown,
        dash,
        special,
        death,
        doNothing
    }
   public PlayerAbstractState.StateType GetStateType()
    {
        return ((PlayerAbstractState)currentState).stateType;
    }
}
