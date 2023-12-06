using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[System.Serializable]
public class DataSystem
{
	public DataSystem(string _name)
	{
		name = _name;
	}
	public string name;
}
public static class SaveSystem
{
	private static string SavePath => Application.persistentDataPath + "/saves/";

	public static void Save(DataSystem saveData, string saveFileName)
	{
		if (!Directory.Exists(SavePath))
		{
			Directory.CreateDirectory(SavePath);
		}

		string saveJson = JsonUtility.ToJson(saveData);

		string saveFilePath = SavePath + saveFileName + ".json";
		File.WriteAllText(saveFilePath, saveJson);
		Debug.Log("Save Success: " + saveFilePath);
	}

	public static DataSystem Load(string saveFileName)
	{
		string saveFilePath = SavePath + saveFileName + ".json";

		if (!File.Exists(saveFilePath))
		{
			Debug.LogError("No such saveFile exists");
			return null;
		}

		string saveFile = File.ReadAllText(saveFilePath);
		DataSystem saveData = JsonUtility.FromJson<DataSystem>(saveFile);
		return saveData;
	}
}
public class SaveLoadController : MonoBehaviour
{

	void Update()
	{
		if (Input.GetKeyDown("s"))
		{
			DataSystem character = new DataSystem("Mosia");

			SaveSystem.Save(character, "save_001");
		}

		if (Input.GetKeyDown("l"))
		{
			DataSystem loadData = SaveSystem.Load("save_001");
			Debug.Log(string.Format("LoadData Result => name : {0}", loadData.name));
		}
	}
}