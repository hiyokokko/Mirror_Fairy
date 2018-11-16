using UnityEngine;
public class BurretController : MonoBehaviour
{
	[SerializeField] bool aim;
	[SerializeField] bool mirror;
	[SerializeField] float moveSpeed;
	[SerializeField] float moveWait;
	[SerializeField] bool moveBarst;
	[SerializeField] float destroyWait;
	public Vector2 target;
	bool mirrorPossX;
	bool mirrorPossY;
	float moveTime;
	void Start ()
	{
		if (aim)
		{
			transform.LookAt(target);
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180 + transform.localEulerAngles.x);
		}
	}
	void Update ()
	{
		if (moveTime < moveWait)
		{
			moveTime += Time.deltaTime;
		}
		else
		{
			if (!moveBarst)
			{
				transform.position += transform.right * Time.deltaTime * moveSpeed;
			}
			else
			{
				transform.position += transform.right * moveSpeed;
				moveTime -= moveWait;
			}
		}
		if (mirror)
		{
			Mirror();
		}
		Destroy();
	}
	void Mirror()
	{
		if (!mirrorPossX &&
			EndChecker.EndRight(transform.position.x))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f - transform.localEulerAngles.z);
			mirrorPossX = true;
		}
		else if (mirrorPossX &&
			!EndChecker.EndRight(transform.position.x))
		{
			mirrorPossX = false;
		}
		if (!mirrorPossY &&
			(EndChecker.EndTop(transform.position.y) ||
			EndChecker.EndBottom(transform.position.y)))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, -transform.localEulerAngles.z);
			mirrorPossY = true;
		}
		else if (mirrorPossY &&
			!EndChecker.EndTop(transform.position.y) &&
			!EndChecker.EndBottom(transform.position.y))
		{
			mirrorPossY = false;
		}
	}
	void Destroy()
	{
		if (EndChecker.EndLeft(transform.position.x + destroyWait))
		{
			Destroy(gameObject);
		}
		if (!mirror &&
			(EndChecker.EndRight(transform.position.x - destroyWait) ||
			EndChecker.EndTop(transform.position.y - destroyWait) ||
			EndChecker.EndBottom(transform.position.y + destroyWait)))
		{
			Destroy(gameObject);
		}
	}
}
