using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectController 
{
    event Action<IEffectController> OnEnterEmptyStateEvent;
    void EffectStart(Vector3 pos, float zDegree);

}
