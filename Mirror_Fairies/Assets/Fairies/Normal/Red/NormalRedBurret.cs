using UnityEngine;
public class NormalRedBurret : MonoBehaviour
{
	[SerializeField] GameObject burret;
	public Vector2 target;
	public float speed;
	public int pattern;
	float attackWait;
	float attackTime;
	int burretNum;
	float[] burretRot;
	void Start()
	{
		if (pattern == 0)
		{
			transform.LookAt(target);
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180 + transform.localEulerAngles.x);
		}
		else
		{
			attackWait = 1.0f;
			attackTime = 0.0f;
		}
		burretNum = 4;
		burretRot = new float[4]
		{
			45.0f,
			135.0f,
			-135.0f,
			-45.0f
		};
	}
	void Update()
	{
		transform.position += transform.right * Time.deltaTime * speed;
		switch (pattern)
		{
			case 0:
				if (transform.position.x <= target.x) { Attack(); }
				break;
			case 1:
				attackTime += Time.deltaTime;
				if (attackTime > attackWait) { Attack(); }
				break;
		}
		Mirror();
		Destroy();
	}
	void Attack()
	{
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret, transform.position, Quaternion.Euler(0.0f, 0.0f, burretRot[i]));
			burretInst.transform.localScale = transform.localScale / 2;
			burretInst.GetComponent<NormalRedBurret>().speed /= 2;
			burretInst.GetComponent<NormalRedBurret>().pattern += 1;
		}
		Destroy(gameObject);
	}
	void Mirror()
	{
		if (EndChecker.EndRight(transform.position.x))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f - transform.localEulerAngles.z);
		}
		if (EndChecker.EndTop(transform.position.y) || EndChecker.EndBottom(transform.position.y))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, -transform.localEulerAngles.z);
		}
	}
	void Destroy()
	{
		if (EndChecker.EndLeft(transform.position.x))
		{
			Destroy(gameObject);
		}
	}
}
