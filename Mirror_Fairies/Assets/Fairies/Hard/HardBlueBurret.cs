using UnityEngine;
public class HardBlueBurret : MonoBehaviour
{
	public float speed;
	bool mirrorY = false;
	void Update()
	{
		transform.position += transform.right * Time.deltaTime * speed;
		Mirror();
		Destroy();
	}
	void Mirror()
	{
		if (!mirrorY && (EndChecker.EndTop(transform.position.y) || EndChecker.EndBottom(transform.position.y)))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, -transform.localEulerAngles.z);
			mirrorY = true;
		}
		else if (mirrorY && !EndChecker.EndTop(transform.position.y) && !EndChecker.EndBottom(transform.position.y))
		{
			mirrorY = false;
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
