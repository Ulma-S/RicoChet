using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noon.LoadManagement;

public class GameOverTouch : MonoBehaviour
{
    public void Reload() {
        LoadManager.Instance.ReloadCurrentScene(()=> { });
        this.enabled = false;
    }
}
