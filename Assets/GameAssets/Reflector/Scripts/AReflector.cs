using System;
using UnityEngine;
using Laser;
using UnityEngine.EventSystems;
using Noon.AudioManagement;

namespace Reflector {
    public abstract class AReflector : MonoBehaviour, IReflector, IOnPointObject {
       // [SerializeField] protected int m_durable;

        [SerializeField] protected ReflectorInc m_currentState;

        protected ObjectType m_type = ObjectType.Reflector;
        protected int m_state = -1;
        protected Vector2Int m_pointIndex = new Vector2Int(-99, -99);
        protected bool canMove = true;
        protected bool isOnStage;
        protected bool isDragged = false;

        public event Action DestroyEvent;
        public event Action<IReflector> ReleaseReflectorEvent;
        public event Action<IOnPointObject> DropEvent;
        public event Action<IOnPointObject> DragEvent;
        public event Action<IOnPointObject> ClickEvent;

        public void SetCanMove(bool can) {
            canMove = can;
        }

        public virtual void AddReflectDamage(int damage) {

        }

        public Direction ChangeLaserDir(Direction currentDir) {

            Direction nextDir = currentDir;

            switch (m_currentState) {
                case ReflectorInc.Hori:

                    if (currentDir == Direction.Down) {
                        nextDir = Direction.UP;
                    } else if (currentDir == Direction.UP) {
                        nextDir = Direction.Down;
                    }

                    break;

                case ReflectorInc.Vert:

                    if (currentDir == Direction.Left) {
                        nextDir = Direction.Right;
                    } else if (currentDir == Direction.Right) {
                        nextDir = Direction.Left;
                    }

                    break;

                case ReflectorInc.Right:

                    switch (currentDir) {
                        case Direction.UP:
                            nextDir = Direction.Right;
                            break;
                        case Direction.Down:
                            nextDir = Direction.Left;
                            break;
                        case Direction.Right:
                            nextDir = Direction.UP;
                            break;
                        case Direction.Left:
                            nextDir = Direction.Down;
                            break;
                    }

                    break;

                case ReflectorInc.Left:

                    switch (currentDir) {
                        case Direction.UP:
                            nextDir = Direction.Left;
                            break;
                        case Direction.Down:
                            nextDir = Direction.Right;
                            break;
                        case Direction.Right:
                            nextDir = Direction.Down;
                            break;
                        case Direction.Left:
                            nextDir = Direction.UP;
                            break;
                    }

                    break;
            }

            return nextDir;
        }

        public void DestorySelf() {
            if (DestroyEvent != null) {
                DestroyEvent();
            }
            GameObject.Destroy(this.gameObject);
        }

        public Vector2Int GetCurrentIndex() {
            return m_pointIndex;
        }

        public Vector3 GetCurrentPosition() {
            return transform.position;
        }

        public int GetCurrentState() {
            return m_state;
        }

        public ObjectType GetObjType() {
            return m_type;
        }

        public abstract void Init(Vector3 pos, int state, int durable);

        public void SetDirection(Direction dir) {


            switch (dir) {

                case Direction.UP:
                    SetReflecterAngle(ReflectorInc.Hori);
                    break;

                case Direction.Down:
                    SetReflecterAngle(ReflectorInc.Vert);
                    break;

                case Direction.Left:
                    SetReflecterAngle(ReflectorInc.Left);
                    break;

                case Direction.Right:
                    SetReflecterAngle(ReflectorInc.Right);
                    break;
            }

        }

        public void SetPointIndex(Vector2Int index) {
            if (!canMove) return;
            m_pointIndex = index;
        }

        public void SetPosition(Vector3 pos) {
            if (!canMove) return;

            transform.position = pos;
        }

        public virtual void SetReflecterAngle(ReflectorInc angle) {
            m_currentState = angle;

            Vector3 nextEuler = transform.eulerAngles;

            switch (angle) {

                case ReflectorInc.Hori:
                    nextEuler = new Vector3(0, 0, 45);
                    break;

                case ReflectorInc.Vert:
                    nextEuler = new Vector3(0, 0, -45);
                    break;

                case ReflectorInc.Left:
                    nextEuler = new Vector3(0, 0, 0);
                    break;

                case ReflectorInc.Right:
                    nextEuler = new Vector3(0, 0, 90);
                    break;
            }

            transform.eulerAngles = nextEuler;


        }

        public void SetState(int state) {
            if (!canMove) return;

            m_state = state;
            switch (state) {
                case -1:
                    transform.position = new Vector3(100, 100, 100);
                    if (ReleaseReflectorEvent != null) {
                        ReleaseReflectorEvent(this);

                    }
                    isOnStage = false;
                    SetDirection(Direction.Left);
                    break;
                case 0:
                    SetDirection(Direction.Left);
                    break;
                case 1:
                    SetDirection(Direction.Right);
                    break;
                case 2:
                    SetDirection(Direction.UP);
                    break;
                case 3:
                    SetDirection(Direction.Down);
                    break;
                case 4:
                    SetDirection(Direction.UP);
                    break;
                case 5:
                    SetDirection(Direction.Down);
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D col) {
            ILaser laser = col.gameObject.GetComponent<ILaser>();

            if (laser != null) {
                Direction cuDir = laser.GetCurrentDir();
                Direction nextDir = ChangeLaserDir(cuDir);
                laser.SetCurrentLaserDir(nextDir);
                col.gameObject.transform.position = transform.position;
                AddReflectDamage(1);

                float angle = EffectAngle(cuDir,nextDir);
                EffectManager.Instance.CallEffect(col.transform.position, angle, 0);
            }

            
        }

        public void OnDrag(PointerEventData eventData) {
            isDragged = true;
            if (!isOnStage) {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
                mousePos.Scale(new Vector3(1, 1, 0));
                transform.position = mousePos;
                if (DragEvent != null) {
                    DragEvent(this);
                }
            }

        }

        public void OnEndDrag(PointerEventData eventData) {
            if (!isOnStage) {
                isDragged = false;
                isOnStage = true;
                if (DropEvent != null) {
                    DropEvent(this);
                }
            }



        }

        public void OnPointerUp() {
            if (isDragged) return;

            AudioManager.Instance.ShotSE("item_PutDown_Rotation");
            if (ClickEvent != null) {
                ClickEvent(this);
            }
        }

        public virtual int GetDurable() {
            return 0;
        }


        private float EffectAngle(Direction current,Direction next) {
            
            float angle = 0;
           
            if (current == Direction.UP) {
                if (next == Direction.Right) {
                    angle = -135;
                } else if (next == Direction.Down) {
                    angle = -180;
                } else if (next == Direction.Left) {
                    angle = 135;
                }

            } else if (current == Direction.Right) {
                if (next == Direction.UP) {
                    angle = 45;
                } else if (next == Direction.Down) {
                    angle = 135;
                } else if (next == Direction.Left) {
                    angle = 90;
                }
            } else if (current == Direction.Down) {
                if (next == Direction.UP) {
                    angle = 0;
                } else if (next == Direction.Right) {
                    angle = -45;
                } else if (next == Direction.Left) {
                    angle = 45;
                }
            } else if (current == Direction.Left) {
                if (next == Direction.Right) {
                    angle = -90;
                } else if (next == Direction.Down) {
                    angle = -135;
                } else if (next == Direction.UP) {
                    angle = -45;
                }
            }

            return angle;
        }
    }
}
