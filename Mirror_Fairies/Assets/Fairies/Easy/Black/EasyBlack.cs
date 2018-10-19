using UnityEngine;
public class EasyBlack : MonoBehaviour
{
	[SerializeField] GameObject burret;
	Camera cam;
	int touchMax;
	float touchBouder;
	Vector2 beforePos;
	Vector2 touchBeforePos;
	int attack;
	float attackWait;
	float attackTime;
	float burretSpeed;
	int move;
	float moveRestRight;
	float moveRestOther;
	void Start()
	{
		cam = GameObject.Find("Camera").GetComponent<Camera>();
		touchMax = TouchOperation.windows ? 1 : 2;
		touchBouder = 12.0f;
		attack = -1;
		attackWait = 0.1f;
		attackTime = attackWait;
		burretSpeed = 16.0f;
		move = -1;
		moveRestRight = 3.0f;
		moveRestOther = 1.0f;
	}
	void Update()
	{
		Touch();
		if (attackTime < attackWait) { attackTime += Time.deltaTime; }
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		AudioSE.kill = true;
		Main.gameOver = true;
		Destroy(gameObject);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		AudioSE.kill = true;
		Main.gameOver = true;
		Destroy(gameObject);
	}
	void Touch()
	{
		int touchMax = TouchOperation.windows ? 1 : Input.touchCount <= 2 ? Input.touchCount : 2;
		for (int touchNum = 0; touchNum < touchMax; touchNum++)
		{
			if (TouchOperation.windows)
			{
				if (Input.GetKey(KeyCode.Space) && attackTime >= attackWait) { Attack(); }
				if (TouchOperation.GetTouch(touchNum) == TouchInfo.Start)
				{
					beforePos = transform.position;
					touchBeforePos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
					move = touchNum;
				}
				if (move != -1)
				{
					Move(TouchOperation.GetTouchWorldPosition(cam, move));
					if (TouchOperation.GetTouch(move) == TouchInfo.End)
					{
						move = -1;
					}
				}
			}
			else
			{
				int fingerId = Input.GetTouch(touchNum).fingerId;
				if (TouchOperation.GetTouch(touchNum) == TouchInfo.Start)
				{
					if (TouchOperation.GetTouchWorldPosition(cam, touchNum).x >= touchBouder && attack == -1)
					{
						attack = fingerId;
					}
					if (TouchOperation.GetTouchWorldPosition(cam, touchNum).x < touchBouder && move == -1)
					{
						beforePos = transform.position;
						touchBeforePos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
						move = fingerId;
					}
				}
				if (attack == fingerId)
				{
					if (attackTime >= attackWait) { Attack(); }
					if (TouchOperation.GetTouch(touchNum) == TouchInfo.End)
					{
						attack = -1;
					}
				}
				if (move == fingerId)
				{
					Move(TouchOperation.GetTouchWorldPosition(cam, touchNum));
					if (TouchOperation.GetTouch(touchNum) == TouchInfo.End)
					{
						move = -1;
					}
				}
			}
		}
	}
	void Attack()
	{
		Vector2 burretPos = new Vector2(transform.position.x + 1, transform.position.y);
		GameObject burretInst = Instantiate(burret, burretPos, Quaternion.identity);
		burretInst.GetComponent<EasyBlackBurret>().speed = burretSpeed;
		attackTime -= attackWait;
	}
	void Move(Vector2 touchAfterPos)
	{
		Vector2 afterPos = beforePos + (touchAfterPos - touchBeforePos);
		transform.position = afterPos;
		if (EndChecker.EndRight(transform.position.x + moveRestRight))
		{
			transform.position = new Vector2(EndChecker.endRight - moveRestRight, transform.position.y);
		}
		else if (EndChecker.EndLeft(transform.position.x - moveRestOther))
		{
			transform.position = new Vector2(EndChecker.endLeft + moveRestOther, transform.position.y);
		}
		if (EndChecker.EndTop(transform.position.y + moveRestOther))
		{
			transform.position = new Vector2(transform.position.x, EndChecker.endTop - moveRestOther);
		}
		else if (EndChecker.EndBottom(transform.position.y - moveRestOther))
		{
			transform.position = new Vector2(transform.position.x, EndChecker.endBottom + moveRestOther);
		}
	}
}
