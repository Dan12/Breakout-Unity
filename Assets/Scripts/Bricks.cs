using UnityEngine;
using System.Collections;

public class Bricks : MonoBehaviour {

	public GameObject brickParticle;

	void OnCollisionEnter(){
		Instantiate (brickParticle, transform.position, Quaternion.identity);
		GameManager.instance.destroyBrick ();
		Destroy (gameObject);
	}
}
