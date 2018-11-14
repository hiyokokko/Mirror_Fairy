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
	Vector2[][] burretPos;
	float burretRot;
	float burretSpeed1;
	float burretSpeed2;
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
		burretPos = new Vector2[12][]
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
		burretRot = -180.0f;
		burretSpeed1 = 8.0f;
		burretSpeed2 = 32.0f;

	}
	void Update()
	{
		time += Time.deltaTime;
		if (attackTime >= attackWait) { Attack(attackCount % burretPos.Length != 0 ? 0 : 1); }
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
			GameObject burretInst = Instantiate(burret[0], burretPos[attackCount % burretPos.Length][i], Quaternion.Euler(0.0f, 0.0f, burretRot));
			burretInst.GetComponent<HardRedBurret>().speed = burretSpeed1;
		}
		if (pattern == 1)
		{
			Vector2 burretPos2;
			try
			{
				burretPos2 = new Vector2(pos.x + 4, GameObject.Find("HardBlack(Clone)").transform.position.y);
			}
			catch (System.Exception e)
			{
				Debug.Log(e.Message);
				burretPos2 = new Vector2(pos.x + 4, pos.y);
			}
			GameObject burretInst = Instantiate(burret[1], burretPos2, Quaternion.Euler(0.0f, 0.0f, burretRot));
			burretInst.GetComponent<HardRedBurret2>().speed = burretSpeed2;
		}
		attackCount++;
		attackTime -= attackWait;
	}
}
