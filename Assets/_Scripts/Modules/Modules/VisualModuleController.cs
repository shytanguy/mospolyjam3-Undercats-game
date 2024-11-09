using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VisualModuleController : ModuleControllerAbstract
{
    public static VisualModuleController instance;
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
