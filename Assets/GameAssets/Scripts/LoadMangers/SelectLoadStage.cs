using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noon.LoadManagement;

public class SelectLoadStage : MonoBehaviour
{
    [SerializeField] private int m_stageNum;


    public void LoadSatge() {
        StageDataProvider.SetDataName(m_stageNum);
        if (m_stageNum != 1)
        {
            LoadManager.Instance.ChangeScene("Stage00", () => { });
        }
        else { 
            LoadManager.Instance.ChangeScene("Tutorial", () => { });
        }

    }

    public void NextStage() {
        int num = StageDataProvider.m_currentNum;
        StageDataProvider.SetDataName(num+1);
        DataManager.Instance.SaveCurrentFIle();
        if (num == 20) {
            LoadManager.Instance.ChangeScene("EndCredit", ()=> { });
        } else {
            LoadManager.Instance.ChangeScene("Stage00", () => { });
        }
    }
}
