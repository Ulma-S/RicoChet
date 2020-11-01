using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noon.LoadManagement;

public class ClearButtonController : MonoBehaviour
{
    public void ToStageSelect() {
        LoadManager.Instance.ChangeScene("stageSelect",()=> { });
    }

    public void ReStartStage() {
        LoadManager.Instance.ReloadCurrentScene(()=> { });
    }

    public void ToNextStage() {

        LoadManager.Instance.ReloadCurrentScene(()=> { });
    }
}
