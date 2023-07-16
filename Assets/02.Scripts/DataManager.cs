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
        #region 
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


    // ?????? ????
    public void SaveData()
    {
        /*
        if (nowPlayer == null)
        {
            nowPlayer = new PlayerData();
        }
        */
        string data = JsonUtility.ToJson(nowPlayer);
        File.WriteAllText(path + nowStage.ToString(), data);
    }

    // ?????? ????
    public void LoadData()
    {
        try
        {
            string data = File.ReadAllText(path + nowStage.ToString());
            nowPlayer = JsonUtility.FromJson<PlayerData>(data);
        }
        catch(FileNotFoundException e)
        {
            Debug.Log("?????? ???????? ????????.");
        }
        
    }

    public void DataClear()
    {
        nowStage = -1;
        //nowPlayer = new PlayerData();
    }

    public void DeleteData(int number)
    {
        nowStage = number;
        File.Delete(path + number.ToString());
    }
}
