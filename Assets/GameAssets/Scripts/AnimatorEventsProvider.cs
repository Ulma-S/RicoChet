using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimatorEventsProvider : StateMachineBehaviour
{

    public event Action<AnimatorStateInfo> StateEnterEvent;
    public event Action<AnimatorStateInfo> StateExitEvent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (StateEnterEvent != null) {
            StateEnterEvent(animatorStateInfo);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        if (StateExitEvent != null) {
            StateExitEvent(animatorStateInfo);
        }
    }




}
