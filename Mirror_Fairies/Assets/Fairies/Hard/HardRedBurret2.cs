using UnityEngine;
public class HardRedBurret2 : MonoBehaviour
{
	public float speed;
	public float wait;
	float time;
	float destroyWait;
	void Start()
	{
		destroyWait = 18.0f;
	}
	void Update()
	{
		time += Time.deltaTime;
		if (time >= wait) { transform.position += transform.right * Time.deltaTime * speed; }
		Destroy();
	}
	void Destroy()
	{
		if (EndChecker.EndTop(transform.position.y - destroyWait) || EndChecker.EndBottom(transform.position.y + destroyWait))
		{
			Destroy(gameObject);
		}
	}
}
