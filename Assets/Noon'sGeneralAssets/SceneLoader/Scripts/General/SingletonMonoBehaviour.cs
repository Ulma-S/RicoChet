
using UnityEngine;

namespace Noon.General {
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T> {

        private static T m_instance;
        protected static string m_objName;


        public static T Instance {
            get {

                if (m_instance == null) {

                    m_instance = (T)FindObjectOfType(typeof(T));

                    if (m_instance == null) {
                        GameObject create = new GameObject(m_objName);

                        m_instance = create.AddComponent<T>();

                        DontDestroyOnLoad(create);
                    }
                }

                return m_instance;
            }
        }

        protected void SetObjName(string name) {

            m_objName = name;

        }


    }

}