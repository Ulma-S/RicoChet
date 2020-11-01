using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum StageDataMask {
    OPEN = 0x000F,
    CLEAR = 0x00F0,
    GET_COIN = 0x0F00,
    ALL_ITE = 0xF000
}

public class PlayerData{
    public int[] stageDatas = new int[20];

    public PlayerData() {
        stageDatas[0] = (int)StageDataMask.OPEN;
        for (int i = 1; i < 20; i++) {
            stageDatas[i] = 0x0000;
        }
    }
}
