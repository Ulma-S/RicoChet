using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ReflectorInc {
    Vert,
    Hori,
    Right,// "/"
    Left  // "\"
}

namespace Reflector {

    
    public interface IReflector {
        event Action<IOnPointObject> DropEvent;
        event Action<IOnPointObject> DragEvent;
        event Action<IOnPointObject> ClickEvent;
        event Action DestroyEvent;
        event Action<IReflector> ReleaseReflectorEvent;

        void OnDrag(PointerEventData eventData);
        void OnEndDrag(PointerEventData eventData);

        void SetCanMove(bool can);
        void Init(Vector3 pos, int state, int durable);
        void SetPosition(Vector3 pos);
        int GetDurable();
        Direction ChangeLaserDir(Direction currentDir);
        void AddReflectDamage(int damage);
        void SetReflecterAngle(ReflectorInc angle);
        void DestorySelf();
    
    }
}