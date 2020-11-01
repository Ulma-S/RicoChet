using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalAnimation : MonoBehaviour
{

    [SerializeField] GameObject m_expandObj;

    public void StartAnimation(System.Action onComplete) {
        StartCoroutine(ExpandAnimation(onComplete));
    }


    private IEnumerator ExpandAnimation(System.Action onComplete) {
        float scale = 1;

        while (scale <= 2.7) {

            scale += Time.deltaTime;
            m_expandObj.transform.localScale = new Vector3(scale,scale,1);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(.5f);

        if (onComplete != null) {
            onComplete();
        }

    }
}
