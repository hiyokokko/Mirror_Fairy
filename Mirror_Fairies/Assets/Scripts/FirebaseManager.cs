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
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://mirrorfairiestest.firebaseio.com/");
		databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
	}
	/// <summary>
	/// ランキングデータをList化し読み込む。
	/// </summary>
	/// <returns>List化されたランキングデータ。順位通りに並んでいる。</returns>
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
						(double)current.Child("time").GetValue(true)
					);
					rankingDataList.Add(rankingData);
				}
				rankingDataList.Sort(Compare);
			}
			return rankingDataList;
		});
	}
	/// <summary>
	/// データの有無、パスワード一致確認してデータベースへランキングデータを書き込む。
	/// </summary>
	/// <returns>書き込んだなら true。書き込んでいないなら false。</returns>
	public static Task<bool> RankingDataWrite(string path, string name, string password)
	{
		return databaseReference.Child(path + name).GetValueAsync().ContinueWith(task =>
		{
			try
			{
				if (task.IsCompleted)
				{
					string hashPassword = GetHashedTextString(password);
					DataSnapshot dataSnapshot = task.Result;
					if (dataSnapshot.Value == null ||
						(string)dataSnapshot.Child("password").GetValue(true) == hashPassword)
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
					return false;
				}
			}catch (System.Exception e)
			{
				Debug.Log(e.Message);
				return false;
			}
		});
	}
	/// <summary>
	/// 入力された名前のランキングデータの順位を返す。
	/// </summary>
	/// <param name="name">入力した名前</param>
	/// <returns>順位</returns>
	public static Task<string> RankingResult(string path, string name)
	{
		return RankingDataRead(path).ContinueWith(rankingDataRead => 
		{
			try
			{
				List<RankingData> rankingDataList = rankingDataRead.Result;
				for (int rank = 1; rank <= rankingDataList.Count; rank++)
				{
					if (rankingDataList[rank - 1].name == name)
					{
						switch (rank)
						{
							case 1: return "you rank" + "\n" + "1st of " + rankingDataList.Count.ToString();
							case 2: return "you rank" + "\n" + "2nd of " + rankingDataList.Count.ToString();
							case 3: return "you rank" + "\n" + "3rd of " + rankingDataList.Count.ToString();
							default: return "you rank" + "\n" + rank.ToString() + "th of " + rankingDataList.Count.ToString();
						}
					}
				}
				return "Network Error";
			}catch (System.Exception e)
			{
				Debug.Log(e.Message);
				return "Error";
			}
		});
	}
	/// <summary>
	/// ランキングデータを順位通りに並び替える。
	/// </summary>
	/// <returns>キルの降順。キルが同一ならタイムの昇順。タイムも同一なら名前の昇順。</returns>
	static int Compare(RankingData a, RankingData b)
	{
		if (a.kill < b.kill) { return 1; }
		else if (a.kill > b.kill) { return -1; }
		else if (a.time > b.time) { return 1; }
		else if (a.time < b.time) { return -1; }
		else { return a.name.CompareTo(b.name); }
	}
	/// <summary>
	/// パスワードのハッシュ化。
	/// </summary>
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
/// <summary>
/// ランキングデータの構造体。
/// </summary>
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
