using UnityEngine;
public class NormalBlackBurret : MonoBehaviour
{
	public float speed;
	void Update()
	{
		transform.position += transform.right * Time.deltaTime * speed;
		Mirror();
		Destroy();
	}
	void Mirror()
	{
		if (EndChecker.EndRight(transform.position.x))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f - transform.localEulerAngles.z);
		}
	}
	void Destroy()
	{
		if (EndChecker.EndLeft(transform.position.x))
		{
			Destroy(gameObject);
		}
	}
}
