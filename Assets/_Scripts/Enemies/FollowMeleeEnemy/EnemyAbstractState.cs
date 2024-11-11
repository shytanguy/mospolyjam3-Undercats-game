using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstractState : BaseStateAbstract<EnemyStatesManager.EnemyStates>
{
    protected EnemyStatesManager _statesManager;
    protected EnemyComponentManager _enemyComponents;
    [SerializeField] private string _animationName;
    protected void Awake()
    {
        _statesManager = GetComponent<EnemyStatesManager>();

        _enemyComponents = GetComponent<EnemyComponentManager>();
    }
    public override void EnterState()
    {
        _enemyComponents.EnemyAnimator.Play(_animationName);
    }
    protected void TurnAroundForPlayer()
    {
        if (_enemyComponents.sightScript.GetPlayerTransform() != null)
        {
            if (_enemyComponents.sightScript.GetPlayerTransform().position.x > transform.position.x && transform.rotation.eulerAngles.y != 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (_enemyComponents.sightScript.GetPlayerTransform().position.x < transform.position.x && transform.rotation.eulerAngles.y == 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}
