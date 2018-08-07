using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : ActionObject {

	private PlayerManager playerManager;

	private Vector3 originalState;

	// Use this for initialization
	void Start () {
		originalState = transform.position;	
		playerManager = GameObject.Find("MoveDirector").GetComponent<PlayerManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < 1.0f) {
			transform.position = new Vector3(transform.position.x, 1.0f, transform.position.z);

		}
		
	}

	public override void ResetState() {
		transform.position = originalState;
	}

	public override bool isActivated() {
		return !transform.position.Equals (originalState);
	}

	void OnCollisionEnter2D(Collision2D other) {

			if (other.collider.tag == "Fruit") {
			Destroy (other.collider.GetComponent<SpriteRenderer>());
			Destroy (other.collider.GetComponent<BoxCollider2D>());
			Destroy (other.collider.GetComponent<CapsuleCollider2D>());
				
			}
	}
						


	
}
