﻿using UnityEngine;
public class EasyBlack : MonoBehaviour
{
	[SerializeField] GameObject burret;
	PlayerTouchState playerTouchState;
	float attackWait;
	float attackTime;
	float burretSpeed;
	float moveRestRight;
	float moveRestOther;
	void Start()
	{
		PlayerOperation.cam = GameObject.Find("Camera").GetComponent<Camera>();
		playerTouchState = new PlayerTouchState();
		attackWait = 0.1f;
		attackTime = attackWait;
		burretSpeed = 16.0f;
		moveRestRight = 3.0f;
		moveRestOther = 1.0f;
	}
	void Update()
	{
		PlayerOperation.PlayerTouch(ref playerTouchState, transform.position);
		if (attackTime < attackWait) { attackTime += Time.deltaTime; }
		if (playerTouchState.attack != -1 && attackTime >= attackWait) { Attack(); }
		if (playerTouchState.move != -1) { Move(); }
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		AudioSE.kill = true;
		Main.gameOver = true;
		Destroy(gameObject);
	}
	void OnTriggerEnter2D(Collider2D col)
	{
		AudioSE.kill = true;
		Main.gameOver = true;
		Destroy(gameObject);
	}
	void Attack()
	{
		Vector2 burretPos = new Vector2(transform.position.x + 1, transform.position.y);
		GameObject burretInst = Instantiate(burret, burretPos, Quaternion.identity);
		burretInst.GetComponent<EasyBlackBurret>().speed = burretSpeed;
		attackTime -= attackWait;
	}
	void Move()
	{
		Vector2 afterPos = playerTouchState.beforePos + (playerTouchState.afterTouchPos - playerTouchState.beforeTouchPos);
		transform.position = afterPos;
		if (EndChecker.EndRight(transform.position.x + moveRestRight))
		{
			transform.position = new Vector2(EndChecker.endRight - moveRestRight, transform.position.y);
		}
		else if (EndChecker.EndLeft(transform.position.x - moveRestOther))
		{
			transform.position = new Vector2(EndChecker.endLeft + moveRestOther, transform.position.y);
		}
		if (EndChecker.EndTop(transform.position.y + moveRestOther))
		{
			transform.position = new Vector2(transform.position.x, EndChecker.endTop - moveRestOther);
		}
		else if (EndChecker.EndBottom(transform.position.y - moveRestOther))
		{
			transform.position = new Vector2(transform.position.x, EndChecker.endBottom + moveRestOther);
		}
	}
}
