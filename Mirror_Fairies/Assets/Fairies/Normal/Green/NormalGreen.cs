using UnityEngine;
using UnityEngine.UI;
public class NormalGreen : MonoBehaviour
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
	int burretNum;
	Vector2[][] burretPos;
	float burretSpeed;
	void Start()
	{
		pos = transform.position;
		health = 200;
		time = 0.0f;
		speed = 1.0f;
		move = 4.0f;
		attackWait = 1.0f;
		attackTime = attackWait;
		attackCount = 0;
		burretNum = 5;
		burretPos = new Vector2[4][]
		{
			new Vector2[5]
			{
				new Vector2(pos.x + 1, pos.y + 2),
				new Vector2(pos.x + 1, pos.y + 1),
				new Vector2(pos.x + 1, pos.y),
				new Vector2(pos.x + 1, pos.y - 1),
				new Vector2(pos.x + 1, pos.y - 2)
			},
			new Vector2[5]
			{
				new Vector2(pos.x + 1, pos.y + 6),
				new Vector2(pos.x + 1, pos.y + 5),
				new Vector2(pos.x + 1, pos.y + 4),
				new Vector2(pos.x + 1, pos.y + 3),
				new Vector2(pos.x + 1, pos.y + 2)
			},
			new Vector2[5]
			{
				new Vector2(pos.x + 1, pos.y + 2),
				new Vector2(pos.x + 1, pos.y + 1),
				new Vector2(pos.x + 1, pos.y),
				new Vector2(pos.x + 1, pos.y - 1),
				new Vector2(pos.x + 1, pos.y - 2)
			},
			new Vector2[5]
			{
				new Vector2(pos.x + 1, pos.y - 2),
				new Vector2(pos.x + 1, pos.y - 3),
				new Vector2(pos.x + 1, pos.y - 4),
				new Vector2(pos.x + 1, pos.y - 5),
				new Vector2(pos.x + 1, pos.y - 6)
			}
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
				Main.kill = 3;
				Main.killTime = Main.time;
				Main.enemySpawn = true;
				Destroy(gameObject);
			}
			healthText.text = health.ToString();
		}
	}
	void Attack()
	{
		Vector2[] burretTarget = new Vector2[5];
		try
		{
			Vector2 target = GameObject.Find("NormalBlack(Clone)").transform.position;
			for (int i = 0; i < burretNum; i++)
			{
				burretTarget[i] = target;
				burretTarget[i].y += i - 2.0f;
			}
		}
		catch (System.NullReferenceException e)
		{
			Debug.Log("ERROR:" + e);
			for (int i = 0; i < burretNum; i++)
			{
				burretTarget[i] = new Vector2(0.0f, 0.0f);
			}
		}
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos[attackCount % burretPos.Length][i], Quaternion.identity);
			burretInst.GetComponent<NormalGreenBurret>().target = burretTarget[i];
			burretInst.GetComponent<NormalGreenBurret>().speed = burretSpeed;
		}
		attackCount++;
		attackTime -= attackWait;
	}
}
