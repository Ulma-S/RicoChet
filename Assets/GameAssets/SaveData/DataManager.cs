using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Noon.General;

public class DataManager : ASingleton<DataManager> {

    private DataSave<PlayerData> m_dataStream;
    public PlayerData m_data;

    public DataManager() {
        m_dataStream = new DataSave<PlayerData>("data", () => { return new PlayerData(); });
        m_data = m_dataStream.Load();
    }
    
    public PlayerData CurrentData { get { return m_data; } }

    public void SaveCurrentFIle() {
        m_dataStream.Save(m_data);
    }

    public void ReloadFile() {
        m_data = m_dataStream.Load();
    }

}
