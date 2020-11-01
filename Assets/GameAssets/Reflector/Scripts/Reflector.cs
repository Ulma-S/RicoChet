using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Laser;

namespace Reflector {

    public class Reflector : AReflector,IEndDragHandler{
        [SerializeField] private int m_durable;
        [SerializeField] private Sprite[] m_sprites;

        public override void AddReflectDamage(int damage) {
            m_durable -= damage;

            if (m_durable <= 0) {
                DestorySelf();
            } else {
                ChangeSprite();
            }
        }

        public override void Init(Vector3 pos, int state, int durable) {
            transform.position = pos;
            m_state = state;
            m_durable = durable;

            SetState(m_state);
            ChangeSprite();
            SetReflecterAngle(m_currentState);
        }
 
        private void ChangeSprite() {
            int index = m_durable - 1;
            if (index >= m_sprites.Length) index = m_sprites.Length - 1;
            gameObject.GetComponent<SpriteRenderer>().sprite = m_sprites[m_durable-1];
        }

        public override int GetDurable() {
            return m_durable;
        }

    }

}