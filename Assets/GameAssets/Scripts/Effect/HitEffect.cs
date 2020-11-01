using System;
using UnityEngine;
using Noon.AudioManagement;

public class HitEffect : MonoBehaviour, IEffectController {
    [SerializeField] Animator m_animator;
    [SerializeField] private string m_animationSE;

    private bool m_isReady;
    private int m_emptyStateHash;

    public event Action<IEffectController> OnEnterEmptyStateEvent;
    float halfHeight;
    private void Start() {

        m_animator.GetBehaviour<AnimatorEventsProvider>().StateEnterEvent += SetIsReady;
       // halfHeight = gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    public void EffectStart(Vector3 pos, float zDegree) {
        m_animator.SetTrigger("Fire");
        AudioManager.Instance.ShotSE(m_animationSE);
        
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        //transform.eulerAngles = new Vector3(0,0,zDegree);
       

        
    }

    private void SetIsReady(AnimatorStateInfo animatorStateInfo) {
        if (animatorStateInfo.IsName("Base Layer.Empty")) {
            m_isReady = true;
            transform.position = new Vector3(400,400,0);
            if (OnEnterEmptyStateEvent != null) {
                OnEnterEmptyStateEvent(this);
            }
        } else {
           
            m_isReady = false;
        }

    }
}
