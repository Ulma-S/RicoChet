using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Reflector;

public class PlayableReflecterManager : MonoBehaviour
{
    [SerializeField] private Reflector.Reflector m_reflecterPrefab;
    [SerializeField] private PlayerReflectorProvider[] m_reflectorProvider; 
    private IReflector[][] m_reflectors;
    private int m_countAllReflector;
    private int m_countBrokenReflector;

    public void AddToReflectorProvider(IReflector reflector) {

        m_reflectorProvider[reflector.GetDurable() - 1].SetReflectorData(reflector) ;

    }

    public void CreateReflector(int[] reflectorCount) {
        m_countAllReflector = 0;
        m_reflectors = new IReflector[reflectorCount.Length][];
        for (int i = 0; i < reflectorCount.Length; i++) {

            m_reflectors[i] = new IReflector[reflectorCount[i]];

            for (int j = 0; j < reflectorCount[i]; j++) {

                m_reflectors[i][j] = Instantiate(m_reflecterPrefab,new Vector3(-500,500,0),Quaternion.identity);
                m_reflectors[i][j].Init(new Vector3(-500, 500, 0),0,i+1);
                m_reflectors[i][j].DestroyEvent += () => m_countBrokenReflector++;
                AddToReflectorProvider(m_reflectors[i][j]);
                m_countAllReflector++;
            }


        }

    }

    public void SetReflectorEvents(Action<IOnPointObject> drag, Action<IOnPointObject> drop, Action<IOnPointObject> click) {
        foreach (IReflector[] list in m_reflectors) {
            foreach (IReflector item in list) {

                item.DragEvent += drag;
                item.DropEvent += drop;
                item.ClickEvent += click;
                item.ReleaseReflectorEvent += AddToReflectorProvider;
            }
        }
    }

    public int CountRestReflector() {
        int count = 0;

        foreach (IReflector[] list in m_reflectors) {
            foreach (IReflector item in list) {

                if (item != null) count++;


            }
        }
        
        return count - m_countBrokenReflector;
    }


}
