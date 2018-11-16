using UnityEngine;
public class BackGroundScroll : MonoBehaviour
{
	float speed = 4.0f;
	float size = 32.0f;
	int backGroundNum = 3;
	void Update ()
	{
		transform.position -= transform.right * Time.deltaTime * speed;
		if (transform.position.x <= -size) { transform.position += transform.right * size * backGroundNum; }
	}
}
