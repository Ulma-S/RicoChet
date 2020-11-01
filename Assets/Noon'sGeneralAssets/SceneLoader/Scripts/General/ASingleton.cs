using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Noon.General {

    /// <summary>
    /// GameObjectである必要がないことも十分にあろうから、ただのsingleton
    /// </summary>
    /// <typeparam name="T"></typeparam>

    public abstract class ASingleton<T> where T : ASingleton<T>,new (){

        private static T m_instance;

        /// <summary>
        /// Singletone<T> のインスタンス
        /// m_instance == null であれば、インスタンスを自動生成
        /// </summary>
        public static T Instance {
            get {

                if (!ExistInstance()) {

                    m_instance = CreateInstance();

                }

                return m_instance;
            }
        }

        private static bool ExistInstance() {

            return m_instance != null;
        }

        /// <summary>
        /// クラスによってコンストラクタが変わるだろうから、インスタンス生成は各クラスに委任
        /// </summary>
        /// <returns></returns>
        protected static T CreateInstance() {
            return new T();
        }

        protected ASingleton() { }
    }
}