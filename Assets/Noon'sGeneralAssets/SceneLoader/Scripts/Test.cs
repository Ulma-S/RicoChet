using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noon.LoadManagement;

public class Test : MonoBehaviour {

    public void test() {

        LoadManager.Instance.ChangeScene("02",()=> { });

    }
}
