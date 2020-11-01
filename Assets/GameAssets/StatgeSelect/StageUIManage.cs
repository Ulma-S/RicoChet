using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUIManage : MonoBehaviour
{

    [SerializeField] private GameObject[] Buttons = new GameObject[20];
    [SerializeField] private Image[] Meters = new Image[20];

    // Start is called before the first frame update
    void Start()
    {
        PlayerData data =  DataManager.Instance.CurrentData;

        for (int i = 0; i < data.stageDatas.Length; i++) {
            
            if ((data.stageDatas[i] & (int)StageDataMask.OPEN) != (int)StageDataMask.OPEN) {
                Buttons[i].SetActive(false);
                Debug.Log("Close");
                continue;
            } else {
                Buttons[i].SetActive(true);
            }

            float meter_p = 0;

            if ((data.stageDatas[i] & (int)StageDataMask.CLEAR) == (int)StageDataMask.CLEAR) {
                meter_p++;
                Debug.Log("CLEAR");
            }
            if ((data.stageDatas[i] & (int)StageDataMask.GET_COIN) == (int)StageDataMask.GET_COIN) {
                meter_p++;
                Debug.Log("COIN");
            }

            if ((data.stageDatas[i] & (int)StageDataMask.ALL_ITE) == (int)StageDataMask.ALL_ITE) {
                meter_p++;
                Debug.Log("ITEM");

            }

            Debug.Log(meter_p);
            Meters[i].fillAmount = meter_p / 3.0f;
        }
    }
    
}
