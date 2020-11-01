using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Noon.AudioManagement;

public class ClearImageController : MonoBehaviour
{
    [SerializeField] private Image m_fillBar;
    [SerializeField] private float m_duration = 1.0f;
    private int m_condition = 0;

    public event Action FinishAnimation;

    public void SetClearCondition(int state) {

        m_condition = state;
        AudioManager.Instance.ShotSE("result");
        StartCoroutine(FillAnimation());

    }


    private IEnumerator FillAnimation() {

        float targetFillamount = m_condition / 3.0f ;
        float rate = 0;


        while (rate <= 1) {

            m_fillBar.fillAmount = Mathf.Lerp(0, targetFillamount, rate);
            rate += Time.deltaTime / m_duration;
            yield return new WaitForEndOfFrame();
            
        }

        m_fillBar.fillAmount = Mathf.Lerp(0, targetFillamount, rate);

        if (FinishAnimation != null) {
            FinishAnimation();
        }
    }
}
