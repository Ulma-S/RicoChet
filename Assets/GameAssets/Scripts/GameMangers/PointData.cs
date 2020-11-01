using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointData
{
    private Vector3 m_pointPos;
    private ObjectType m_hasObjectType;

    public PointData(Vector3 pos){
        m_pointPos = pos;
      
        m_hasObjectType = ObjectType.None;
    }

    public Vector3 GetPosition() {
        return m_pointPos;
    }

    public void SetObjectType(ObjectType type) {
        m_hasObjectType = type;

    }

    public ObjectType GetCurrentObjType() {
        return m_hasObjectType;
    }

    public bool CanSetObject() {
        if (m_hasObjectType == ObjectType.None) {
            return true;
        } else {
            return false;
        }
    }
    
    

}
