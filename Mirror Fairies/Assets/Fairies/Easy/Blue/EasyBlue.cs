using UnityEngine;
using UnityEngine.UI;
public class EasyBlue : MonoBehaviour
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
	float[] burretRot;
	float burretSpeed;
	void Start ()
	{
		pos = transform.position;
		health = 50;
		time = 0.0f;
		speed = 1.0f;
		move = 2.0f;
		attackWait = 1.0f;
		attackTime = attackWait;
		burretNum = 3;
		burretRot = new float[3]
		{
			135.0f,
			-180.0f,
			-135.0f
		};
		burretSpeed = 4.0f;
	}
	void Update ()
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
				MainManager.enemySpawn = true;
				Destroy(gameObject);
			}
			healthText.text = health.ToString();
		}
	}
	void Attack()
	{
		Vector2[] burretPos = new Vector2[3]
		{
			new Vector2(transform.position.x - 1, transform.position.y + 1),
			new Vector2(transform.position.x - 1, transform.position.y),
			new Vector2(transform.position.x - 1, transform.position.y - 1)
		};
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos[i], Quaternion.Euler(0.0f, 0.0f, burretRot[i]));
			burretInst.GetComponent<EasyBlueBurret>().speed = burretSpeed;
		}
		attackTime -= attackWait;
	}
}
