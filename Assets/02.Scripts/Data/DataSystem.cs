using System;
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
	private static string SavePath => Path.Combine(Application.persistentDataPath, "saves");

	public static void Save(PlayerData saveData, string saveFileName)
	{
		try
		{
			if (!Directory.Exists(SavePath))
			{
				Directory.CreateDirectory(SavePath);
			}

			string saveJson = JsonUtility.ToJson(saveData);
			string saveFilePath = Path.Combine(SavePath, saveFileName + ".json");
			File.WriteAllText(saveFilePath, saveJson);
			Debug.Log("Save Success: " + saveFilePath);
		}
		catch (Exception e)
		{
			Debug.LogError($"Save Failed: {e.Message}");
		}
	}

	public static PlayerData Load(string saveFileName)
	{
		string saveFilePath = Path.Combine(SavePath, saveFileName + ".json");

		try
		{
			if (!File.Exists(saveFilePath))
			{
				Debug.LogError("No such saveFile exists");
				return null;
			}

			string saveFile = File.ReadAllText(saveFilePath);
			PlayerData saveData = JsonUtility.FromJson<PlayerData>(saveFile);
			return saveData;
		}
		catch (Exception e)
		{
			Debug.LogError($"Load Failed: {e.Message}");
			return null;
		}
	}

}