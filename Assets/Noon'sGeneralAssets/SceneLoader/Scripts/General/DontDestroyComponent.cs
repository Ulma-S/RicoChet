using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Noon.General {
    public class DontDestroyComponent : MonoBehaviour {

        private void Awake() {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}