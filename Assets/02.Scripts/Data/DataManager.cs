using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[SerializeField]
public class PlayerData
{
    public string name;     
    public int stage = 0;       
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    public PlayerData nowPlayer = new PlayerData();     
    public string path;
    public int nowStage;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        #endregion

        path = Application.persistentDataPath + "/savedata";
        print(path);
        LoadData();
        
    }


    // 데이터 저장
    public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path, data);
        Debug.Log("Save Success : " + path);
    }

    // 데이터 불러오기
    public void LoadData()
    {
        try
        {
            string data = File.ReadAllText(path);
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        }
        catch(FileNotFoundException e)
        {
            Debug.LogWarning("FileNotFoundException: " + e.Message);
        }
        
    }

    // 데이터 초기화
    public void DataClear()
    {
        nowPlayer = new PlayerData();
        SaveData();
    }

    // 데이터 삭제
    public void DeleteData()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("Data deleted.");
        }
        else
        {
            Debug.LogWarning("Data not found for deletion.");
        }
    }
}
