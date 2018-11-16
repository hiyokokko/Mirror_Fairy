using UnityEngine;
public class EasyBlueBurret : MonoBehaviour
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
		if (EndChecker.EndTop(transform.position.y) || EndChecker.EndBottom(transform.position.y))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, -transform.localEulerAngles.z);
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
