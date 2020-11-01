using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noon.General;

public class EffectManager : SingletonMonoBehaviour<EffectManager>
{
    [SerializeField] GameObject ReflectEffectPrefab;

    int m_ReflecteEffectCount = 5;
    Queue<IEffectController> m_reflecteQueue = new Queue<IEffectController>();

    private void Start() {

        for (int i = 0; i < m_ReflecteEffectCount; i++) {
            GameObject effect = Instantiate(ReflectEffectPrefab, new Vector3(-100,-100,-55),Quaternion.identity);
            effect.transform.eulerAngles = new Vector3(-90,0,0);
            IEffectController effectController = effect.GetComponent<IEffectController>();

            effectController.OnEnterEmptyStateEvent += (controller) => {
                m_reflecteQueue.Enqueue(controller);
            };

            m_reflecteQueue.Enqueue(effectController);

        }

    }

    /// <summary>
    /// <para>type 0 => reflecteEffect</para>
    /// <para>type 1 => dontReflecteEffect</para>
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="type"></param>
    public void CallEffect(Vector3 pos,float zDegree,int type) {

        switch (type)
        {
            case 0:
                m_reflecteQueue.Dequeue().EffectStart(pos, zDegree);
                break;
            default:
                break;
        }
    }

}
