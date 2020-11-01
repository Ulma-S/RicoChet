using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Noon.TransitionAnimator {
    public abstract class ATransitionAnimation : MonoBehaviour, ITransitionAnimation {
        public abstract bool StartAnimation(float duration, bool isInverse, System.Action onComplete);
        public abstract bool SetProgress(float progress);
    }
}
