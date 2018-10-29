﻿using UnityEngine;
public class HardBlackBurret : MonoBehaviour
{
	public Vector2 target;
	public float speed;
	bool mirrorX;
	bool mirrorY;
	void Start()
	{
		transform.LookAt(target);
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, -transform.localEulerAngles.x);
		mirrorX = false;
		mirrorY = false;
	}
	void Update()
	{
		transform.position += transform.right * Time.deltaTime * speed;
		Mirror();
		Destroy();
	}
	void Mirror()
	{
		if (!mirrorX && EndChecker.EndRight(transform.position.x))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, 180.0f - transform.localEulerAngles.z);
			mirrorX = true;
		}
		else if (mirrorX && !EndChecker.EndRight(transform.position.x))
		{
			mirrorX = false;
		}
		if (!mirrorY && EndChecker.EndTop(transform.position.y) || EndChecker.EndBottom(transform.position.y))
		{
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, -transform.localEulerAngles.z);
			mirrorY = true;
		}
		else if (mirrorY && !EndChecker.EndTop(transform.position.y) && !EndChecker.EndBottom(transform.position.y))
		{
			mirrorY = false;
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