﻿using UnityEngine;
using UnityEngine.UI;
public class EasyYellow : MonoBehaviour
{
	[SerializeField] GameObject burret;
	[SerializeField] Text healthText;
	GameObject burretInst;
	Vector2 pos;
	int health;
	float time;
	float speed;
	float move;
	float shotWait;
	float shotTime;
	float burretSpeed;
	void Start()
	{
		pos = transform.position;
		health = 50;
		time = 0.0f;
		speed = 1.0f;
		move = 2.0f;
		shotWait = 1.0f;
		shotTime = shotWait;
		burretSpeed = 2.0f;
	}
	void Update()
	{
		time += Time.deltaTime;
		Shot();
		transform.position = new Vector2(pos.x, pos.y + Mathf.Sin(time * Mathf.PI / 2 * speed) * move);
	}
	void Shot()
	{
		if (shotTime >= shotWait)
		{
			burretInst = Instantiate(burret, transform.position, Quaternion.identity);
			burretInst.GetComponent<EasyYellowBurret>().speed = burretSpeed;
			shotTime -= shotWait;
		}
		if (shotTime < shotWait)
		{
			shotTime += Time.deltaTime;
		}
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
}