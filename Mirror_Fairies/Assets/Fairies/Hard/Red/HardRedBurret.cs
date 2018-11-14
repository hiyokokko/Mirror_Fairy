using UnityEngine;
public class HardRedBurret : MonoBehaviour
{
	public float speed;
	void Update()
	{
		transform.position += transform.right * Time.deltaTime * speed;
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
