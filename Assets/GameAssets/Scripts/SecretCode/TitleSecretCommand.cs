using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSecretCommand : MonoBehaviour
{

    [SerializeField] private float m_capableTime = 10;
    [SerializeField] private ToggleSwitch m_toggleSwitch;
    [SerializeField] private bool DEBUG;

    private bool m_trigger = false;
    private float m_passedTime = 0;
    private Vector3 m_currentAcceleration = Vector3.zero;
    private Vector3 m_preAcceleration = Vector3.zero;
    private int m_shakeCount = 0;


    // Update is called once per frame
    void Update()
    {
        if (!DEBUG) {
            m_currentAcceleration = Input.acceleration;
        } else {
            m_shakeCount = 5;
        }

        if (m_trigger) {
            m_passedTime += Time.deltaTime;
            if (m_passedTime <= m_capableTime) {

                if (Vector3.Dot(m_currentAcceleration, m_preAcceleration) < 0) {
                    m_shakeCount++;
                }
               
                if (m_shakeCount >= 5) {
                    int stageC = DataManager.Instance.CurrentData.stageDatas.Length;
                    Debug.Log(DataManager.Instance.CurrentData);
                    for (int i = 0; i < stageC; i++) {
                        DataManager.Instance.CurrentData.stageDatas[i] |= 0xFFFF;   
                    }
                    Debug.Log(DataManager.Instance.CurrentData);
                    DataManager.Instance.SaveCurrentFIle();
                    DataManager.Instance.ReloadFile();
                    m_toggleSwitch.SwapActivity();
                    m_trigger = false;
                }


            } else {
                m_trigger = false;
            }

        }
        m_preAcceleration = m_currentAcceleration;
    }

    public void CommandTrigger() {
        m_trigger = true;
        m_passedTime = 0;
        m_shakeCount = 0; 
    }
}
