using UnityEngine;
public class EasyBlack : MonoBehaviour
{
	[SerializeField] GameObject burret;
	GameObject burretInst;
	float moveRestRight;
	float moveRestOther;
	float speed;
	float shotWait;
	float shotTime;
	Vector2 burretPos;
	float burretSpeed;
	void Start()
	{
		moveRestRight = 3.0f;
		moveRestOther = 1.0f;
		speed = 5.0f;
		shotWait = 0.1f;
		shotTime = shotWait;
		burretSpeed = 16.0f;
	}
	void Update()
	{
		Shot();
		Move();
	}
	void Shot()
	{
		if (Input.GetKey(KeyCode.Return) && shotTime >= shotWait)
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
	void Move()
	{
		if (Input.GetKey(KeyCode.D) && !EndChecker.EndRight(transform.position.x + moveRestRight))
		{
			transform.position += transform.right * Time.deltaTime * speed;
		}
		else if (Input.GetKey(KeyCode.A) && !EndChecker.EndLeft(transform.position.x - moveRestOther))
		{
			transform.position -= transform.right * Time.deltaTime * speed;
		}
		if (Input.GetKey(KeyCode.W) && !EndChecker.EndTop(transform.position.y + moveRestOther))
		{
			transform.position += transform.up * Time.deltaTime * speed;
		}
		else if (Input.GetKey(KeyCode.S) && !EndChecker.EndBottom(transform.position.y - moveRestOther))
		{
			transform.position -= transform.up * Time.deltaTime * speed;
		}
	}
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
