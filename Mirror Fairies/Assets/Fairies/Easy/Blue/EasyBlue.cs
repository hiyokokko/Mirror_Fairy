using UnityEngine;
using UnityEngine.UI;
public class EasyBlue : MonoBehaviour
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
	int shotCount;
	int burretNum;
	Vector2[] burretPos;
	float[][] burretRot;
	float burretSpeed;
	void Start ()
	{
		pos = transform.position;
		health = 50;
		time = 0.0f;
		speed = 1.0f;
		move = 2.0f;
		shotWait = 1.0f;
		shotTime = shotWait;
		shotCount = 0;
		burretNum = 2;
		burretRot = new float[2][]
		{
			new float[3]
			{
				120.0f,
				135.0f,
				150.0f
			},
			new float[3]
			{
				-120.0f,
				-135.0f,
				-150.0f
			}
		};
		burretSpeed = 4.0f;
	}
	void Update ()
	{
		time += Time.deltaTime;
		Shot();
		transform.position = new Vector2(pos.x, pos.y + Mathf.Sin(time * Mathf.PI / 2 * speed) * move);
	}
	void Shot()
	{
		if (shotTime >= shotWait)
		{
			burretPos = new Vector2[2]
			{
				new Vector2(pos.x, transform.position.y + 1),
				new Vector2(pos.x, transform.position.y - 1)
			};
			for (int i = 0; i < burretNum; i++)
			{
				burretInst = Instantiate(burret, burretPos[i], Quaternion.Euler(0.0f, 0.0f, burretRot[i][shotCount % 3]));
				burretInst.GetComponent<EasyBlueBurret>().speed = burretSpeed;
			}
			shotTime -= shotWait;
			shotCount++;
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
