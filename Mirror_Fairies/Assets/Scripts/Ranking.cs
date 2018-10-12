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
	int page;
	void Start()
	{
		string os = TouchOperation.windows ? "Windows" : "Android";
		string diffName = ((DiffName)Select.diff).ToString();
		path = "Ranking/" + os + "/" + diffName + "/";
		page = 0;
		FirebaseManager.RankingDataRead(path).ContinueWith(rankingDataRead =>
		{
			if (rankingDataRead.IsFaulted)
			{
				Debug.Log(rankingDataRead.Exception);
			}
			else {
				try
				{
					RankingDisplay(rankingDataRead.Result);
				}
				catch (System.Exception e)
				{

					Debug.Log(e);
				}
				
			}
		});
	}
	public void Entry()
	{
		string name = nameInput.text;
		string pass = passInput.text;
		if (name != "" && pass != "")
		{
			entryText.text = "Entry Now";
			FirebaseManager.RankingDataWrite(path, name, pass).ContinueWith(rankingDataWrite => 
			{
				if (rankingDataWrite.Result)
				{
					FirebaseManager.RankingResult(path, name).ContinueWith(rankingResult =>
					{
						entryText.text = rankingResult.Result;
					});
				}
				else
				{
					entryText.text = "Entry Error";
				}
			});
		}
		else
		{
			entryText.text = "No Data or Pass Miss";
		}
	}
	void RankingDisplay(List<RankingData> rankingDataList)
	{
		for (int rank = page * 5 + 1; rank <= page * 5 + 5; rank++)
		{
			if (rank <= rankingDataList.Count)
			{
				switch (rank)
				{
					case 1:
						rankingText[rank - 1].text =  "1st:" + rankingDataList[page * 5 + rank - 1].name + "\n" + "KILL:" + rankingDataList[page * 5 + rank - 1].kill + " TIME:" + rankingDataList[page * 5 + rank - 1].time + " s";
						break;
					case 2:
						rankingText[rank - 1].text = "2nd:" + rankingDataList[page * 5 + rank - 1].name + "\n" + "KILL:" + rankingDataList[page * 5 + rank - 1].kill + " TIME:" + rankingDataList[page * 5 + rank - 1].time + " s";
						break;
					case 3:
						rankingText[rank - 1].text = "3rd:" + rankingDataList[page * 5 + rank - 1].name + "\n" + "KILL:" + rankingDataList[page * 5 + rank - 1].kill + " TIME:" + rankingDataList[page * 5 + rank - 1].time + " s";
						break;
					default:
						rankingText[rank - 1].text = rank + "th:" + rankingDataList[page * 5 + rank - 1].name + "\n" + "KILL:" + rankingDataList[page * 5 + 1].kill + " TIME:" + rankingDataList[page * 5 + 1].time + " s";
						break;
				}
			}
			else
			{
				rankingText[rank - 1].text = "";
			}
		}
	}
	public void SelectScene(int selectScene)
	{
		SceneChanger.sceneChange = selectScene;
	}
}
