using UnityEngine;
using Noon.AudioManagement;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private string m_BGMName;

    void Start()
    {
        AudioManager.Instance.SwapBGM(m_BGMName);        
    }

}
