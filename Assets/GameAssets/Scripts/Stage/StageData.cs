using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "data",
  menuName = "ScriptableObject/StageData",
  order = 0)
    ]

public class StageData :ScriptableObject{

    [SerializeField] public int[] PlayerSetReflectors;

    [SerializeField] public OnStageObjSet GoalData;
    [SerializeField] public OnStageObjSet StartData;
    [SerializeField] public Vector2Int[] WallPoses;
    [SerializeField] public Vector2Int CoinPos;
    [SerializeField] public OnStageObjSet[] ReflectorsData;
    [SerializeField] public OnStageObjSet[] InfinityReflectorsData;


}

[System.Serializable]
public class OnStageObjSet {

    public Vector2Int pos;
    public int state;
    public int durable;

}