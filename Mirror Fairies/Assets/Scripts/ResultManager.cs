using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// リザルトシーンの管理。
/// </summary>
public class ResultManager : MonoBehaviour
{
	[SerializeField] GameObject congratulations;
	[SerializeField] Text killText;
	[SerializeField] Text timeText;
	public static Result result;
	void Start ()
	{
		RecordSave();
		ResultDisplay();
	}
	/// <summary>
	/// 新記録が出たらセーブする。
	/// </summary>
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
	/// <summary>
	/// リザルトを表示する。
	/// </summary>
	void ResultDisplay()
	{
		if (result.kill == MainManager.enemyNum)
		{
			congratulations.SetActive(true);
			killText.text = "ALL FAIRY KILL!!";
			timeText.text = "TIME _ " + result.time + " s";
		}
		else
		{
			killText.text = "KILL _ " + result.kill;
			timeText.text = "TIME _ " + result.time + " s";
		}
	}
	/// <summary>
	/// シーンを切り替える。
	/// </summary>
	/// <param name="selectScene">選んだシーン番号</param>
	public void SelectScene(int selectScene)
	{
		SceneChanger.sceneChange = selectScene;
	}
}
/// <summary>
/// キル数、経過時間を入れるクラス。
/// </summary>
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
