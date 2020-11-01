using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pivot {
    Center,
    Top,
    Right,
    Bottom,
    Left

}

public class PointsManager{

    private PointData[] m_centerPoints = new PointData[170];
    private PointData[] m_vertPoints = new PointData[161];
    private PointData[] m_HoriPoints = new PointData[160];

    private Vector2 m_cellScale = new Vector2(3.0f,3.0f);
    private Vector2 m_firstCellPos = new Vector2(-17,31.5f);

    private Vector3[] m_pivotPoses = new Vector3[4];

    public PointsManager(Vector2 cellPos,Vector2 cellScale) {
        m_cellScale = cellScale;
        m_firstCellPos = cellPos;

        for (int i = 0; i < m_pivotPoses.Length;i++) {
            int sign = 1;
            if (i >= 2) sign *= -1;
            m_pivotPoses[i] = new Vector3(m_cellScale.x/2 * (i%2) *sign,m_cellScale.y/2 * (( i + 1 ) % 2 ) * sign,0 );
        }

        for (int i = 0; i < m_centerPoints.Length; i++) {
            Vector3 pos = new Vector3(m_firstCellPos.x + m_cellScale.x * (i%10) ,m_firstCellPos.y - m_cellScale.y * (i/10 ), 0);
            m_centerPoints[i] = new PointData(pos);
        }

        for (int i = 0; i < m_vertPoints.Length; i++) {
            Vector3 pos = new Vector3(m_firstCellPos.x + (m_cellScale.x /2.0f) + m_cellScale.x * (i%9) , m_firstCellPos.y - ( m_cellScale.y) * ( i/9), 0);
            m_vertPoints[i] = new PointData(pos);
        }

        for (int i = 0; i < m_HoriPoints.Length; i++) {
            Vector3 pos = new Vector3(m_firstCellPos.x + (m_cellScale.x) * (i % 10), m_firstCellPos.y - (m_cellScale.y / 2.0f) * (1 + (i / 10)*2), 0);
            m_HoriPoints[i] = new PointData(pos);
        }
    }

    public void NextState(IOnPointObject obj) {
        if (obj.GetObjType() != ObjectType.Reflector) return;

        Vector2Int index = obj.GetCurrentIndex(); 
        int state = obj.GetCurrentState();
        int pivotNum = state;

        int setState = state;

        for (int i = 0; i < 6; i++) {
            setState++;
            setState %= 6;

            int pointIndex = pointIndex = index.x + index.y * 10;

            if (setState == 0) {
                GetPointData(pointIndex, (Pivot)Enum.ToObject(typeof(Pivot), state - 1)).SetObjectType(ObjectType.None);
                obj.SetState(-1);
                return;
            }

            pivotNum = setState;
            if (setState != 0) {
                pivotNum--;
            }


            Pivot pivot = (Pivot)Enum.ToObject(typeof(Pivot), pivotNum);
            PointData data = GetPointData(pointIndex, pivot);

            if (data == null) {
                continue;
            }

            if (data.CanSetObject()) {
                GetPointData(pointIndex, (Pivot)Enum.ToObject(typeof(Pivot), state != 0 ? state - 1 : state)).SetObjectType(ObjectType.None);
                obj.SetState(setState);
                obj.SetPosition(data.GetPosition());
                obj.SetPointIndex(index);
                data.SetObjectType(obj.GetObjType());
                return;
            } else if (setState == 1) {
                if (state == 0) {
                    obj.SetState(setState);
                    obj.SetPosition(data.GetPosition());
                    obj.SetPointIndex(index);
                    data.SetObjectType(obj.GetObjType());
                    return;
                }
            }
        }


    }

    public void SetObjPointData(IOnPointObject obj) {

        int pointIndex = GetCenterPointDataIndex(obj.GetCurrentPosition());

        SetObjPointData(obj,new Vector2Int(pointIndex%10,pointIndex/10),obj.GetCurrentState());

    }

    public void SetObjPointData(IOnPointObject obj,Vector2Int index,int state) {
       
        int pointIndex = index.x + index.y*10;

       
        int pivotNum;


        Pivot pivot ;

        PointData data = null ;

        switch (obj.GetObjType()) {
            case ObjectType.Reflector:
                
                int setState = state;

                for (int i = 0; i < 6; i++) {
                    
                    setState %= 6;

                    pivotNum = setState;
                    if (setState != 0) {
                        pivotNum--;
                    }

                    pivot = (Pivot)Enum.ToObject(typeof(Pivot), pivotNum);

                    data = GetPointData(pointIndex, pivot);

                    if (data == null) {
                        setState++;
                        continue;
                    }

                    if (data.CanSetObject()) {
                        obj.SetState(setState);
                        obj.SetPosition(data.GetPosition());
                        obj.SetPointIndex(index);
                        data.SetObjectType(obj.GetObjType());
                        return;
                    }
                    setState++;
                }
                obj.SetState(-1);

                break;

            case ObjectType.Goal:
            case ObjectType.Start:
            case ObjectType.Wall:
            case ObjectType.Coin:

                for (int i = 0; i < Enum.GetNames(typeof(Pivot)).Length; i++) {
                    data = GetPointData(pointIndex, (Pivot)Enum.ToObject(typeof(Pivot), i));

                    if (data == null) continue;

                    if (!data.CanSetObject()) {
                        if (i == 0 || data.GetCurrentObjType() == ObjectType.Reflector) {
                            return;
                        } 
                    }
                }

                for (int i = 0; i < Enum.GetNames(typeof(Pivot)).Length; i++) {
                    data = GetPointData(pointIndex, (Pivot)Enum.ToObject(typeof(Pivot), i));

                    if (data == null) continue;

                    data.SetObjectType(obj.GetObjType());
                }

                Vector3 offset = new Vector3(0,0,0);

                if (obj.GetObjType() == ObjectType.Goal ) {
                    offset = m_pivotPoses[state % 4];
                }

                if (obj.GetObjType() == ObjectType.Start) {
                    offset = m_pivotPoses[(state+2) % 4];
                }

                data = GetPointData(pointIndex, Pivot.Center);
                obj.SetState(state);
                obj.SetPointIndex(index);
                obj.SetPosition(data.GetPosition() + offset);
                data.SetObjectType(obj.GetObjType());


                break;
            
        }


    }


    public void RemoveObj(IOnPointObject obj) {
    }

    private int GetCenterPointDataIndex(Vector3 pos) {

        Vector2 halfCellScale = m_cellScale / 2.0f;

        for (int i = 0; i < m_centerPoints.Length; i++) {
            if (!(pos.x >= m_firstCellPos.x + m_cellScale.x * ( i % 10) - halfCellScale.x && pos.x < m_firstCellPos.x + m_cellScale.x * ( i % 10) + halfCellScale.x)) continue;
      
            if (!(pos.y >= m_firstCellPos.y - m_cellScale.y * ( i / 10) - halfCellScale.y && pos.y < m_firstCellPos.y - m_cellScale.y * ( i / 10) + halfCellScale.y)) continue;
            
            return i;

        }

      
        return -1;
    }

    private PointData GetPointData(int centerIndex, Pivot pivot) {

        if (centerIndex >= m_centerPoints.Length || centerIndex < 0) {
            return null;
        }

        PointData data = null;

        switch (pivot) {
            case Pivot.Center:
                data = m_centerPoints[centerIndex];
                break;
            case Pivot.Top:
                if (centerIndex / 10 == 0) break;
                data = m_HoriPoints[centerIndex - 10];
                break;
            case Pivot.Right:
                if (centerIndex % 10 == 9) break;
                data = m_vertPoints[centerIndex - (centerIndex / 10) ];
                break;
            case Pivot.Bottom:
                if (centerIndex / 10 == 16) break;
                data = m_HoriPoints[centerIndex];
                break;
            case Pivot.Left:
                if (centerIndex % 10 == 0) break;
                data = m_vertPoints[centerIndex - (centerIndex / 10) - 1];
                break;
        }

        return data;
    }
}
