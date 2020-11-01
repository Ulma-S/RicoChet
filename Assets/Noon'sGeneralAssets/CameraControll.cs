using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rect safeAreaRect = new Rect();
        safeAreaRect.width = Screen.safeArea.width / Screen.width;
        safeAreaRect.height = Screen.safeArea.height / Screen.height;
        safeAreaRect.x = Screen.safeArea.x / Screen.width;
        safeAreaRect.y = Screen.safeArea.y / Screen.height;
        Camera.main.rect = safeAreaRect;
    }

}
