using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Noon.AudioManagement {
    public class AudioVolumeSwapper : MonoBehaviour {
        private static bool m_currentState = true;

        [SerializeField] private GameObject kine;
        private void Start() {
            kine.SetActive(!m_currentState);
        }

        public void SwapVolume() {
            AudioManager.Instance.MusicVolumeSwap(!m_currentState);
            m_currentState = !m_currentState;
        }

    }
}