using UnityEngine;
using UnityEngine.UI;
public class FairyController : MonoBehaviour
{
	[SerializeField] GameObject[] burretPrefab;
	[SerializeField] Text healthText;
	[SerializeField] int fairyNum;
	[SerializeField] int health;
	[SerializeField] float moveSpeed;
	[SerializeField] float moveRange;
	[SerializeField] float attackWait;
	[SerializeField] long[] attackPattern;
	[SerializeField] Vector3Array2[] attackInfo;
	[SerializeField] bool[] aim;
	[SerializeField] Vector2Array2[] aimPos;
	[SerializeField] bool[] targetShiftPosX;
	[SerializeField] bool[] targetShiftPosY;
	Vector2 initPos;
	float moveTime = 0.0f;
	float attackTime;
	int attackCount = 0;
	void Start()
	{
		healthText.text = health.ToString();
		initPos = transform.position;
		attackTime = attackWait;
	}
	void Update()
	{
		moveTime += Time.deltaTime;
		if (attackTime >= attackWait)
		{
			Attack();
		}
		else
		{
			attackTime += Time.deltaTime;
		}
		transform.position = new Vector2(initPos.x, initPos.y + Mathf.Sin(moveTime * Mathf.PI / 2 * moveSpeed) * moveRange);
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
				Main.kill = fairyNum;
				Main.killTime = Main.time;
				if (fairyNum != Main.enemyNum)
				{
					Main.enemySpawn = true;
				}
				else
				{
					Main.gameOver = true;
				}
				Destroy(gameObject);
			}
			AudioSE.damage = true;
			healthText.text = health.ToString();
		}
	}
	void Attack()
	{
		for (int pattern = 0; pattern < attackPattern.Length; pattern++)
		{
			if ((attackPattern[pattern] & (long)1 << attackCount) != 0)
			{
				Vector2 targetPos = new Vector2();
				if (aim[pattern] || targetShiftPosX[pattern] || targetShiftPosY[pattern])
				{
					try
					{
						targetPos = GameObject.FindGameObjectWithTag("Player").transform.position;
					}
					catch (System.Exception e)
					{
						Debug.Log(e.Message);
					}
				}
				int patternCount = attackCount % attackInfo[pattern].vector3Array.Length;
				for (int infoNum = 0; infoNum < attackInfo[pattern].vector3Array[patternCount].vector3.Length; infoNum++)
				{
					Vector3 info = attackInfo[pattern].vector3Array[patternCount].vector3[infoNum];
					Vector2 burretPos = 
						new Vector2
						(
							info.x +
							(targetShiftPosX[pattern] ? targetPos.x : 0.0f),
							info.y +
							(targetShiftPosY[pattern] ? targetPos.y : 0.0f)
						);
					Vector3 burretRot = 
						new Vector3
						(
							0.0f,
							0.0f,
							info.z
						);
					GameObject burretInst = Instantiate(burretPrefab[pattern], burretPos, Quaternion.Euler(burretRot));
					if (aim[pattern])
					{
						burretInst.GetComponent<BurretController>().targetPos = targetPos + aimPos[pattern].vector2Array[patternCount].vector2[infoNum];
					}
				}
			}
		}
		if (attackCount < 47)
		{
			attackCount++;
		}
		else
		{
			attackCount = 0;
		}
		attackTime -= attackWait;
	}
}
[System.Serializable]
public class Vector2Array
{
	public Vector2[] vector2;
	public Vector2Array(Vector2[] vector2)
	{
		this.vector2 = vector2;
	}
}
[System.Serializable]
public class Vector2Array2
{
	public Vector2Array[] vector2Array;
	public Vector2Array2(Vector2Array[] vector2Array)
	{
		this.vector2Array = vector2Array;
	}
}
[System.Serializable]
public class Vector3Array
{
	public Vector3[] vector3;
	public Vector3Array(Vector3[] vector3)
	{
		this.vector3 = vector3;
	}
}
[System.Serializable]
public class Vector3Array2
{
	public Vector3Array[] vector3Array;
	public Vector3Array2(Vector3Array[] vector3Array)
	{
		this.vector3Array = vector3Array;
	}
}
