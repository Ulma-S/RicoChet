using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Laser;

public class Wall : MonoBehaviour, IOnPointObject {

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
        return ObjectType.Wall;
    }

   
    public void SetDirection(Direction dir) {
        throw new System.NotImplementedException();
    }

    public void SetPointIndex(Vector2Int index) {
    }

    public void SetPosition(Vector3 pos) {
        transform.position = pos;
    }

    public void SetState(int state) {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        ILaser laser = col.gameObject.GetComponent<ILaser>();

        if (laser == null) return;

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

        EffectManager.Instance.CallEffect(col.transform.position,degree,1);

        laser.OnCollisionWall(ObjectType.Wall);
    }

}
