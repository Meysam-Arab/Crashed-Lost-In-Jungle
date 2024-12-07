using System;
using System.Collections.Generic;
using UnityEngine;

public struct TouchState
{
    public static int TOUCH_STATE_NON = -1;
    public static int TOUCH_STATE_TAP = 0;
    public static int TOUCH_STATE_HOLD = 1;


    public static float HOLD_DURATION = 1.0f;

    public int state;
    public float holdDuration;

}

