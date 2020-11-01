using UnityEngine;
using Noon.AudioManagement;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] private GameObject SwitchObject;
    [SerializeField] private bool m_haveSE = false;
    [SerializeField] private string[] m_SEName = new string[2];

    public void SwapActivity() {

        SwitchObject.SetActive(!SwitchObject.activeSelf);

        if (m_haveSE) {
            string name;
            if (SwitchObject.activeSelf) {
                name = m_SEName[0];
            } else{
                name = m_SEName[1];
            }
            AudioManager.Instance.ShotSE(name);
        }
    }
}
