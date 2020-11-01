using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Noon.TransitionAnimator {

    public interface ITransitionAnimation {
        bool StartAnimation(float duration, bool isInverse, System.Action onComplete);
        bool SetProgress(float progress);
    }

}