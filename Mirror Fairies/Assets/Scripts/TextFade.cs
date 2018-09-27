using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// テキストをフェードイン、アウトを繰り返す。
/// </summary>
public class TextFade : MonoBehaviour
{
	[SerializeField] Text text;
	float time;
	void Update()
	{
		Fade();
		time += Time.deltaTime;
	}
	/// <summary>
	/// 時間経過でテキストをフェードイン、アウトする。
	/// </summary>
	void Fade()
	{
		text.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Abs(Mathf.Sin(time * Mathf.PI / 2)));
	}
}
