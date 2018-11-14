using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour {
	float loopPos = -32.0f;
	float speed = 4.0f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position -= transform.right * Time.deltaTime * speed;
		if (transform.position.x <= loopPos) { transform.position += transform.right * 64; }
	}
}
