using UnityEngine;
public class NormalSkyBurret : MonoBehaviour
{
	public float speed;
	float radius;
	void Start()
	{
		radius = 1.0f;
	}
	void Update()
	{
		transform.position += transform.right * Time.deltaTime * speed;
		Mirror();
		Destroy();
	}
	void Mirror()
	{
		if (EndChecker.EndTop(transform.position.y) || EndChecker.EndBottom(transform.position.y))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, -transform.localEulerAngles.z);
		}
	}
	void Destroy()
	{
		if (EndChecker.EndLeft(transform.position.x + radius))
		{
			Destroy(gameObject);
		}
	}
}
