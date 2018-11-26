using UnityEngine;
public class Title : MonoBehaviour
{
	void Start()
	{
		PlayerPrefs.DeleteAll();
	}
	void Update()
	{
		if (TouchOperation.GetTouch(0) == TouchInfo.End) { SelectScene(1); }
	}
	void SelectScene(int selectScene)
	{
		AudioSE.button = true;
		SceneChanger.sceneChange = selectScene;
	}
}
