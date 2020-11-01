using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class performanceObs : MonoBehaviour
{

    [SerializeField] private GameObject[] masks;

    // Start is called before the first frame update
    void Start()
    {

        int data = DataManager.Instance.CurrentData.stageDatas[StageDataProvider.m_currentNum - 1];

        if ((data & (int)StageDataMask.CLEAR) != 0) masks[0].SetActive(true);
        if ((data & (int)StageDataMask.GET_COIN) != 0) masks[1].SetActive(true);
        if ((data & (int)StageDataMask.ALL_ITE) != 0) masks[2].SetActive(true);

    }

}
