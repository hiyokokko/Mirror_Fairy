using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
	int sceneNum;
	public void Select()
	{
		sceneNum = 1;
		SceneChanger.sceneChange = sceneNum;
	}
}
