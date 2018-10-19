using UnityEngine;
using UnityEngine.UI;
public class NormalRed : MonoBehaviour
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
	Vector2 burretPos;
	float burretSpeed;
	void Start()
	{
		pos = transform.position;
		health = 200;
		time = 0.0f;
		speed = 1.0f;
		move = 2.0f;
		attackWait = 4.0f;
		attackTime = attackWait;
		burretPos = new Vector2(pos.x, pos.y);
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
		if (col.gameObject.transform.tag == "Player" && GameObject.Find("NormalBlack(Clone)") != null)
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
	void Attack()
	{
		Vector2 burretTarget; ;
		try
		{
			burretTarget = GameObject.Find("NormalBlack(Clone)").transform.position;
		}
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
			burretTarget = new Vector2(0.0f, 0.0f);
		}
		GameObject burretInst = Instantiate(burret, burretPos, Quaternion.identity);
		burretInst.GetComponent<NormalRedBurret>().target = burretTarget;
		burretInst.GetComponent<NormalRedBurret>().speed = burretSpeed;
		burretInst.GetComponent<NormalRedBurret>().pattern = 0;
		attackTime -= attackWait;
	}
}
