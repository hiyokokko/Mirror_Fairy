using UnityEngine;
using UnityEngine.UI;
public class Select : MonoBehaviour
{
	public static int diff;
	[SerializeField] Text recordKillText;
	[SerializeField] Text recordTimeText;
	void Start()
	{
		diff = 0;
		Record.RecordDisplay(recordKillText, recordTimeText);
	}
	public void SelectDiff(int selectDiff)
	{
		diff = selectDiff;
		Record.RecordDisplay(recordKillText, recordTimeText);
	}
	public void SelectScene(int selectScene)
	{
		SceneChanger.sceneChange = selectScene;
	}
}
enum DiffName
{
	Easy,
	Normal,
	Hard
}