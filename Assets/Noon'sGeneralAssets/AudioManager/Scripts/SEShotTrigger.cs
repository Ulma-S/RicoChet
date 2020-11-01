using UnityEngine;
using Noon.AudioManagement;

public class SEShotTrigger : MonoBehaviour
{
    [SerializeField] private string m_SEName;

    public void ShotSE() {
        AudioManager.Instance.ShotSE(m_SEName);
    }

}
