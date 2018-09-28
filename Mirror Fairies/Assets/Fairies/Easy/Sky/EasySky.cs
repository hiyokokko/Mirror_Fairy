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
	float burretSpeed;
	float burretMoveWait;
	void Start()
	{
		pos = transform.position;
		health = 50;
		time = 0.0f;
		speed = 1.0f;
		move = 2.0f;
		attackWait = 1.0f;
		attackTime = attackWait;
		burretSpeed = 16.0f;
		burretMoveWait = 1.0f;
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
				MainManager.enemySpawn = true;
				Destroy(gameObject);
			}
			healthText.text = health.ToString();
		}
	}
	void Attack()
	{
		Vector2 burretPos = new Vector2(transform.position.x - 1, transform.position.y);
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
		GameObject burretInst = Instantiate(burret, burretPos, Quaternion.identity);
		burretInst.GetComponent<EasySkyBurret>().target = burretTarget;
		burretInst.GetComponent<EasySkyBurret>().speed = burretSpeed;
		burretInst.GetComponent<EasySkyBurret>().moveWait = burretMoveWait;
		attackTime -= attackWait;
	}
}
