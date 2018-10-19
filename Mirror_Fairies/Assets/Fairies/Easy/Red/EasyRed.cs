using UnityEngine;
using UnityEngine.UI;
public class EasyRed : MonoBehaviour
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
	int burretNum;
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
		burretNum = 6;
		burretSpeed = 8.0f;
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
		Vector2[] burretPos = new Vector2[6]
		{
			new Vector2(transform.position.x, transform.position.y + 1),
			new Vector2(transform.position.x, transform.position.y + 2),
			new Vector2(transform.position.x, transform.position.y + 3),
			new Vector2(transform.position.x, transform.position.y - 1),
			new Vector2(transform.position.x, transform.position.y - 2),
			new Vector2(transform.position.x, transform.position.y - 3)
		};
		Vector2 burretTarget;
		try
		{
			burretTarget = GameObject.Find("EasyBlack(Clone)").transform.position;
		}
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
			burretTarget = new Vector2(0.0f, 0.0f);
		}
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos[i], Quaternion.identity);
			burretInst.GetComponent<EasyRedBurret>().target = burretTarget;
			burretInst.GetComponent<EasyRedBurret>().speed = burretSpeed;
		}
		attackTime -= attackWait;
	}
}
