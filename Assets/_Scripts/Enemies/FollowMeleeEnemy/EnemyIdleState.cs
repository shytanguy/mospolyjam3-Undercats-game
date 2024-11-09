using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyAbstractState
{
    public override void EnterState()
    {
 
    }

    public override void ExitState()
    {
   
    }

    public override void FixedUpdateState()
    {
       if(_enemyComponents.sightScript.CheckForPlayer())
        {
            _statesManager.SwitchState(EnemyStatesManager.EnemyStates.follow);
        }
    }

    public override void UpdateState()
    {
        
    }
}
