using UnityEngine;
public class SelectManager : MonoBehaviour
{
	public static int diff;
	int sceneNum;
	public void SelectDiff(int selectDiff)
	{
		diff = selectDiff;
	}
	public void MainScene()
	{
		sceneNum = 2;
		SceneChanger.sceneChange = sceneNum;
	}
}
