using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Noon.General {
    public class InitManager {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

        public static void Init() {

            string[] sceneNames = {

            "LoadCanvasScene",
            "AudioManagers"

        };

            foreach (string name in sceneNames) {

                if (!SceneManager.GetSceneByName(name).IsValid()) {
                    SceneManager.LoadScene(name, LoadSceneMode.Additive);
                }
            }

            
        }
    }
}