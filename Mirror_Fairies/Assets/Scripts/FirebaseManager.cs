using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class FirebaseManager : MonoBehaviour
{
	public static DatabaseReference databaseReference;
	void Start()
	{
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://mirrorfairies.firebaseio.com/");
		databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
	}
	public static Task<string> RankingDataWrite(string os, string diff, string name, string password)
	{
		string path = "Ranking/" + os + "/" + diff + "/" + name + "/";
		string hashPassword = GetHashedTextString(password);
		return databaseReference.Child(path).GetValueAsync().ContinueWith(task =>
		{
			if (task.IsCompleted)
			{
				DataSnapshot dataSnapshot = task.Result;
				if (dataSnapshot.GetValue(true) == null)
				{
					RankingData rankingData = new RankingData(hashPassword, new RecordData(PlayerPrefs.GetInt(((RecordDataName)(Select.diff * 2)).ToString()), PlayerPrefs.GetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString())));
					databaseReference.Child(path).SetRawJsonValueAsync(JsonUtility.ToJson(rankingData));
					return "New data create";
				}
				else if ((string)dataSnapshot.Child("pass").GetValue(true) == hashPassword)
				{
					RankingData rankingData = new RankingData(hashPassword, new RecordData(PlayerPrefs.GetInt(((RecordDataName)(Select.diff * 2)).ToString()), PlayerPrefs.GetFloat(((RecordDataName)(Select.diff * 2 + 1)).ToString())));
					databaseReference.Child(path).SetRawJsonValueAsync(JsonUtility.ToJson(rankingData));
					return "Data update";
				}
				else
				{
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
class RankingData
{
	public string password;
	public int kill;
	public float time;
	public RankingData(string password, RecordData record)
	{
		this.password = password;
		kill = record.kill;
		time = record.time;
	}
}
