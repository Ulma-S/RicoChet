using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputHandler : MonoBehaviour, IInputHandler {
    private bool m_ontouch = false;
    private bool m_diffTouchState = false;
    private Vector3 m_mousePos;

    private bool m_preTouchState = false;

    public bool OnTouch => m_ontouch;

    public bool IsDiffState => m_diffTouchState;

    public Vector3 CursorPos { get {
            Vector3 ret = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            ret.Scale(new Vector3(1,1,0));
            return ret;
                }
    }


    private void Update() {

        m_ontouch = Input.GetMouseButton(0);

        if (m_ontouch != m_preTouchState) {
            m_diffTouchState = true;
        } else {
            m_diffTouchState = false;
        }

        m_preTouchState = m_ontouch;
    }


}
