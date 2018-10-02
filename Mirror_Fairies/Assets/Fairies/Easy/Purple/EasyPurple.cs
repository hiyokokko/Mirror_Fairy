using UnityEngine;
using UnityEngine.UI;
public class EasyPurple : MonoBehaviour
{
	[SerializeField] GameObject burret;
	[SerializeField] Text healthText;
	Vector2 pos;
	int health;
	float time;
	float speed;
	float move;
	float attackWait;
	float attackTime;
	int burretNum;
	float burretRot;
	float burretSpeed;
	void Start()
	{
		pos = transform.position;
		health = 100;
		time = 0.0f;
		speed = 1.0f;
		move = 2.0f;
		attackWait = 4.0f;
		attackTime = attackWait;
		burretNum = 6;
		burretRot = 180.0f;
		burretSpeed = 16.0f;
	}
	void Update()
	{
		time += Time.deltaTime;
		if (attackTime >= attackWait) { Attack(); }
		if (attackTime < attackWait) { attackTime += Time.deltaTime; }
		transform.position = new Vector2(pos.x, pos.y + Mathf.Sin(time * Mathf.PI / 2 * speed) * move);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.transform.tag == "Player")
		{
			health--;
			Destroy(col.gameObject);
			if (health <= 0)
			{
				MainManager.kill++;
				MainManager.gameOver = true;
				Destroy(gameObject);
			}
			healthText.text = health.ToString();
		}
	}
	void Attack()
	{
		Vector2[] burretPos = new Vector2[6]
		{
			new Vector2(transform.position.x, transform.position.y + 1),
			new Vector2(transform.position.x, transform.position.y - 1),
			new Vector2(transform.position.x + 2, transform.position.y + 3),
			new Vector2(transform.position.x + 2, transform.position.y - 3),
			new Vector2(transform.position.x + 4, transform.position.y + 5),
			new Vector2(transform.position.x + 4, transform.position.y - 5)
		};
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos[i], Quaternion.Euler(0.0f, 0.0f, burretRot));
			burretInst.GetComponent<EasyPurpleBurret>().speed = burretSpeed;
		}
		attackTime -= attackWait;
	}
}
