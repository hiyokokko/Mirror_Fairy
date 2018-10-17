using UnityEngine;
using UnityEngine.UI;
public class NormalYellow : MonoBehaviour
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
	int attackCount;
	Vector2[] burretPos;
	float burretSpeed;
	void Start()
	{
		pos = transform.position;
		health = 200;
		time = 0.0f;
		speed = 1.0f;
		move = 4.0f;
		attackWait = 0.5f;
		attackTime = attackWait;
		attackCount = 0;
		burretPos = new Vector2[8]
		{
			new Vector2(pos.x, pos.y),
			new Vector2(pos.x, pos.y + 2),
			new Vector2(pos.x, pos.y + 4),
			new Vector2(pos.x, pos.y + 2),
			new Vector2(pos.x, pos.y),
			new Vector2(pos.x, pos.y - 2),
			new Vector2(pos.x, pos.y - 4),
			new Vector2(pos.x, pos.y - 2)
		};
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
				Main.kill = 4;
				Main.killTime = Main.time;
				Main.enemySpawn = true;
				Destroy(gameObject);
			}
			healthText.text = health.ToString();
		}
	}
	void Attack()
	{
		GameObject burretInst = Instantiate(burret, burretPos[attackCount % burretPos.Length], Quaternion.Euler(0.0f, 0.0f, -180.0f));
		burretInst.GetComponent<NormalYellowBurret>().speed = burretSpeed;
		attackCount++;
		attackTime -= attackWait;
	}
}
