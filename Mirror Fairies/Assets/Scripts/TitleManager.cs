using UnityEngine;
public class TitleManager : MonoBehaviour
{
	int sceneNum;
	public static bool mobile;
	void Start()
	{
		mobile = true;
	}
	public void Select()
	{
		sceneNum = 1;
		SceneChanger.sceneChange = sceneNum;
	}
}
