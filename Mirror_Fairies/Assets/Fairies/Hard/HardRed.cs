using UnityEngine;
using UnityEngine.UI;
public class HardRed : MonoBehaviour
{
	[SerializeField] GameObject[] burret;
	[SerializeField] Text healthText;
	Vector2 pos;
	int health;
	float time;
	float speed;
	float move;
	float attackWait;
	float attackTime;
	int attackCount;
	int burretNum;
	Vector2[][] burretPos1;
	float burretRot1;
	float burretSpeed1;
	float[] burretRot2;
	float burretSpeed2;
	float burretWait2;
	void Start()
	{
		pos = transform.position;
		health = 400;
		time = 0.0f;
		speed = 1.0f;
		move = 6.0f;
		attackWait = 1.0f / 3;
		attackTime = attackWait;
		attackCount = 0;
		burretNum = 8;
		burretPos1 = new Vector2[12][]
		{
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 4),
				new Vector2(pos.x, pos.y + 2),
				new Vector2(pos.x, pos.y - 2),
				new Vector2(pos.x, pos.y - 4),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 4),
				new Vector2(pos.x, pos.y),
				new Vector2(pos.x, pos.y - 2),
				new Vector2(pos.x, pos.y - 4),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 2),
				new Vector2(pos.x, pos.y),
				new Vector2(pos.x, pos.y - 2),
				new Vector2(pos.x, pos.y - 4),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 4),
				new Vector2(pos.x, pos.y + 2),
				new Vector2(pos.x, pos.y),
				new Vector2(pos.x, pos.y - 2),
				new Vector2(pos.x, pos.y - 4),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 2),
				new Vector2(pos.x, pos.y),
				new Vector2(pos.x, pos.y - 2),
				new Vector2(pos.x, pos.y - 4),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 4),
				new Vector2(pos.x, pos.y),
				new Vector2(pos.x, pos.y - 2),
				new Vector2(pos.x, pos.y - 4),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 4),
				new Vector2(pos.x, pos.y + 2),
				new Vector2(pos.x, pos.y - 2),
				new Vector2(pos.x, pos.y - 4),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 4),
				new Vector2(pos.x, pos.y + 2),
				new Vector2(pos.x, pos.y),
				new Vector2(pos.x, pos.y - 4),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 4),
				new Vector2(pos.x, pos.y + 2),
				new Vector2(pos.x, pos.y),
				new Vector2(pos.x, pos.y - 2),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 4),
				new Vector2(pos.x, pos.y + 2),
				new Vector2(pos.x, pos.y),
				new Vector2(pos.x, pos.y - 2),
				new Vector2(pos.x, pos.y - 4),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 4),
				new Vector2(pos.x, pos.y + 2),
				new Vector2(pos.x, pos.y),
				new Vector2(pos.x, pos.y - 2),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
			new Vector2[8]
			{
				new Vector2(pos.x, pos.y + 8),
				new Vector2(pos.x, pos.y + 6),
				new Vector2(pos.x, pos.y + 4),
				new Vector2(pos.x, pos.y + 2),
				new Vector2(pos.x, pos.y),
				new Vector2(pos.x, pos.y - 4),
				new Vector2(pos.x, pos.y - 6),
				new Vector2(pos.x, pos.y - 8)
			},
		};
		burretRot1 = -180.0f;
		burretSpeed1 = 8.0f;
		burretRot2 = new float[2]
		{
			-90.0f,
			90.0f
		};
		burretSpeed2 = 32.0f;
		burretWait2 = 1.0f;
	}
	void Update()
	{
		time += Time.deltaTime;
		if (attackTime >= attackWait) { Attack(attackCount % (burretPos1.Length / 4) != 0 ? 0 : 1); }
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
				Main.kill = 5;
				Main.killTime = Main.time;
				Main.enemySpawn = true;
				Destroy(gameObject);
			}
			AudioSE.damage = true;
			healthText.text = health.ToString();
		}
	}
	void Attack(int pattern)
	{
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret[0], burretPos1[attackCount % burretPos1.Length][i], Quaternion.Euler(0.0f, 0.0f, burretRot1));
		}
		if (pattern == 1)
		{
			Vector2[] burretPos2;
			try
			{
				Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
				burretPos2 = new Vector2[8]
				{
					new Vector2(playerPos.x + 1.5f, pos.y + 9),
					new Vector2(playerPos.x + 0.5f, pos.y + 9),
					new Vector2(playerPos.x - 0.5f, pos.y + 9),
					new Vector2(playerPos.x - 1.5f, pos.y + 9),
					new Vector2(playerPos.x + 1.5f, pos.y - 9),
					new Vector2(playerPos.x + 0.5f, pos.y - 9),
					new Vector2(playerPos.x - 0.5f, pos.y - 9),
					new Vector2(playerPos.x - 1.5f, pos.y - 9)
				};
			}
			catch (System.Exception e)
			{
				Debug.Log(e.Message);
				burretPos2 = new Vector2[8]
				{
					new Vector2(1.5f, pos.y + 9),
					new Vector2(0.5f, pos.y + 9),
					new Vector2(-0.5f, pos.y + 9),
					new Vector2(-1.5f, pos.y + 9),
					new Vector2(1.5f, pos.y - 9),
					new Vector2(0.5f, pos.y - 9),
					new Vector2(-0.5f, pos.y - 9),
					new Vector2(-1.5f, pos.y - 9)
				};
			}
			for (int i = 0; i < burretPos2.Length; i++)
			{
				GameObject burretInst = Instantiate(burret[1], burretPos2[i], Quaternion.Euler(0.0f, 0.0f, burretRot2[i / (burretPos2.Length / burretRot2.Length)]));
			}
		}
		attackCount++;
		attackTime -= attackWait;
	}
}
