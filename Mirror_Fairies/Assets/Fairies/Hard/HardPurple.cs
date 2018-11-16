using UnityEngine;
using UnityEngine.UI;
public class HardPurple : MonoBehaviour
{
	[SerializeField] GameObject[] burret;
	[SerializeField] Text healthText;
	Vector2 pos;
	int health;
	float time;
	float speed;
	float move;
	int attackCount;
	float attackWait;
	float attackTime;
	Vector2[][] burretPos;
	float[][] burretRot;
	void Start()
	{
		pos = transform.position;
		health = 800;
		time = 0.0f;
		speed = 1.0f;
		move = 4.0f;
		attackCount = 0;
		attackWait = 1.0f;
		attackTime = attackWait;
		burretPos = new Vector2[2][]
		{
			new Vector2[16]
			{
				new Vector2(pos.x - 20, pos.y + 8),
				new Vector2(pos.x - 18, pos.y + 8),
				new Vector2(pos.x - 16, pos.y + 8),
				new Vector2(pos.x - 14, pos.y + 8),
				new Vector2(pos.x - 12, pos.y + 8),
				new Vector2(pos.x - 10, pos.y + 8),
				new Vector2(pos.x - 8, pos.y + 8),
				new Vector2(pos.x - 6, pos.y + 8),
				new Vector2(pos.x - 20, pos.y - 8),
				new Vector2(pos.x - 18, pos.y - 8),
				new Vector2(pos.x - 16, pos.y - 8),
				new Vector2(pos.x - 14, pos.y - 8),
				new Vector2(pos.x - 12, pos.y - 8),
				new Vector2(pos.x - 10, pos.y - 8),
				new Vector2(pos.x - 8, pos.y - 8),
				new Vector2(pos.x - 6, pos.y - 8)
			},
			new Vector2[2]
			{
				new Vector2(pos.x + 1, pos.y + 4),
				new Vector2(pos.x + 1, pos.y - 4)
			}
		};
		burretRot = new float[2][]
		{
			new float[2]
			{
				-45.0f,
				-135.0f
			},
			new float[2]
			{
				45.0f,
				135.0f
			}
		};
	}
	void Update()
	{
		time += Time.deltaTime;
		if (attackTime >= attackWait) { Attack(attackCount % 2); }
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
				Main.kill = 6;
				Main.killTime = Main.time;
				Main.gameOver = true;
				Destroy(gameObject);
			}
			AudioSE.damage = true;
			healthText.text = health.ToString();
		}
	}
	void Attack(int pattern)
	{
		float burretSpeed;
		GameObject burretInst;
		switch (pattern)
		{
			case 0:
				burretSpeed = 2.0f;
				for (int i = 0; i < burretPos[pattern].Length; i++)
				{
					for (int j = 0; j < 2; j++)
					{
						burretInst = Instantiate(burret[pattern], burretPos[pattern][i], Quaternion.Euler(0.0f, 0.0f, burretRot[i / 8][j]));
						burretInst.GetComponent<HardPurpleBurret1>().speed = burretSpeed;
					}
				}
				break;
			case 1:
				burretSpeed = 32.0f;
				Vector2 burretTarget;
				try
				{
					burretTarget = GameObject.Find("HardBlack(Clone)").transform.position;
				}
				catch (System.Exception e)
				{
					Debug.Log(e.Message);
					burretTarget = new Vector2(0.0f, 0.0f);
				}
				burretInst = Instantiate(burret[pattern], burretPos[pattern][attackCount % 4 / 2], Quaternion.identity);
				burretInst.GetComponent<HardPurpleBurret2>().target = burretTarget;
				burretInst.GetComponent<HardPurpleBurret2>().speed = burretSpeed;
				break;
		}
		attackCount++;
		attackTime -= attackWait;
	}
}
