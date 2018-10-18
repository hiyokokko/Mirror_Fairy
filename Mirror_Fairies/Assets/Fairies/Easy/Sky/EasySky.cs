using System;
using UnityEngine;
using UnityEngine.UI;
public class EasySky : MonoBehaviour
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
		health = 50;
		time = 0.0f;
		speed = 1.0f;
		move = 2.0f;
		attackWait = 1.0f;
		attackTime = attackWait;
		attackCount = 0;
		burretNum = 2;
		burretPos = new Vector2[4][]
		{
			new Vector2[2]
			{
				new Vector2(pos.x + 1, pos.y + 1),
				new Vector2(pos.x + 1, pos.y - 1),
			},
			new Vector2[2]
			{
				new Vector2(pos.x + 1, pos.y + 3),
				new Vector2(pos.x + 1, pos.y + 1),
			},
			new Vector2[2]
			{
				new Vector2(pos.x + 1, pos.y + 1),
				new Vector2(pos.x + 1, pos.y - 1),
			},
			new Vector2[2]
			{
				new Vector2(pos.x + 1, pos.y - 1),
				new Vector2(pos.x + 1, pos.y - 3),
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
		if (col.gameObject.transform.tag == "Player" && GameObject.Find("EasyBlack(Clone)") != null)
		{
			health--;
			Destroy(col.gameObject);
			if (health <= 0)
			{
				AudioSE.kill = true;
				Main.kill = 2;
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
		Vector2 burretTarget;
		try
		{
			burretTarget = GameObject.Find("EasyBlack(Clone)").transform.position;
		}
		catch (NullReferenceException e)
		{
			Debug.Log("ERROR:" + e);
			burretTarget = new Vector2(0.0f, 0.0f);
		}
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos[attackCount % burretPos.Length][i], Quaternion.identity);
			burretInst.GetComponent<EasySkyBurret>().target = burretTarget;
			burretInst.GetComponent<EasySkyBurret>().speed = burretSpeed;
		}
		attackCount++;
		attackTime -= attackWait;
	}
}
