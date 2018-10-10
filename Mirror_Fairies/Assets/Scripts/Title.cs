using UnityEngine;
public class Title : MonoBehaviour
{
	void Update()
	{
		if (TouchOperation.GetTouch(0) == TouchInfo.End) { SelectScene(1); }
		if (Input.GetKeyDown(KeyCode.Space)) { PlayerPrefs.DeleteAll(); }
	}
	void SelectScene(int selectScene)
	{
		SceneChanger.sceneChange = selectScene;
	}
}
