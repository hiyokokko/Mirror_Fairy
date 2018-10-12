﻿using UnityEngine;
using UnityEngine.UI;
public class EasyGreen : MonoBehaviour
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
	float burretRot;
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
		burretRot = 180.0f;
		burretSpeed = 4.0f;
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
				Main.kill++;
				Main.killTime = Main.time;
				Main.enemySpawn = true;
				Destroy(gameObject);
			}
			healthText.text = health.ToString();
		}
	}
	void Attack()
	{
		GameObject burretInst = Instantiate(burret, transform.position, Quaternion.Euler(0.0f, 0.0f, burretRot));
		burretInst.GetComponent<EasyGreenBurret>().speed = burretSpeed;
		attackTime -= attackWait;
	}
}
