using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstractState : BaseStateAbstract<EnemyStatesManager.EnemyStates>
{
    protected EnemyStatesManager _statesManager;
    protected EnemyComponentManager _enemyComponents;
    protected void Awake()
    {
        _statesManager = GetComponent<EnemyStatesManager>();

        _enemyComponents = GetComponent<EnemyComponentManager>();
    }
}
