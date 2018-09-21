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
		if (result.down == MainManager.enemyNum)
		{
			congratulations.SetActive(true);
			downText.text = "ALL FAIRY KILL!!";
			timeText.text = "TIME _ " + result.time.ToString("F2") + " m";
		}
		else
		{
			downText.text = result.down + " FAIRY KILL";
			timeText.text = "YOUR DEATH";
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
	public int down;
	public float time;
	public Result(int down, float time)
	{
		this.down = down;
		this.time = time;
	}
}
