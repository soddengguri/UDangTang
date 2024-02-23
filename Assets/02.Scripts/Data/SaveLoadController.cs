using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadController : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown("s"))
		{
			PlayerData character = new PlayerData { name = "우당탕" };
			SaveSystem.Save(character, "save_001");
		}

		if (Input.GetKeyDown("l"))
		{
			PlayerData loadData = SaveSystem.Load("save_001");
			Debug.Log(string.Format("LoadData Result => name : {0}", loadData.name));
		}
	}
}
