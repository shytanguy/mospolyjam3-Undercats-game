using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatesManager :StateMachineAbstract<EnemyStatesManager.EnemyStates>
{
   public enum EnemyStates
    {
        idle,
        follow,
        attack
    }
}
