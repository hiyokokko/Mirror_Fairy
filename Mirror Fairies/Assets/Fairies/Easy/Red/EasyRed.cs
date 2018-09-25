using System;
using UnityEngine;
using UnityEngine.UI;
public class EasyRed : MonoBehaviour
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
	int burretNum;
	Vector2[] burretPos;
	Vector2 burretTarget;
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
		burretNum = 6;
		burretSpeed = 8.0f;
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
			burretPos = new Vector2[6]
			{
				new Vector2(transform.position.x, transform.position.y + 1),
				new Vector2(transform.position.x, transform.position.y + 2),
				new Vector2(transform.position.x, transform.position.y + 3),
				new Vector2(transform.position.x, transform.position.y - 1),
				new Vector2(transform.position.x, transform.position.y - 2),
				new Vector2(transform.position.x, transform.position.y - 3)
			};
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
				burretInst = Instantiate(burret, burretPos[i], Quaternion.identity);
				burretInst.GetComponent<EasyRedBurret>().target = burretTarget;
				burretInst.GetComponent<EasyRedBurret>().speed = burretSpeed;
			}
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
