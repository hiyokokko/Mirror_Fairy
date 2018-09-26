using UnityEngine;
public class EasyBlack : MonoBehaviour
{
	[SerializeField] GameObject burret;
	Camera cam;
	int attack;
	int move;
	GameObject burretInst;
	float moveRestRight;
	float moveRestOther;
	float speed;
	float shotWait;
	float shotTime;
	Vector2 burretPos;
	float burretSpeed;
	
	Vector2 cursorPos;
	Vector2 clickPos;
	Vector2 beforePos;
	Vector2 targetPos;
	Vector2 movePos;
	void Start()
	{
		cam = GameObject.Find("Camera").GetComponent<Camera>();
		attack = -1;
		move = -1;
		moveRestRight = 3.0f;
		moveRestOther = 1.0f;
		speed = 2.0f;
		shotWait = 0.1f;
		shotTime = shotWait;
		burretSpeed = 16.0f;
	}
	void Update()
	{
		Touch();
	}
	void Attack()
	{
		if (shotTime >= shotWait)
		{
			burretPos = new Vector2(transform.position.x + 1, transform.position.y);
			burretInst = Instantiate(burret, burretPos, Quaternion.identity);
			burretInst.GetComponent<EasyBlackBurret>().speed = burretSpeed;
			shotTime -= shotWait;
		}
		if (shotTime < shotWait)
		{
			shotTime += Time.deltaTime;
		}
	}
	void Move(Vector2 nowPos)
	{
		targetPos = nowPos - beforePos;
		transform.Translate(targetPos * Time.deltaTime * speed);
	}
	void Touch()
	{
		int maxTouch = 2;
		for (int i = 0; i < Input.touchCount || i < maxTouch; i++)
		{
			if (TouchOperation.GetTouch(i) == TouchInfo.Began)
			{
				if (TouchOperation.GetTouchWorldPosition(cam, i).x >= 12.0f && attack == -1)
				{
					attack = i;
				}
				else if (TouchOperation.GetTouchWorldPosition(cam, i).x < 12.0f && move == -1)
				{
					beforePos = TouchOperation.GetTouchWorldPosition(cam, i);
					move = i;
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
			Move((Vector2)TouchOperation.GetTouchWorldPosition(cam, move));
			if (TouchOperation.GetTouch(move) == TouchInfo.Ended || TouchOperation.GetTouch(move) == TouchInfo.Canceled)
			{
				move = -1;
			}
		}
	}
	//void Move()
	//{
	//	if (Input.GetKey(KeyCode.D) && !EndChecker.EndRight(transform.position.x + moveRestRight))
	//	{
	//		transform.position += transform.right * Time.deltaTime * speed;
	//	}
	//	else if (Input.GetKey(KeyCode.A) && !EndChecker.EndLeft(transform.position.x - moveRestOther))
	//	{
	//		transform.position -= transform.right * Time.deltaTime * speed;
	//	}
	//	if (Input.GetKey(KeyCode.W) && !EndChecker.EndTop(transform.position.y + moveRestOther))
	//	{
	//		transform.position += transform.up * Time.deltaTime * speed;
	//	}
	//	else if (Input.GetKey(KeyCode.S) && !EndChecker.EndBottom(transform.position.y - moveRestOther))
	//	{
	//		transform.position -= transform.up * Time.deltaTime * speed;
	//	}
	//}
	void OnTriggerEnter2D(Collider2D col)
	{
		MainManager.gameOver = true;
		Destroy(gameObject);
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		MainManager.gameOver = true;
		Destroy(gameObject);
	}
}
