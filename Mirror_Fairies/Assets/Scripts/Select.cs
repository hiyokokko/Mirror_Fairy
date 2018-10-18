using UnityEngine;
using UnityEngine.UI;
public class Select : MonoBehaviour
{
	[SerializeField] GameObject[] diffButton;
	[SerializeField] Text recordKillText;
	[SerializeField] Text recordTimeText;
	public static int diff;
	int beforeDiff;
	bool scaleChange;
	Vector3 defaultScale;
	float scaleSmall;
	float scaleBig;
	float scaleChangeWait;
	float scaleChangeTime;
	float scaleChangeSpeed;
	void Start()
	{
		diff = 0;
		beforeDiff = 0;
		scaleChange = false;
		defaultScale = new Vector3(0.05f, 0.05f, 0.05f);
		scaleSmall = 1.0f;
		scaleBig = 2.0f;
		scaleChangeWait = 0.25f;
		scaleChangeTime = 0.0f;
		scaleChangeSpeed = 1.0f / scaleChangeWait;
		Record.RecordDisplay(recordKillText, recordTimeText);
	}
	void Update()
	{
		if (scaleChange) { ScaleChange(); }
	}
	public void SelectDiff(int selectDiff)
	{
		if (!scaleChange)
		{
			AudioSE.button = true;
			beforeDiff = diff;
			diff = selectDiff;
			if (beforeDiff != diff) { scaleChange = true; }
			Record.RecordDisplay(recordKillText, recordTimeText);
		}
	}
	void ScaleChange()
	{
		scaleChangeTime += Time.deltaTime;
		diffButton[diff].transform.localScale = defaultScale * (scaleSmall + scaleChangeTime * scaleChangeSpeed);
		diffButton[beforeDiff].transform.localScale = defaultScale * (scaleBig - scaleChangeTime * scaleChangeSpeed);
		if (scaleChangeTime >= scaleChangeWait)
		{
			diffButton[diff].transform.localScale = defaultScale * scaleBig;
			diffButton[beforeDiff].transform.localScale = defaultScale * scaleSmall;
			scaleChangeTime = 0.0f;
			scaleChange = false;
		}
	}
	public void SelectScene(int selectScene)
	{
		AudioSE.button = true;
		SceneChanger.sceneChange = selectScene;
	}
}
enum DiffName
{
	Easy,
	Normal,
	Hard
}
