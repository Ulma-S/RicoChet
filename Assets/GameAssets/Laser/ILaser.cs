using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Laser {

    

    public interface ILaser {

        event Action DestroyEvent;

        void SetCurrentLaserDir(Direction dir);
        Direction GetCurrentDir();
        void OnCollisionWall(ObjectType type);
        void SetTargetPosition(Vector2 targePos);
        Vector2 GetCurrentPosition();
    }

}