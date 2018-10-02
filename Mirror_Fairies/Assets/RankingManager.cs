using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
	[SerializeField] InputField nameInput;
	[SerializeField] InputField passInput;
	public void Entry()
	{
		string name = nameInput.text;
		string pass = passInput.text;
		if (PlayerPrefs.HasKey(SelectManager.recordDataName[0]) && name != "" && pass != "")
		{
			Result myBestRecord = new Result(PlayerPrefs.GetInt(SelectManager.recordDataName[0]), PlayerPrefs.GetFloat(SelectManager.recordDataName[1]));
			string json = JsonUtility.ToJson(myBestRecord);
			FirebaseManager.databaseReference.Child("ranking").Child("easy").Child(name).SetRawJsonValueAsync(json);
		}
	}
}
