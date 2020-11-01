using System;
using System.IO;
using UnityEngine;

/*===================================================
 * セーブ用クラス
 ====================================================*/

public class DataSave<T>{
    private string filePath = Application.persistentDataPath;  //保存ディレクトリパス
    private string m_currentFileName = "/";
    private Func<T> m_TInit;

    public DataSave(string filename,Func<T> init){
        m_currentFileName += filename;
        m_TInit = init;
    }


    public T Load() {
        T data = m_TInit();

        try {
            using (StreamReader sr = new StreamReader(filePath + m_currentFileName + ".json")) {
                string json = sr.ReadToEnd();
                data = JsonUtility.FromJson<T>(json);
            }
        } catch {
            Save(data);
            
        }

        return data;
    }

    public void Save(T data) {
        try {
            using (StreamWriter sw = new StreamWriter(filePath + m_currentFileName + ".json", false)) {

                string json = JsonUtility.ToJson(data);
                Debug.Log(json);
                sw.WriteLine(json);
            }
        } catch { }
        
    }


}
