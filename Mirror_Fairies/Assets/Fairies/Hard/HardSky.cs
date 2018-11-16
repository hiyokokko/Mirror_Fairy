using UnityEngine;
using UnityEngine.UI;
public class HardSky : MonoBehaviour
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
	Vector2[] burretPos;
	float[] burretRot;
	float burretSpeed;
	void Start()
	{
		pos = transform.position;
		health = 400;
		time = 0.0f;
		speed = 2.0f;
		move = 2.0f;
		attackWait = 1.0f;
		attackTime = attackWait;
		attackCount = 0;
		burretNum = 16;
		burretPos = new Vector2[2]
		{
			new Vector2(8.0f, 0.0f),
			new Vector2(-8.0f, 0.0f)
		};
		burretRot = new float[16]
		{
			0.0f,
			22.5f,
			45.0f,
			67.5f,
			90.0f,
			112.5f,
			135.0f,
			157.5f,
			-180.0f,
			-157.5f,
			-135.0f,
			-112.5f,
			-90.0f,
			-67.5f,
			-45.0f,
			-22.5f
		};
		burretSpeed = 2.0f;
		
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
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos[attackCount % burretPos.Length], Quaternion.Euler(0.0f, 0.0f, burretRot[i]));
			burretInst.GetComponent<HardSkyBurret>().speed = burretSpeed;
		}
		attackCount++;
		attackTime -= attackWait;
	}
}
