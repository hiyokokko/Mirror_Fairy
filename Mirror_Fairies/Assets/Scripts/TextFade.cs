using UnityEngine;
using UnityEngine.UI;
public class TextFade : MonoBehaviour
{
	[SerializeField] Text text;
	float time;
	void Update()
	{
		text.color = new Color(0.0f, 0.0f, 0.0f, Mathf.Abs(Mathf.Sin(time * Mathf.PI / 2)));
		time += Time.deltaTime;
	}
}
