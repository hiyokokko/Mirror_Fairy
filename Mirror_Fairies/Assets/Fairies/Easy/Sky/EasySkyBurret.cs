using UnityEngine;
public class EasySkyBurret : MonoBehaviour
{
	public Vector2 target;
	public float speed;
	public float moveWait;
	float time;
	void Start()
	{
		transform.LookAt(target);
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180 + transform.localEulerAngles.x);
	}
	void Update()
	{
		time += Time.deltaTime;
		if (time >= moveWait) { transform.position += transform.right * Time.deltaTime * speed; }
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
