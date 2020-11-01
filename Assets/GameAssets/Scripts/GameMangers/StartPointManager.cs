using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Laser;
using System;
using Noon.AudioManagement;

public class StartPointManager : MonoBehaviour, IOnPointObject {

    private ILaser m_laser;

    private Direction m_startDir;
    private bool m_first = true;
    private Vector2Int m_index = new Vector2Int();

    public event Action LaserDestoryEvent;

    public void SetLaser(ILaser laser) {
        if (m_laser == null) m_laser = laser;
    }

    public void Init(Vector3 pos, int state, int durable) {
        transform.position = pos;
    }

    public Vector2Int GetCurrentIndex() {
        throw new System.NotImplementedException();
    }

    public Vector3 GetCurrentPosition() {
        throw new System.NotImplementedException();
    }

    public int GetCurrentState() {
        throw new System.NotImplementedException();
    }

    public ObjectType GetObjType() {
        return ObjectType.Start;
    }

   

    public void SetDirection(Direction dir) {
     
    }

    public void SetPointIndex(Vector2Int index) {
        m_index = index;
    }

    public void SetPosition(Vector3 pos) {
        transform.position = pos;
    }

    public void SetState(int state) {
        if (state == -1) {
            Debug.Log("Error State1");
            return;
        }

        transform.eulerAngles = new Vector3(0, 0, -90 * state);

        m_startDir = (Direction)Enum.ToObject(typeof(Direction), state);
    }

    public void FireLaser() {
        Vector3 offset = Vector3.zero ;

        switch (m_startDir) {
            case Direction.UP:
                offset = new Vector3(0,+5,0);
                break;
            case Direction.Right:
                offset = new Vector3(5, 0, 0);
                break;
            case Direction.Down:
                offset = new Vector3(0, -5, 0);
                break;
            case Direction.Left:
                offset = new Vector3(-5,0,0);
                break;
        }

        ILaser laser = Instantiate((Laser.Laser)m_laser,transform.position + offset,Quaternion.identity);
        laser.DestroyEvent += LaserDestoryEvent;
        laser.SetCurrentLaserDir(m_startDir);

    }

    private void OnTriggerEnter2D(Collider2D col) {
        ILaser laser = col.gameObject.GetComponent<ILaser>();

        if (laser == null) return;

        if (!m_first) {

            float degree = 0;

            switch (laser.GetCurrentDir()) {
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

            laser.OnCollisionWall(ObjectType.Start);
        } else {
            m_first = false;
            Debug.Log("FIRST");
        }
    }

}
