using UnityEngine;
using UnityEngine.UI;
public class NormalPurple : MonoBehaviour
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
	Vector2 burretPos;
	float[] burretRot;
	float burretSpeed;
	void Start()
	{
		pos = transform.position;
		health = 400;
		time = 0.0f;
		speed = 1.0f;
		move = 2.0f;
		attackWait = 2.0f;
		attackTime = attackWait;
		burretNum = 17;
		burretPos = new Vector2(pos.x - 1, pos.y);
		burretRot = new float[17]
		{
			100.0f,
			110.0f,
			120.0f,
			130.0f,
			140.0f,
			150.0f,
			160.0f,
			170.0f,
			-180.0f,
			-170.0f,
			-160.0f,
			-150.0f,
			-140.0f,
			-130.0f,
			-120.0f,
			-110.0f,
			-100.0f
		};
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
		if (col.gameObject.transform.tag == "Player" && GameObject.Find("NormalBlack(Clone)") != null)
		{
			health--;
			Destroy(col.gameObject);
			if (health <= 0)
			{
				AudioSE.kill = true;
				Main.kill = 6;
				Main.killTime = Main.time;
				Main.gameOver = true;
				Destroy(gameObject);
			}
			AudioSE.damage = true;
			healthText.text = health.ToString();
		}
	}
	void Attack()
	{
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos, Quaternion.Euler(0.0f, 0.0f, burretRot[i]));
			burretInst.GetComponent<NormalPurpleBurret>().speed = burretSpeed;
		}
		attackTime -= attackWait;
	}
}
