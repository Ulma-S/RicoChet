using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reflector {
    public class InfinityReflector : AReflector {

        public override void Init(Vector3 pos, int state, int durable) {
            transform.position = pos;
            m_state = state;
            

            SetState(m_state);
            SetReflecterAngle(m_currentState);
        }

        public override void SetReflecterAngle(ReflectorInc angle) {
            m_currentState = angle;

            Vector3 nextEuler = transform.eulerAngles;

            switch (angle) {

                case ReflectorInc.Hori:
                    nextEuler = new Vector3(0, 0, 90);
                    break;

                case ReflectorInc.Vert:
                    nextEuler = new Vector3(0, 0, 0);
                    break;

                case ReflectorInc.Left:
                    nextEuler = new Vector3(0, 0, 45);
                    break;

                case ReflectorInc.Right:
                    nextEuler = new Vector3(0, 0, -45);
                    break;
            }

            transform.eulerAngles = nextEuler;


        }

    }
}