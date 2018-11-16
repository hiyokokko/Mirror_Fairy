using UnityEngine;
using UnityEngine.UI;
public class HardBlue : MonoBehaviour
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
	int[] burretNum;
	Vector2 burretPos;
	float[][] burretRot;
	float burretSpeed;
	void Start()
	{
		pos = transform.position;
		health = 400;
		time = 0.0f;
		speed = 2.0f;
		move = 4.0f;
		attackWait = 0.5f;
		attackTime = attackWait;
		attackCount = 0;
		burretNum = new int[2]
		{
			5,
			4
		};
		burretPos = new Vector2(pos.x + 2, pos.y);
		burretRot = new float[2][]
		{
			new float[5]
			{
				140.0f,
				160.0f,
				-180.0f,
				-160.0f,
				-140.0f
			},
			new float[4]
			{
				150.0f,
				170.0f,
				-170.0f,
				-150.0f,
			}
		};
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
		if (col.gameObject.transform.tag == "PlayerBurret" && GameObject.FindGameObjectWithTag("Player") != null)
		{
			health--;
			Destroy(col.gameObject);
			if (health <= 0)
			{
				AudioSE.kill = true;
				Main.kill = 1;
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
		for (int i = 0; i < burretNum[attackCount % burretNum.Length]; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos, Quaternion.Euler(0.0f, 0.0f, burretRot[attackCount % burretRot.Length][i]));
		}
		attackCount++;
		attackTime -= attackWait;
	}
}
