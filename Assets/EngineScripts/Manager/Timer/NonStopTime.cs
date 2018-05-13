using System;
using System.Collections.Generic;
using UnityEngine;

public class NonStopTime : MonoBehaviour
{
    bool paused = false;

    float gameDeltaTime = 0;

    float lastTimeSinceStartup = 0;

    public bool Paused
    {
        get { return paused; }
        set { paused = value; }
    }

    public float time
    {
        get
        {
            return lastTimeSinceStartup;
        }
    }

    public float deltaTime
    {
        get
        {
            if (!paused)
            {
                return gameDeltaTime;
            }
            else
            {
                return 0;
            }
        }
    }

    void Update()
    {
        gameDeltaTime = Time.realtimeSinceStartup - lastTimeSinceStartup;
        lastTimeSinceStartup = Time.realtimeSinceStartup;
    }
}
