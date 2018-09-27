using UnityEngine;
public class EasyBlack : MonoBehaviour
{
	Camera cam;
	int touchMax;
	float touchBouder;
	Vector2 beforePos;
	[SerializeField] GameObject burret;
	int attack;
	float shotWait;
	float shotTime;
	float burretSpeed;
	int move;
	float moveRestRight;
	float moveRestOther;
	void Start()
	{
		cam = GameObject.Find("Camera").GetComponent<Camera>();
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
		if (Input.GetKey(KeyCode.Space)) { Attack(); }
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
		for (int touchNum = 0; touchNum < Input.touchCount || touchNum < touchMax; touchNum++)
		{
			if (TouchOperation.GetTouch(touchNum) == TouchInfo.Began)
			{
				if (TouchOperation.GetTouchWorldPosition(cam, touchNum).x >= touchBouder && attack == -1)
				{
					attack = touchNum;
				}
				if (TouchOperation.GetTouchWorldPosition(cam, touchNum).x < touchBouder && move == -1)
				{
					beforePos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
					move = touchNum;
				}
			}
		}
		if (attack != -1)
		{
			Attack();
			if (TouchOperation.GetTouch(attack) == TouchInfo.Ended || TouchOperation.GetTouch(attack) == TouchInfo.Canceled)
			{
				attack = -1;
			}
		}
		if (move != -1)
		{
			Move(TouchOperation.GetTouchWorldPosition(cam, move));
			if (TouchOperation.GetTouch(move) == TouchInfo.Ended || TouchOperation.GetTouch(move) == TouchInfo.Canceled)
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
	void Move(Vector2 afterPos)
	{
		Vector2 targetPos = (afterPos - beforePos) * Time.deltaTime;
		if (EndChecker.EndRight(transform.position.x + targetPos.x + moveRestRight)) { targetPos.x = 0.0f; }
		if (EndChecker.EndLeft(transform.position.x + targetPos.x - moveRestOther)) { targetPos.x = 0.0f; }
		if (EndChecker.EndTop(transform.position.y + targetPos.y + moveRestOther)) { targetPos.y = 0.0f; }
		if (EndChecker.EndBottom(transform.position.y + targetPos.y - moveRestOther)) { targetPos.y = 0.0f; }
		transform.Translate(targetPos);
	}
}
