using System.Collections;
using Noon.General;
using Noon.TransitionAnimator;
using UnityEngine;
using UnityEngine.SceneManagement;
using Noon.AudioManagement;

namespace Noon.LoadManagement {

    public class LoadManager : SingletonMonoBehaviour<LoadManager> {

        public event System.Action LoadFinishEvent;

        [SerializeField] private float m_waitSeconds = 0.5f;

        private AsyncOperation m_currentLoadSceneOp;
        private float m_progress;

        public void ReloadCurrentScene(System.Action onComplete) {
            Scene currentScene = SceneManager.GetActiveScene();
            StartCoroutine(
                SimpleLoadScene(currentScene.name, LoadSceneMode.Single, onComplete)
                );
        }

        public void ChangeScene(string scene, System.Action onComplete) {
            StartCoroutine(
                SimpleLoadScene(scene, LoadSceneMode.Single, onComplete)
                );
        }

        public void AddScene(string scene, System.Action onComplete) {
            StartCoroutine(
                SimpleLoadScene(scene, LoadSceneMode.Additive, onComplete)
                );
        }


        private IEnumerator SimpleLoadScene(string scene, LoadSceneMode mode, System.Action onComplete) {
            bool finishAnimation = false;
            Scene currentScene = SceneManager.GetActiveScene();

            //画面遷移アニメーション
            TransitionAnimationManager.Instance.StartAnimation(false, () => { finishAnimation = true; });

            //アニメーション、ロード待機
            yield return LoadScene(scene, mode, () => { });
            yield return new WaitUntil(() => finishAnimation);

            //シーンのアクティブ
            finishAnimation = false;
            m_currentLoadSceneOp.allowSceneActivation = true;


            if (mode == LoadSceneMode.Single) {
                yield return UnloadScene(currentScene.name);
            }
            yield return new WaitForSeconds(m_waitSeconds);

            TransitionAnimationManager.Instance.StartAnimation(true, () => { finishAnimation = true; });

            yield return new WaitUntil(() => finishAnimation);

            onComplete();
            if (LoadFinishEvent != null) {
                LoadFinishEvent();
            }
        }

        private IEnumerator LoadScene(string scene, LoadSceneMode mode, System.Action onComplete) {

            m_currentLoadSceneOp = SceneManager.LoadSceneAsync(scene, mode);

            m_currentLoadSceneOp.allowSceneActivation = false;

            while (m_currentLoadSceneOp.progress < 0.89) {

                m_progress = m_currentLoadSceneOp.progress;
                yield return null;
            }

            onComplete();
        }

        private IEnumerator UnloadScene(string scene) {
            if (SceneManager.GetSceneByName(scene) == null) yield break;

            yield return SceneManager.UnloadSceneAsync(scene);
        }

    }
}