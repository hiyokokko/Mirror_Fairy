using UnityEngine;
public class HardYellowBurret1 : MonoBehaviour
{
	[SerializeField] GameObject burret;
	public Vector2 target;
	public float speed;
	public int attackPattern;
	int burretNum = 4;
	float[] burretRot;
	float burretSpeed = 8.0f;
	void Start()
	{
		transform.LookAt(target);
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180 + transform.localEulerAngles.x);
		burretRot = attackPattern == 0 ?
		new float[4]
		{
			-22.5f,
			-67.5f,
			-112.5f,
			-157.5f
		}
		:
		new float[4]
		{
			22.5f,
			67.5f,
			112.5f,
			157.5f
		};
	}
	void Update()
	{
		transform.position += transform.right * Time.deltaTime * speed;
		if (transform.position.x <= target.x) { Attack(); }
		Mirror();
		Destroy();
	}
	void Attack()
	{
		Vector2 burretPos = transform.position;
		for (int i = 0; i < burretNum; i++)
		{
			GameObject burretInst = Instantiate(burret, burretPos, Quaternion.Euler(0.0f, 0.0f, burretRot[i]));
			burretInst.GetComponent<HardYellowBurret2>().speed = burretSpeed;
		}
		Destroy(gameObject);
	}
	void Mirror()
	{
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
