using UnityEngine;
public class EasyBlack : MonoBehaviour
{
	Camera cam;
	int mouseUse;
	int touchMax;
	float touchBouder;
	Vector2 beforePos;
	Vector2 touchBeforePos;
	[SerializeField] GameObject burret;
	int attack;
	float shotWait;
	float shotTime;
	float burretSpeed;
	int move;
	float moveRestRight;
	float moveRestOther;
	float moveSpeed;
	void Start()
	{
		cam = GameObject.Find("Camera").GetComponent<Camera>();
		if (TouchOperation.mouseUse) { mouseUse = 1; };
		if (!TouchOperation.mouseUse) { mouseUse = 0; };
		touchMax = 2;
		touchBouder = 12.0f;
		attack = -1;
		shotWait = 0.1f;
		shotTime = shotWait;
		burretSpeed = 16.0f;
		move = -1;
		moveRestRight = 3.0f;
		moveRestOther = 1.0f;
	}
	void Update()
	{
		Touch();
		if (shotTime < shotWait) { shotTime += Time.deltaTime; }
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		MainManager.gameOver = true;
		Destroy(gameObject);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		MainManager.gameOver = true;
		Destroy(gameObject);
	}
	void Touch()
	{
		for (int touchNum = 0; touchNum < Input.touchCount && touchNum < touchMax || touchNum < mouseUse; touchNum++)
		{
			if (TouchOperation.GetTouch(touchNum) == TouchInfo.Start)
			{
				if (TouchOperation.GetTouchWorldPosition(cam, touchNum).x >= touchBouder && attack == -1)
				{
					attack = touchNum;
				}
				if (TouchOperation.GetTouchWorldPosition(cam, touchNum).x < touchBouder && move == -1)
				{
					beforePos = transform.position;
					touchBeforePos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
					move = touchNum;
				}
			}
		}
		if (attack != -1 || Input.GetKey(KeyCode.Space))
		{
			Attack();
			if (TouchOperation.GetTouch(attack) == TouchInfo.End)
			{
				attack = -1;
			}
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
	void Attack()
	{
		if (shotTime >= shotWait)
		{
			Vector2 burretPos = new Vector2(transform.position.x + 1, transform.position.y);
			GameObject burretInst = Instantiate(burret, burretPos, Quaternion.identity);
			burretInst.GetComponent<EasyBlackBurret>().speed = burretSpeed;
			shotTime -= shotWait;
		}
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
