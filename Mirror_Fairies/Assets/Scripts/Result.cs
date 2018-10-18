using UnityEngine;
using UnityEngine.UI;
public class Result : MonoBehaviour
{
	[SerializeField] GameObject congratulations;
	[SerializeField] Text resultKillText;
	[SerializeField] Text resultTimeText;
	public static RecordData resultRecordData;
	void Start ()
	{
		Record.RecordUpdate(resultRecordData);
		ResultDisplay();
	}
	void ResultDisplay()
	{
		if (resultRecordData.kill == Main.enemyNum)
		{
			congratulations.SetActive(true);
			resultKillText.text = "ALL FAIRY KILL!!";
			resultTimeText.text = "TIME _ " + resultRecordData.time + " s";
		}
		else
		{
			resultKillText.text = "KILL _ " + resultRecordData.kill;
			resultTimeText.text = "TIME _ " + resultRecordData.time + " s";
		}
	}
	public void SelectScene(int selectScene)
	{
		AudioSE.button = true;
		SceneChanger.sceneChange = selectScene;
	}
}
