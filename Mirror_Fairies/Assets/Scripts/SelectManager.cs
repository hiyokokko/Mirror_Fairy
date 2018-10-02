using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// セレクトシーンの管理。
/// </summary>
public class SelectManager : MonoBehaviour
{
	[SerializeField] Text recordKillText;
	[SerializeField] Text recordTimeText;
	public static int diff;
	public static string[] recordDataName;
	void Start()
	{
		diff = 0;
		recordDataName = new string[2];
		RecordDisplay();
	}
	/// <summary>
	/// レコードの表示。レコードがなければ NO DATA が表示される。
	/// </summary>
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
	/// <summary>
	/// 難易度を変える。
	/// </summary>
	/// <param name="selectDiff">選んだ難易度番号</param>
	public void SelectDiff(int selectDiff)
	{
		diff = selectDiff;
		RecordDisplay();
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
