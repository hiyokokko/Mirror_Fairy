﻿using UnityEngine;
public class Title : MonoBehaviour
{
	void Update()
	{
		if (TouchOperation.GetTouch(0) == TouchInfo.End) { SelectScene(1); }
	}
	void SelectScene(int selectScene)
	{
		SceneChanger.sceneChange = selectScene;
	}
}
