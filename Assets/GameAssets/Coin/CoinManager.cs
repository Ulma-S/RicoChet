using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Laser;
using Noon.AudioManagement;

public class CoinManager : MonoBehaviour, IOnPointObject {

    public event Action DestroyEvent;

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
        return ObjectType.Coin;
    }

    public void SetDirection(Direction dir) {
       
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

        if (laser != null) {
            if (DestroyEvent != null) {
                DestroyEvent();
            }
            Destroy(this.gameObject);
            AudioManager.Instance.ShotSE("coin");
        }
    }


}
