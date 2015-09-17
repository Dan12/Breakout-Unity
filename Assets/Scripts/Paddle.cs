using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public float paddleSpeed = 1f;
	public float wallBounds = 6.5f;

	private Vector3 playerPos = new Vector3(0,0.5f,0);
	private float prevSpeed = 0f;

	// Update is called once per frame
	void Update () {
		float speed = Input.GetAxis ("Horizontal");
		int pSpeed = 0;
		if (speed > 0)
			pSpeed = 1;
		else if (speed < 0)
			pSpeed = -1;
		if ((speed > 0 && prevSpeed > speed) || (speed < 0 && prevSpeed < speed))
			pSpeed = 0;
		prevSpeed = speed;
		float xPos = transform.position.x + pSpeed*paddleSpeed;
		playerPos = new Vector3 (Mathf.Clamp (xPos, -wallBounds, wallBounds), 0.5f, 0);

		transform.position = playerPos;
	}
}
