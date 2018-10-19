using UnityEngine;
public class AudioSE : MonoBehaviour
{
	[SerializeField] AudioSource audioSourceSE;
	[SerializeField] AudioClip[] audioClipSE;
	public static bool button;
	public static bool damage;
	public static bool kill;
	void Start()
	{
		button = false;
		damage = false;
		kill = false;
	}
	void Update()
	{
		if (button) { ButtonSE(); }
		if (damage) { DamageSE(); }
		if (kill) { KillSE(); }
	}
	void ButtonSE()
	{
		audioSourceSE.clip = audioClipSE[0];
		audioSourceSE.Play();
		button = false;
	}
	void DamageSE()
	{
		audioSourceSE.clip = audioClipSE[1];
		audioSourceSE.Play();
		damage = false;
	}
	void KillSE()
	{
		audioSourceSE.clip = audioClipSE[2];
		audioSourceSE.Play();
		kill = false;
	}
}
