using UnityEngine;
using UnityEngine.UI;
public class HardGreen : MonoBehaviour
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
	Vector2[][] burretPos;
	float burretRot;
	float burretSpeed;
	float burretMoveWait = 0.5f;
	void Start()
	{
		pos = transform.position;
		health = 400;
		time = 0.0f;
		speed = 1.0f;
		move = 4.0f;
		attackWait = 0.5f;
		attackTime = attackWait;
		attackCount = 0;
		burretPos = new Vector2[8][]
		{
			new Vector2[1]
			{
				new Vector2(pos.x - 2, pos.y)
			},
			new Vector2[2]
			{
				new Vector2(pos.x - 2, pos.y + 2),
				new Vector2(pos.x - 2, pos.y - 2)
			},
			new Vector2[2]
			{
				new Vector2(pos.x - 2, pos.y + 4),
				new Vector2(pos.x - 2, pos.y - 4)
			},
			new Vector2[2]
			{
				new Vector2(pos.x - 2, pos.y + 6),
				new Vector2(pos.x - 2, pos.y - 6)
			},
			new Vector2[2]
			{
				new Vector2(pos.x - 2, pos.y + 8),
				new Vector2(pos.x - 2, pos.y - 8)
			},
			new Vector2[2]
			{
				new Vector2(pos.x - 2, pos.y + 6),
				new Vector2(pos.x - 2, pos.y - 6)
			},
			new Vector2[2]
			{
				new Vector2(pos.x - 2, pos.y + 4),
				new Vector2(pos.x - 2, pos.y - 4)
			},
			new Vector2[2]
			{
				new Vector2(pos.x - 2, pos.y + 2),
				new Vector2(pos.x - 2, pos.y - 2)
			}
		};
		burretRot = -180.0f;
		burretSpeed = 2.0f;
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
		if (col.gameObject.transform.tag == "PlayerBurret" && GameObject.FindGameObjectWithTag("Player") != null)
		{
			health--;
			Destroy(col.gameObject);
			if (health <= 0)
			{
				AudioSE.kill = true;
				Main.kill = 3;
				Main.killTime = Main.time;
				Main.enemySpawn = true;
				Destroy(gameObject);
			}
			AudioSE.damage = true;
			healthText.text = health.ToString();
		}
	}
	void Attack()
	{
		for (int i = 0; i < burretPos[attackCount % burretPos.Length].Length; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos[attackCount % burretPos.Length][i], Quaternion.Euler(0.0f, 0.0f, burretRot));
			burretInst.GetComponent<HardGreenBurret>().speed = burretSpeed;
			burretInst.GetComponent<HardGreenBurret>().moveWait = burretMoveWait;
		}
		attackCount++;
		attackTime -= attackWait;
	}
}
