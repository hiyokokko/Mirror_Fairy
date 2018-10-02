using UnityEngine;
/// <summary>
/// タイトルシーンの管理。
/// </summary>
public class TitleManager : MonoBehaviour
{
	void Update()
	{
		if (TouchOperation.GetTouch(0) == TouchInfo.End) { SelectScene(1); }
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
