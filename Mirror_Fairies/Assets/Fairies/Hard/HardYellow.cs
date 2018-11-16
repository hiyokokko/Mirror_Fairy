using UnityEngine;
using UnityEngine.UI;
public class HardYellow : MonoBehaviour
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
	Vector2[] burretPos;
	float burretRot;
	float burretSpeed;
	void Start()
	{
		pos = transform.position;
		health = 400;
		time = 0.0f;
		speed = 1.0f;
		move = 4.0f;
		attackWait = 2.0f;
		attackTime = attackWait;
		burretPos = new Vector2[2]
		{
			new Vector2(pos.x, pos.y + 2),
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
		if (col.gameObject.transform.tag == "PlayerBurret" && GameObject.FindGameObjectWithTag("Player") != null)
		{
			health--;
			Destroy(col.gameObject);
			if (health <= 0)
			{
				AudioSE.kill = true;
				Main.kill = 4;
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
		Vector2[] burretTarget;
		try
		{
			Vector2 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
			burretTarget = new Vector2[2]
			{
				new Vector2(playerPos.x, playerPos.y + 2.0f),
				new Vector2(playerPos.x, playerPos.y - 2.0f)
			};
		}
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
			burretTarget = new Vector2[2]
			{
				new Vector2(0.0f, 2.0f),
				new Vector2(0.0f, -2.0f)
			};
		}
		for (int i = 0; i < burretPos.Length; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos[i], Quaternion.identity);
			burretInst.GetComponent<HardYellowBurret1>().target = burretTarget[i];
			burretInst.GetComponent<HardYellowBurret1>().speed = burretSpeed;
			burretInst.GetComponent<HardYellowBurret1>().attackPattern = i;
		}
		attackTime -= attackWait;
	}
}
