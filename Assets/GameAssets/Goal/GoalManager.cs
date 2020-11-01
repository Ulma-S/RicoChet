using System;
using UnityEngine;
using Laser;
using Noon.AudioManagement;

public class GoalManager : MonoBehaviour , IOnPointObject {

    [SerializeField] private Direction m_receiveDir;
    [SerializeField] private GoalAnimation m_gAnim;

    public event Action Finish;

    public void Init(Vector3 pos, int state, int durable) {
        transform.position = pos;
    }


    public Vector2Int GetCurrentIndex() {
        throw new NotImplementedException();
    }

    public Vector3 GetCurrentPosition() {
        throw new NotImplementedException();
    }

    public int GetCurrentState() {
        throw new NotImplementedException();
    }

    public ObjectType GetObjType() {
        return ObjectType.Goal;
    }

    public void SetPointIndex(Vector2Int index) {

    }

    public void SetPosition(Vector3 pos) {
        transform.position = pos;
    }

    public void SetState(int state) {
        if (state == -1) {
            Debug.Log("Error State0");
            return;
        }
        
        transform.eulerAngles = new Vector3(0,0,-90*state);

        m_receiveDir = (Direction)Enum.ToObject(typeof(Direction),state); 

    }

    public void SetDirection(Direction dir) {
        m_receiveDir = dir;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Laser") {
            if (col.gameObject.GetComponent<ILaser>().GetCurrentDir() == m_receiveDir) {
                m_gAnim.StartAnimation(Finish);
                AudioManager.Instance.ShotSE("clear",0.5f);
                Destroy(col.gameObject);


            } else {

                float degree = 0;
                switch (col.gameObject.GetComponent<ILaser>().GetCurrentDir()) {
                    case Direction.UP:
                        degree = 180;
                        break;
                    case Direction.Right:
                        degree = 90;
                        break;
                    case Direction.Down:
                        degree = 0;
                        break;
                    case Direction.Left:
                        degree = -90;
                        break;
                }

                EffectManager.Instance.CallEffect(col.transform.position, degree, 1);
                col.gameObject.GetComponent<ILaser>().OnCollisionWall(ObjectType.Goal);
            }
        }
    }


}
