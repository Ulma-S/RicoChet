using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Reflector;
using Noon.AudioManagement;

//d&dはこのクラスで管理？

public class PlayerReflectorProvider : MonoBehaviour,IPointerDownHandler,IDragHandler,IEndDragHandler
{
    [SerializeField] private Image m_counterImage;
    [SerializeField] private ScrollRect m_scrollRect;
    [SerializeField] private Sprite[] m_counterSprites;

    private int m_currentIndex;
    private Queue<IReflector> m_reflectorQueue = new Queue<IReflector>();

    public void OnDrag(PointerEventData eventData) {
        if (m_reflectorQueue.Count < 1) return;
        m_reflectorQueue.Peek().OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData) {

        if (m_reflectorQueue.Count < 1) return;
        m_reflectorQueue.Dequeue().OnEndDrag(eventData);
        ChangeQueueCount();
        AudioManager.Instance.ShotSE("item_PutDown_Rotation");

        m_scrollRect.enabled = true;
    }

    public void OnPointerDown(PointerEventData eventData) {
        if (m_reflectorQueue.Count < 1) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
        mousePos.Scale(new Vector3(1,1,0));
        m_reflectorQueue.Peek().SetPosition(mousePos);
        AudioManager.Instance.ShotSE("item_PickUp");
        m_scrollRect.enabled = false;
        
    }

    public void SetReflectorData(IReflector[] reflectors) {
        foreach (IReflector item in reflectors) {
            m_reflectorQueue.Enqueue(item);
        }
        ChangeQueueCount();
    }

    public void SetReflectorData(IReflector reflector) {

        m_reflectorQueue.Enqueue(reflector);
        ChangeQueueCount();
    }

    private void ChangeQueueCount() {
        m_counterImage.sprite = m_counterSprites[m_reflectorQueue.Count];
    }

}
