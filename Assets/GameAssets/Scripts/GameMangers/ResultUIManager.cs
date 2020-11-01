using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField] private ClearImageController m_clearUI;
    [SerializeField] private GameObject m_gameOverUI;

    public void ShowClearUI(int condition) {
        m_clearUI.gameObject.SetActive(true);

        m_clearUI.SetClearCondition(condition);
    }

    public void ShowGameOverUI() {
        m_gameOverUI.SetActive(true);
    }

}
