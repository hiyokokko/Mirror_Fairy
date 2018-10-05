using UnityEngine;
using UnityEngine.UI;
public class NormalBlue : MonoBehaviour
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
		health = 100;
		time = 0.0f;
		speed = 2.0f;
		move = 2.0f;
		attackWait = 0.25f;
		attackTime = attackWait;
		attackCount = 0;
		burretNum = 2;
		burretPos = new Vector2[8]
		{
			new Vector2(pos.x - 1.0f, pos.y),
			new Vector2(pos.x - 1.0f, pos.y + 1.0f),
			new Vector2(pos.x - 1.0f, pos.y + 2.0f),
			new Vector2(pos.x - 1.0f, pos.y + 1.0f),
			new Vector2(pos.x - 1.0f, pos.y),
			new Vector2(pos.x - 1.0f, pos.y - 1.0f),
			new Vector2(pos.x - 1.0f, pos.y - 2.0f),
			new Vector2(pos.x - 1.0f, pos.y - 1.0f)
		};
		burretRot = new float[2]
		{
			165.0f,
			-165.0f
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
		if (col.gameObject.transform.tag == "Player")
		{
			health--;
			Destroy(col.gameObject);
			if (health <= 0)
			{
				Main.enemySpawn = true;
				Destroy(gameObject);
			}
			healthText.text = health.ToString();
		}
	}
	void Attack()
	{
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos[attackCount % burretPos.Length], Quaternion.Euler(0.0f, 0.0f, burretRot[i]));
			burretInst.GetComponent<NormalBlueBurret>().speed = burretSpeed;
		}
		attackCount++;
		attackTime -= attackWait;
	}
}
