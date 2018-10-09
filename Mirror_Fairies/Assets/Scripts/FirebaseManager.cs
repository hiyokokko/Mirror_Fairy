using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class FirebaseManager : MonoBehaviour
{
	static List<RankingData> rankingDataList = new List<RankingData>();
	static Dictionary<string, RecordData> dataDict = new Dictionary<string, RecordData>();
	public static DatabaseReference databaseReference;
	void Start()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://mirrorfairies.firebaseio.com/");
		databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
	}

	public static Task<List<RankingData>> RankingDataRead()
	{
		Debug.Log("ランキング集計しゅる～");
		string os = TouchOperation.windows ? "Windows" : "Andloid";
		string diffName = ((DiffName)Select.diff).ToString();
		string path = "Ranking/" + os + "/" + diffName + "/";
		return databaseReference.Child(path).GetValueAsync().ContinueWith(task =>
		{
			try {
		Debug.Log("ランキング集計処理開始");
		DataSnapshot dataSnapshot = task.Result;
		IEnumerator<DataSnapshot> en = dataSnapshot.Children.GetEnumerator();
			while (en.MoveNext())
			{
				DataSnapshot data = en.Current;
				try
				{
						Debug.Log("いいぞ");
					RankingData rankingData = new RankingData
					((string)data.Child("name").GetValue(true),
					(string)data.Child("password").GetValue(true),
					new RecordData((long)data.Child("kill").GetValue(true),
					(double)data.Child("time").GetValue(true)));
					rankingDataList.Add(rankingData);
					//dataDict.Add((string)data.Child("name").GetValue(true), new RecordData((int)data.Child("kill").GetValue(true), (float)data.Child("time").GetValue(true)));
				}
				catch (Exception ex)
				{
					Debug.Log(ex.Message);
				}
			}
			//List<KeyValuePair<string, RecordData>> dataList = new List<KeyValuePair<string, RecordData>>(dataDict);
			//dataList.Sort(Compare);
			//foreach (var a in dataList)
			//{
			//	Debug.Log(dataList);
			//}
			//return dataList;
			rankingDataList.Sort(Compare);
			}catch (Exception ex)
			{
				Debug.Log(ex.Message);
			}
			return rankingDataList;
		});
	}
	static int Compare(RankingData a, RankingData b)
	{
		if (a.kill < b.kill)
		{
			return 1;
		}
		else if (a.kill > b.kill)
		{
			return -1;
		}
		else
		{
			if (a.time > b.time)
			{
				return 1;
			}
			else if (a.time < b.time)
			{
				return -1;
			}
			else
			{
				return a.name.CompareTo(b.name);
			}
		}
	}



	public static Task<string> RankingDataWrite(string name, string password)
	{
		string os = TouchOperation.windows ? "Windows" : "Andloid";
		string diffName = ((DiffName)Select.diff).ToString();
		string path = "Ranking/" + os + "/" + diffName + "/" + name + "/";
		string hashPassword = GetHashedTextString(password);
		return databaseReference.Child(path).GetValueAsync().ContinueWith(task =>
		{
			if (task.IsCompleted)
			{
				DataSnapshot dataSnapshot = task.Result;
				if (dataSnapshot.GetValue(true) == null)
				{
					RankingData rankingData = new RankingData(name, hashPassword, new RecordData(PlayerPrefs.GetInt(((RecordDataName)(Select.diff * 2)).ToString()), PlayerPrefs.GetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString())));
					databaseReference.Child(path).SetRawJsonValueAsync(JsonUtility.ToJson(rankingData));
					return "New data create";
				}
				else if ((string)dataSnapshot.Child("password").GetValue(true) == hashPassword)
				{
					Debug.Log((string)dataSnapshot.Child("password").GetValue(true));
					RankingData rankingData = new RankingData(name, hashPassword, new RecordData(PlayerPrefs.GetInt(((RecordDataName)(Select.diff * 2)).ToString()), PlayerPrefs.GetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString())));
					databaseReference.Child(path).SetRawJsonValueAsync(JsonUtility.ToJson(rankingData));
					return "Data update";
				}
				else
				{
					Debug.Log(dataSnapshot.Child("password").GetValue(true));
					return "Password miss";
				}
			}
			else
			{
				return "No connect";
			}
		});
	}
	protected static string GetHashedTextString(string password)
	{
		// パスワードをUTF-8エンコードでバイト配列として取り出す
		byte[] byteValues = Encoding.UTF8.GetBytes(password);
		// SHA256のハッシュ値を計算する
		SHA256 crypto256 = new SHA256CryptoServiceProvider();
		byte[] hash256Value = crypto256.ComputeHash(byteValues);
		// SHA256の計算結果をUTF8で文字列として取り出す
		StringBuilder hashedText = new StringBuilder();
		for (int i = 0; i < hash256Value.Length; i++)
		{
			// 16進の数値を文字列として取り出す
			hashedText.AppendFormat("{0:X2}", hash256Value[i]);
		}
		return hashedText.ToString();
	}
}
class Sort { 
}
public class RankingData
{
	public string name;
	public string password;
	public long kill;
	public double time;
	public RankingData(string name, string password, RecordData record)
	{
		this.name = name;
		this.password = password;
		kill = record.kill;
		time = record.time;
	}
}
