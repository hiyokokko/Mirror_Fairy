using UnityEngine;
public class AudioBGM : MonoBehaviour
{
	[SerializeField] AudioSource audioSourceBGM;
	public static bool startBGM;
	public static bool stopBGM;
	void Start()
	{
		startBGM = false;
		stopBGM = false;
	}
	void Update()
	{
		if (startBGM) { StartBGM(); }
		if (stopBGM) { StopBGM(); }
	}
	void StartBGM()
	{
		audioSourceBGM.Play();
		startBGM = false;
	}
	void StopBGM()
	{
		audioSourceBGM.Stop();
		stopBGM = false;
	}
}
