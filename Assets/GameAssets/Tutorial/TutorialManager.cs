using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noon.LoadManagement;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] private GameObject[] m_explainImages = null;

    private int m_currentPage = -1;

    private void Start()
    {
        NextPage();
    }
    public void NextPage() {
        m_currentPage++;
        if (m_currentPage == 5) {
            LoadManager.Instance.ChangeScene("Stage00",()=> { });
            return;
        }

        for (int i = 0; i < m_explainImages.Length; i++) {
            if (i == m_currentPage)
            {
                m_explainImages[i].SetActive(true);
            }
            else {
                m_explainImages[i].SetActive(false);
            }


        }


    }



}
