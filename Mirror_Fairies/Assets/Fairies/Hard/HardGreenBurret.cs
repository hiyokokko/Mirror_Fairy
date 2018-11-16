using UnityEngine;
public class HardGreenBurret : MonoBehaviour
{
	public float speed;
	public float moveWait;
	float moveTime;
	void Update()
	{
		if (moveTime < moveWait)
		{
			moveTime += Time.deltaTime;
		}
		else
		{
			transform.position += transform.right * speed;
			moveTime -= moveWait;
		}
		Destroy();
	}
	void Destroy()
	{
		if (EndChecker.EndLeft(transform.position.x))
		{
			Destroy(gameObject);
		}
	}
}
