using UnityEngine;
using UnityEngine.UI;
public class ResultManager : MonoBehaviour
{
	[SerializeField] GameObject congratulations;
	[SerializeField] Text downText;
	[SerializeField] Text timeText;
	public static Result result;
	void Start ()
	{
		if (result.down == MainManager.enemyNum)
		{
			congratulations.SetActive(true);
			downText.text = "All Fairy Down!!";
			timeText.text = "time ： " + result.time.ToString("F2") + " m";
		}
		else
		{
			downText.text = result.down + " Fairy Down";
			timeText.text = "time " + result.time.ToString("F2") + " m";
		}
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
