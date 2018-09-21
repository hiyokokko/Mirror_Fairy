using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
	public static int sceneChange = -1;
	bool cameraMove = false;
	float cameraSpeed = 18.0f;
	float[] cameraTarget = new float[2]
	{
		18.0f,
		0.0f
	};
	void Start()
	{
		DontDestroyOnLoad(gameObject);
	}
	void Update ()
	{
		SceneChange();
	}
	void SceneChange()
	{
		
		if (sceneChange != -1 && !cameraMove)
		{
			cameraMove = true;
		}
		if (cameraMove)
		{
			GameObject camera = GameObject.Find("Camera");
			camera.transform.position = new Vector3(0.0f, camera.transform.position.y + Time.deltaTime * cameraSpeed, -10.0f);
			if (sceneChange != -1 && camera.transform.position.y > cameraTarget[0])
			{
				SceneManager.LoadScene(sceneChange);
				sceneChange = -1;
			}
			else if (sceneChange == -1 && camera.transform.position.y > cameraTarget[1])
			{
				camera.transform.position = new Vector3(0.0f, cameraTarget[1], -10.0f);
				cameraMove = false;
			}
		}
	}
}
