using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{

    bool OnTouch { get; }
    bool IsDiffState { get; }
    Vector3 CursorPos { get; }


}
