using UnityEngine;
public class EasyPurpleBurret : MonoBehaviour
{
	public float speed;
	float radius;
	void Start()
	{
		radius = 2.0f;
	}
	void Update()
	{
		transform.position += transform.right * Time.deltaTime * speed;
		Destroy();
	}
	void Destroy()
	{
		if (EndChecker.EndLeft(transform.position.x + radius))
		{
			Destroy(gameObject);
		}
	}
}
