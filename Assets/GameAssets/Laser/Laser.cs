using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Noon.LoadManagement;
using Noon.AudioManagement;

namespace Laser {



    public class Laser : MonoBehaviour,ILaser {

        [SerializeField] private Vector2 m_currentTarget;
        [SerializeField] private Direction m_dir;
        [SerializeField] private float m_speed = 2.0f;

        private Vector2 m_currentDir;

        public event Action DestroyEvent;
 
        private void FixedUpdate() {

            transform.Translate(m_currentDir * m_speed * Time.deltaTime, Space.World );

        }

        public void OnCollisionWall(ObjectType type) {
            
            if (type == ObjectType.Start) {

                Destroy(this.gameObject);
                if (DestroyEvent != null) {
                    DestroyEvent();
                }
            } else {

                LoadManager.Instance.ReloadCurrentScene(()=> { });
                Destroy(this.gameObject);
            }
        }

        public Vector2 GetCurrentPosition() {
            return m_currentTarget;
        }

        public Direction GetCurrentDir() {
            return m_dir;
        }

        public void SetTargetPosition(Vector2 targePos) {
            m_currentTarget = targePos;
        }

        public void SetCurrentLaserDir(Direction dir) {
            m_dir = dir;
            ChangeLaserDir();

        }

        private void ChangeLaserDir() {

            Vector3 nextRot  = transform.eulerAngles;

            switch (m_dir) {

                case Direction.UP:
                    m_currentDir = Vector2.up;
                    nextRot = new Vector3(0, 0, 0);
                    break;

                case Direction.Down:
                    m_currentDir = Vector2.down;
                    nextRot = new Vector3(0, 0, -180);
            
                    break;

                case Direction.Right:
                    m_currentDir = Vector2.right;
                    nextRot = new Vector3(0, 0, -90);
                    break;

                case Direction.Left:
                    m_currentDir = Vector2.left;
                    nextRot = new Vector3(0, 0, 90);
                    break;

            }
            AudioManager.Instance.ShotSE("laser_beam");
            transform.eulerAngles = nextRot;
        }

    }

}