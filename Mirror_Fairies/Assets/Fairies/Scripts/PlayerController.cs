using UnityEngine;
public class PlayerController : MonoBehaviour
{
	[SerializeField] GameObject burretPrefab;
	[SerializeField] float attackWait;
	[SerializeField] Vector3[] attackInfo;
	Camera cam;
	PlayerTouchState playerTouchState;
	Vector2 nowPos;
	float attackTime;
	float moveRestRight = 3.0f;
	float moveRestOther = 1.0f;
	float touchBouder = 12.0f;
	void Start()
	{
		cam = GameObject.Find("Camera").GetComponent<Camera>();
		playerTouchState = new PlayerTouchState();
		nowPos = transform.position;
		attackTime = attackWait;
	}
	void Update()
	{
		nowPos = transform.position;
		PlayerOperation();
		if (attackTime < attackWait) { attackTime += Time.deltaTime; }
		if (playerTouchState.attack != -1 && attackTime >= attackWait) { Attack(); }
		if (playerTouchState.move != -1) { Move(); }
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
	void Attack()
	{
		for (int info = 0; info < attackInfo.Length; info++)
		{
			Vector2 burretPos = new Vector2(attackInfo[info].x + nowPos.x, attackInfo[info].y + nowPos.y);
			Vector3 burretRot = new Vector3(0.0f, 0.0f, attackInfo[info].z);
			Instantiate(burretPrefab, burretPos, Quaternion.Euler(burretRot));
		}
		attackTime -= attackWait;
	}
	void Move()
	{
		Vector2 afterPos = playerTouchState.beforePos + (playerTouchState.afterTouchPos - playerTouchState.beforeTouchPos);
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
	public void PlayerOperation()
	{
		int touchMax = TouchOperation.windows ? 1 : Input.touchCount <= 2 ? Input.touchCount : 2;
		for (int touchNum = 0; touchNum < touchMax; touchNum++)
		{
			if (TouchOperation.windows)
			{
				if (Input.GetKeyDown(KeyCode.Space)) { playerTouchState.attack = 0; }
				else if (Input.GetKeyUp(KeyCode.Space)) { playerTouchState.attack = -1; }
				if (TouchOperation.GetTouch(0) == TouchInfo.Start)
				{
					playerTouchState.beforePos = nowPos;
					playerTouchState.beforeTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
					playerTouchState.afterTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
					playerTouchState.move = 0;
				}
				else if (TouchOperation.GetTouch(0) == TouchInfo.Now)
				{
					playerTouchState.afterTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
				}
				else if (TouchOperation.GetTouch(0) == TouchInfo.End)
				{
					playerTouchState.move = -1;
				}
			}
			else
			{
				int fingerId = Input.GetTouch(touchNum).fingerId;
				if (TouchOperation.GetTouch(touchNum) == TouchInfo.Start)
				{
					if (TouchOperation.GetTouchWorldPosition(cam, touchNum).x >= touchBouder && playerTouchState.attack == -1)
					{
						playerTouchState.attack = fingerId;
					}
					if (TouchOperation.GetTouchWorldPosition(cam, touchNum).x < touchBouder && playerTouchState.move == -1)
					{
						playerTouchState.beforePos = nowPos;
						playerTouchState.beforeTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
						playerTouchState.afterTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
						playerTouchState.move = fingerId;
					}
				}
				else if (playerTouchState.move == fingerId && TouchOperation.GetTouch(touchNum) == TouchInfo.Now)
				{
					playerTouchState.afterTouchPos = TouchOperation.GetTouchWorldPosition(cam, touchNum);
				}
				else if (TouchOperation.GetTouch(touchNum) == TouchInfo.End)
				{
					if (playerTouchState.attack == fingerId) { playerTouchState.attack = -1; }
					else if (playerTouchState.move == fingerId) { playerTouchState.move = -1; }
				}
			}
		}
	}
}
public class PlayerTouchState
{
	public int attack = -1;
	public int move = -1;
	public Vector2 beforePos;
	public Vector2 beforeTouchPos;
	public Vector2 afterTouchPos;
}
