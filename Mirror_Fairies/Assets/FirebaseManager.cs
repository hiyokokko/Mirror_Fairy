using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
public class FirebaseManager : MonoBehaviour
{
	public static DatabaseReference databaseReference;
	void Start()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://mirrorfairies.firebaseio.com/");
		databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
	}
	public static string RankingDataWrite(string os, string diff, string name, string pass)
	{
		string returnStr;
		string path = "Ranking/" + os + "/" + diff + "/" + name + "/";
		databaseReference.Child(path).GetValueAsync().ContinueWith(task =>
		{
			if (task.IsCompleted)
			{
				Debug.Log("Connection success");
				DataSnapshot dataSnapshot = task.Result;
				if ((string)dataSnapshot.GetValue(true) == null)
				{
					Debug.Log("Data not found");
					RankingData rankingData = new RankingData(pass, new Record(PlayerPrefs.GetInt(SelectManager.recordDataName[0]), PlayerPrefs.GetFloat(SelectManager.recordDataName[1])));
					databaseReference.Child(path).SetRawJsonValueAsync(JsonUtility.ToJson(rankingData));
					returnStr = "New data create";
				}
				else if ((string)dataSnapshot.Child("pass").GetValue(true) == pass)
				{
					Debug.Log("Data found");
					RankingData rankingData = new RankingData(pass, new Record(PlayerPrefs.GetInt(SelectManager.recordDataName[0]), PlayerPrefs.GetFloat(SelectManager.recordDataName[1])));
					databaseReference.Child(path).SetRawJsonValueAsync(JsonUtility.ToJson(rankingData));
				}
				else if ((string)dataSnapshot.Child("pass").GetValue(true) != pass)
				{
					Debug.Log("Password mismatch");
					returnStr = "Password miss";
				}
			}
			else
			{
				Debug.Log("Connection failed");
				returnStr = "No Connect";
			}
		});
		return "";
	}
}
