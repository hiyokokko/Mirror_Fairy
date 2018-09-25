using UnityEngine;
using UnityEngine.UI;
public class SelectManager : MonoBehaviour
{
	[SerializeField] Text recordKillText;
	[SerializeField] Text recordTimeText;
	public static int diff;
	public static string[] recordDataName;
	int sceneNum;
	void Start()
	{
		diff = 0;
		recordDataName = new string[2];
		RecordDisplay();
	}
	public void SelectDiff(int selectDiff)
	{
		diff = selectDiff;
		RecordDisplay();
	}
	void RecordDisplay()
	{
		recordDataName[0] = ((RecordDataName)(diff * 2)).ToString();
		recordDataName[1] = ((RecordDataName)(diff * 2 + 1)).ToString();
		if (PlayerPrefs.HasKey(recordDataName[0]))
		{
			recordKillText.text = "RECORD KILL _ " + PlayerPrefs.GetInt(recordDataName[0]);
			recordTimeText.text = "RECORD TIME _ " + PlayerPrefs.GetFloat(recordDataName[1]);
		}
		else
		{
			recordKillText.text = "RECORD KILL _ NO DATA";
			recordTimeText.text = "RECORD TIME _ NO DATA";
		}
	}
	public void MainScene()
	{
		sceneNum = 2;
		SceneChanger.sceneChange = sceneNum;
	}
}
