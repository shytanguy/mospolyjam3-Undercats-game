using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeController
{

    private static Object lastCaller;

    private static float _previousTimeScale;
    public static void StopTime(Object caller)
    {
        _previousTimeScale = Time.timeScale;
        lastCaller = caller;
        Time.timeScale = 0;
    }
    public static void SetTimeScale( float timescale)
    {
        Time.timeScale = 0;
        _previousTimeScale = Time.timeScale;

    }
    public static bool ResumeTime(Object caller)
    {
        if (caller == lastCaller)
        {
            Time.timeScale = _previousTimeScale;
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void ResumeTimeNoCaller()
    {
        Time.timeScale = _previousTimeScale;
    }
}