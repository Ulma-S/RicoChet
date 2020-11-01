using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noon.General;

namespace Noon.TransitionAnimator {
    /// <summary>
    /// Animationをここを通してしか実行できないようにしておく、
    /// 複数のアニメーションが実行されるなどは非常に困る
    /// 
    /// ToDo: Animationの切り替え、どっかで生成からの譲渡を指したいね
    /// </summary>
    public class TransitionAnimationManager : SingletonMonoBehaviour<TransitionAnimationManager> {

        [SerializeField] ATransitionAnimation m_animation;
        [SerializeField] bool m_runAnyAnimation = false;
        [SerializeField] float m_duration;

        private void Awake() {
            SetObjName("TransitionAnimationManager");

            foreach (Transform obj in gameObject.GetComponentsInChildren<Transform>()) {
                if (obj.name == this.name) {
                    continue;
                }
                obj.gameObject.SetActive(false);
            }
        }

        public bool StartAnimation( bool isInverse, System.Action onComplete) {
            bool state = StartAnimation(m_duration,isInverse,onComplete);

            return state;
        }

        public bool StartAnimation(float duration, bool isInverse, System.Action onComplete) {
            if (m_runAnyAnimation) return false;

            bool state = m_animation.StartAnimation(duration,isInverse,
                () => {
                    m_runAnyAnimation = false;
                    onComplete();
                    }
                );

            if (state) {
                m_runAnyAnimation = true;
            }

            return state;
        }

        public bool SetAnimationProgress(float progress) {
            if (m_runAnyAnimation) return false;

            bool state = m_animation.SetProgress(progress);

            return state;

        }

        public void SetCurrentAnimationActive(bool value) {
            m_animation.gameObject.SetActive(value);
        }

    }
}