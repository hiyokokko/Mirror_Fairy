using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Threading.Tasks;

public class FirebaseManager : MonoBehaviour
{
	static string writeData;
	public static DatabaseReference databaseReference;
	void Start()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://mirrorfairies.firebaseio.com/");
		databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
	}
	public static string RankingDataWrite(string os, string diff, string name, string pass)
	{
		string path = "Ranking/" + os + "/" + diff + "/" + name + "/";
		Task<string> entryMassage =
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
					return "New data create";
				}
				else if ((string)dataSnapshot.Child("pass").GetValue(true) == pass)
				{
					Debug.Log("Data found");
					RankingData rankingData = new RankingData(pass, new Record(PlayerPrefs.GetInt(SelectManager.recordDataName[0]), PlayerPrefs.GetFloat(SelectManager.recordDataName[1])));
					databaseReference.Child(path).SetRawJsonValueAsync(JsonUtility.ToJson(rankingData));
					return "Data update";
				}
				else
				{
					Debug.Log("Password mismatch");
					return "Password miss";
				}
			}
			else
			{
				Debug.Log("Connection failed");
				return "No connect";
			}
		});
		return entryMassage.Result;
	}
}
public class RankingData
{
	public string pass;
	public int kill;
	public float time;
	public RankingData(string pass, Record record)
	{
		this.pass = pass;
		kill = record.kill;
		time = record.time;
	}
}
