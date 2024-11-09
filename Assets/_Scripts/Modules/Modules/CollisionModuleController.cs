using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionModuleController :ModuleControllerAbstract
{
    public static CollisionModuleController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
