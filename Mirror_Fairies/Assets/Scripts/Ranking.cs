using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ranking : MonoBehaviour
{
	[SerializeField] InputField nameInput;
	[SerializeField] InputField passInput;
	[SerializeField] Text entryText;
	[SerializeField] Text[] rankingText;
	string path;
	List<RankingData> rankingDataList;
	int rankingPage;
	int rankingPageMax;
	void Start()
	{
		string os = TouchOperation.windows ? "Windows" : "Android";
		string diffName = ((DiffName)Select.diff).ToString();
		path = "Ranking/" + os + "/" + diffName + "/";
		rankingPage = 0;
		rankingPageMax = 0;
		FirebaseManager.RankingDataRead(path).ContinueWith(rankingDataRead =>
		{
			if (rankingDataRead.IsCompleted)
			{
				rankingDataList = rankingDataRead.Result;
				rankingPage = 0;
				rankingPageMax = rankingDataList.Count - 1 / 5;
				RankingDisplay(rankingDataList);
			}
			else
			{
				Debug.Log(rankingDataRead.Exception);
			}
		});
	}
	public void Entry()
	{
		string name = nameInput.text;
		string pass = passInput.text;
		if (name == "" || pass == "")
		{
			entryText.text = "InputPlease";
		} 
		else if (!PlayerPrefs.HasKey(((DiffName)(Select.diff * 2)).ToString() + "Kill"))
		{
			entryText.text = "NoRecordData";
		}
		else
		{
			entryText.text = "EntryNow";
			FirebaseManager.RankingDataWrite(path, name, pass).ContinueWith(rankingDataWrite =>
			{
				if (rankingDataWrite.IsCompleted)
				{
					if (rankingDataWrite.Result)
					{
						FirebaseManager.RankingDataRead(path).ContinueWith(rankingDataRead =>
						{
							if (rankingDataRead.IsCompleted)
							{
								rankingDataList = rankingDataRead.Result;
								rankingPage = 0;
								rankingPageMax = rankingDataList.Count / 5;
								RankingDisplay(rankingDataList);
								entryText.text = RankingResult(rankingDataList, name);
							}
							else
							{
								Debug.Log(rankingDataRead.Exception);
								entryText.text = "Error";
							}
						});
					}
					else
					{
						entryText.text = "PasswordMiss";
					}
				}
				else
				{
					Debug.Log(rankingDataWrite.Exception);
					entryText.text = "Error";
				}
			});
		}
	}
	void RankingDisplay(List<RankingData> rankingDataList)
	{
		int rank = rankingPage * 5 + 1;
		foreach (Text text in rankingText)
		{
			if (rank <= rankingDataList.Count)
			{
				switch (rank)
				{
					case 1:
						text.text = "1st:" + rankingDataList[rank - 1].name + "\n" + "KILL:" + rankingDataList[rank - 1].kill + " TIME:" + rankingDataList[rank - 1].time + " s";
						break;
					case 2:
						text.text = "2nd:" + rankingDataList[rank - 1].name + "\n" + "KILL:" + rankingDataList[rank - 1].kill + " TIME:" + rankingDataList[rank - 1].time + " s";
						break;
					case 3:
						text.text = "3rd:" + rankingDataList[rank - 1].name + "\n" + "KILL:" + rankingDataList[rank - 1].kill + " TIME:" + rankingDataList[rank - 1].time + " s";
						break;
					default:
						text.text = rank + "th:" + rankingDataList[rank - 1].name + "\n" + "KILL:" + rankingDataList[rank - 1].kill + " TIME:" + rankingDataList[rank - 1].time + " s";
						break;
				}
			}
			else
			{
				text.text = "";
			}
			rank++;
		}
	}
	string RankingResult(List<RankingData> rankingDataList, string name)
	{
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
		return "Error";
	}
	public void RankingPageUp()
	{
		if (rankingPage > 0)
		{
			rankingPage--;
			RankingDisplay(rankingDataList);
		}
	}
	public void RankingPageDown()
	{
		if (rankingPage < rankingPageMax)
		{
			rankingPage++;
			RankingDisplay(rankingDataList);
		}
	}
	public void SelectScene(int selectScene)
	{
		SceneChanger.sceneChange = selectScene;
	}
}
