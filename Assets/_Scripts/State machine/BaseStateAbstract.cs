using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class BaseStateAbstract<StatesEnum> : MonoBehaviour where StatesEnum: Enum
{
    public StatesEnum stateKey;

    public bool startingState=false;
    public abstract void EnterState();

    public abstract void ExitState();

    public abstract void FixedUpdateState();

    public abstract void UpdateState();




}
