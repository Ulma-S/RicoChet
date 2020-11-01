using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noon.LoadManagement;


public class LoadButton : MonoBehaviour
{

    [SerializeField] private string m_loadSceneName;


    public void Load() {
        LoadManager.Instance.ChangeScene(m_loadSceneName,()=> { });
    }

}
