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
		string os = TouchOperation.windows ? "Windows" : "Andloid";
		string diffName = ((DiffName)Select.diff).ToString();
		path = "Ranking/" + os + "/" + diffName + "/";
		page = 0;
		FirebaseManager.RankingDataRead(path).ContinueWith(rankingDataRead =>
		{
			RankingDisplay(rankingDataRead.Result);
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
		for (int i = 0; i < 5; i++)
		{
			if (page * 5 + i < rankingDataList.Count)
			{
				rankingText[i].text = i + "st:" + rankingDataList[page * 5 + i].name + "\n" + "KILL:" + rankingDataList[page * 5 + 1].kill + " TIME:" + rankingDataList[page * 5 + 1].time + " s";
			}
			else
			{
				rankingText[i].text = "";
			}
		}
	}
	public void SelectScene(int selectScene)
	{
		SceneChanger.sceneChange = selectScene;
	}
}
