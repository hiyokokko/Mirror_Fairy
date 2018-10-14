using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class FirebaseManager : MonoBehaviour
{
	static DatabaseReference databaseReference;
	void Start()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://mirrorfairies.firebaseio.com/");
		databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
	}
	public static Task<List<RankingData>> RankingDataRead(string path)
	{
		return databaseReference.Child(path).GetValueAsync().ContinueWith(task =>
		{
			List<RankingData> rankingDataList = new List<RankingData>();
			if (task.IsCompleted)
			{
				DataSnapshot dataSnapshot = task.Result;
				IEnumerator<DataSnapshot> enumerator = dataSnapshot.Children.GetEnumerator();
				while (enumerator.MoveNext())
				{
					DataSnapshot current = enumerator.Current;
					RankingData rankingData = new RankingData
					(
						(string)current.Child("name").GetValue(true),
						(string)current.Child("password").GetValue(true),
						(long)current.Child("kill").GetValue(true),
						double.Parse(current.Child("time").GetRawJsonValue())
					);
					rankingDataList.Add(rankingData);
				}
				rankingDataList.Sort(Compare);
			}
			else
			{
				Debug.Log(task.Exception);
			}
			return rankingDataList;
		});
	}
	public static Task<bool> RankingDataWrite(string path, string name, string password)
	{
		return databaseReference.Child(path + name).GetValueAsync().ContinueWith(task =>
		{
			if (task.IsCompleted)
			{
				string hashPassword = GetHashedTextString(password);
				DataSnapshot dataSnapshot = task.Result;
				if (dataSnapshot == null || dataSnapshot.GetValue(true) == null || (string)dataSnapshot.Child("password").GetValue(true) == hashPassword)
				{
					RankingData rankingData = new RankingData
					(
						name,
						hashPassword,
						PlayerPrefs.GetInt(((DiffName)(Select.diff)).ToString() + "Kill"),
						Conversion.DoubleConversion(PlayerPrefs.GetFloat(((DiffName)(Select.diff)).ToString() + "Time"))
					);
					databaseReference.Child(path + name).SetRawJsonValueAsync(JsonUtility.ToJson(rankingData));
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				Debug.Log(task.Exception);
				return false;
			}
		});
	}
	static int Compare(RankingData a, RankingData b)
	{
		if (a.kill < b.kill) { return 1; }
		else if (a.kill > b.kill) { return -1; }
		else if (a.time > b.time) { return 1; }
		else if (a.time < b.time) { return -1; }
		else { return a.name.CompareTo(b.name); }
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
public class RankingData
{
	public string name;
	public string password;
	public long kill;
	public double time;
	public RankingData(string name, string password, long kill, double time)
	{
		this.name = name;
		this.password = password;
		this.kill = kill;
		this.time = time;
	}
}
