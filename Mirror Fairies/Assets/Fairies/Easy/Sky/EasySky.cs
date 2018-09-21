using System;
using UnityEngine;
using UnityEngine.UI;
public class EasySky : MonoBehaviour
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
	Vector2 burretPos;
	Vector2 burretTarget;
	float burretSpeed;
	float burretMoveWait;
	void Start()
	{
		pos = transform.position;
		health = 50;
		time = 0.0f;
		speed = 1.0f;
		move = 2.0f;
		shotWait = 1.0f;
		shotTime = shotWait;
		burretSpeed = 16.0f;
		burretMoveWait = 1.0f;
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
			burretPos = new Vector2(transform.position.x - 1, transform.position.y);
			try
			{
				burretTarget = GameObject.Find("EasyBlack(Clone)").transform.position;
			}
			catch (NullReferenceException e)
			{
				Debug.Log("ERROR:" + e);
				burretTarget = new Vector2(0.0f, 0.0f);
			}
			burretInst = Instantiate(burret, burretPos, Quaternion.identity);
			burretInst.GetComponent<EasySkyBurret>().target = burretTarget;
			burretInst.GetComponent<EasySkyBurret>().speed = burretSpeed;
			burretInst.GetComponent<EasySkyBurret>().moveWait = burretMoveWait;
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
