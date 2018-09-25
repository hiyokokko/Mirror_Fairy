using UnityEngine;
using UnityEngine.UI;
public class ResultManager : MonoBehaviour
{
	[SerializeField] GameObject congratulations;
	[SerializeField] Text downText;
	[SerializeField] Text timeText;
	public static Result result;
	int sceneNum;
	void Start ()
	{
		RecordSave();
		ResultDisplay();
	}
	void RecordSave()
	{
		if (PlayerPrefs.HasKey(SelectManager.recordDataName[0])){
			if (PlayerPrefs.GetInt(SelectManager.recordDataName[0]) < result.kill)
			{
				PlayerPrefs.SetInt(SelectManager.recordDataName[0], result.kill);
				PlayerPrefs.SetFloat(SelectManager.recordDataName[1], result.time);
				PlayerPrefs.Save();
			}
			else if (PlayerPrefs.GetInt(SelectManager.recordDataName[0]) == result.kill && PlayerPrefs.GetFloat(SelectManager.recordDataName[1]) > result.time)
			{
				PlayerPrefs.SetFloat(SelectManager.recordDataName[1], result.time);
				PlayerPrefs.Save();
			}
		}
		else
		{
			PlayerPrefs.SetInt(SelectManager.recordDataName[0], result.kill);
			PlayerPrefs.SetFloat(SelectManager.recordDataName[1], result.time);
			PlayerPrefs.Save();
		}
	}
	void ResultDisplay()
	{
		if (result.kill == MainManager.enemyNum)
		{
			congratulations.SetActive(true);
			downText.text = "ALL FAIRY KILL!!";
			timeText.text = "TIME _ " + result.time + " s";
		}
		else
		{
			downText.text = "KILL _ " + result.kill;
			timeText.text = "TIME _ " + result.time + " s";
		}
	}
	public void Select()
	{
		sceneNum = 1;
		SceneChanger.sceneChange = sceneNum;
	}
	public void Retry()
	{
		sceneNum = 2;
		SceneChanger.sceneChange = sceneNum;
	}
}
public class Result
{
	public int kill;
	public float time;
	public Result(int kill, float time)
	{
		this.kill = kill;
		this.time = time;
	}
}
