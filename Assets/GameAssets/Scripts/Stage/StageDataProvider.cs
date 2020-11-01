using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataProvider : MonoBehaviour
{

    static public int m_currentNum = 1;
    static string data = "stage01";
        
    static public void SetDataName(int num) {
        m_currentNum = num;
        data = "stage";
        data += string.Format("{0:00}", num);
    }

    [SerializeField] private int m_sceneNum;
    [SerializeField] private bool DEBUG;
    private StageData m_data;

    private void Awake() {
        if (DEBUG) {
            SetDataName(m_sceneNum);
        }

        SetData();
    }


    private void SetData() {
        m_data = (StageData)Resources.Load("StageData/" + data);
    }

    public StageData GetData() {
        return m_data;
    }
}
