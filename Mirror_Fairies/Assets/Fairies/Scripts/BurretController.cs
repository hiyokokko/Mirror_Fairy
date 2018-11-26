using UnityEngine;
public class BurretController : MonoBehaviour
{
	[SerializeField] float moveSpeed;
	[SerializeField] float moveWait;
	[SerializeField] bool moveBarst;
	[SerializeField] bool mirror;
	[SerializeField] float mirrorSpeedChange;
	[SerializeField] float destroyWait;
	[SerializeField] bool aim;
	[SerializeField] bool action;
	[SerializeField] GameObject actionBurretPrefab;
	[SerializeField] bool actionTargetPos;
	[SerializeField] float actionWait;
	[SerializeField] Vector3[] actionInfo;
	public Vector2 targetPos;
	float moveTime = 0.0f;
	float actionTime = 0.0f;
	bool mirrorNowX = false;
	bool mirrorNowY = false;
	void Start ()
	{
		if (aim)
		{
			transform.LookAt(targetPos);
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180 + transform.localEulerAngles.x);
		}
	}
	void Update ()
	{
		Move();
		if (action)
		{
			Action();
		}
		if (mirror)
		{
			Mirror();
		}
		Destroy();
	}
	void Move()
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
	}
	void Action()
	{
		if ((actionTargetPos && transform.position.x <= targetPos.x) || (!actionTargetPos && actionTime >= actionWait))
		{
			foreach (Vector3 info in actionInfo)
			{
				Vector2 burretPos =
					new Vector2
					(
						transform.position.x + info.x,
						transform.position.y + info.y
					);
				Vector3 burretRot = new Vector3(0.0f, 0.0f, info.z);
				Instantiate(actionBurretPrefab, burretPos, Quaternion.Euler(burretRot));
			}
			Destroy(gameObject);
		}
		else if (!actionTargetPos && actionTime < actionWait)
		{
			actionTime += Time.deltaTime;
		}
	}
	void Mirror()
	{
		if (!mirrorNowX &&
			EndChecker.EndRight(transform.position.x))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f - transform.localEulerAngles.z);
			if (mirrorSpeedChange != 0)
			{
				moveSpeed *= mirrorSpeedChange;
			}
			mirrorNowX = true;
		}
		else if (mirrorNowX &&
			!EndChecker.EndRight(transform.position.x))
		{
			mirrorNowX = false;
		}
		if (!mirrorNowY &&
			(EndChecker.EndTop(transform.position.y) ||
			EndChecker.EndBottom(transform.position.y)))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, -transform.localEulerAngles.z);
			if (mirrorSpeedChange != 0)
			{
				moveSpeed *= mirrorSpeedChange;
			}
			mirrorNowY = true;
		}
		else if (mirrorNowY &&
			!EndChecker.EndTop(transform.position.y) &&
			!EndChecker.EndBottom(transform.position.y))
		{
			mirrorNowY = false;
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
