using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Ranking : MonoBehaviour
{
	[SerializeField] InputField nameInput;
	[SerializeField] InputField passInput;
	[SerializeField] Text EntryText;
	[SerializeField] Text[] rankingText;
	List<RankingData> rankingDataList;
	int page;
	void Start()
	{
		page = 0;
	}
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			FirebaseManager.RankingDataRead().ContinueWith(task =>
			{
				rankingDataList = task.Result;
				RankingDisplay();
			});
		}
	}
	public void Entry()
	{
		string name = nameInput.text;
		string pass = passInput.text;
		if (Record.recordData.kill >= 1 && name != "" && pass != "")
		{
			FirebaseManager.RankingDataWrite(name, pass).ContinueWith(task =>
			{
				EntryText.text = task.Result;
			});
		}
		else
		{

		}
	}
	void RankingDisplay()
	{
		for (int i = 0; i < 5; i++)
		{
			rankingText[i].text = i + "st:" + rankingDataList[page * 5 + i].name + @"\n" + "KILL:" + rankingDataList[page * 5 + 1].kill + " TIME:" + rankingDataList[page * 5 + 1].time + " s";
		}
	}
}
