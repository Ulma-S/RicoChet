using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOnPointObject{

    void Init(Vector3 pos, int state,int durable);

    void SetDirection(Direction dir);
    void SetPosition(Vector3 pos);
    void SetPointIndex(Vector2Int index);
    Vector2Int GetCurrentIndex();
    Vector3 GetCurrentPosition();

    ObjectType GetObjType();
    int GetCurrentState();
    void SetState(int state);

}
