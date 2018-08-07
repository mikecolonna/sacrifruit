using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : ActionObject {

	private PlayerManager playerManager;


	private Vector3 originalState;

	// Use this for initialization
	void Start () {
		originalState = transform.position;
		playerManager = GameObject.Find("MoveDirector").GetComponent<PlayerManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > -2.2f) {
			transform.position = new Vector3(transform.position.x, -2.2f, transform.position.z);
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
			var activeFruit = playerManager.activeCharacter;
			if (activeFruit.name == "Watermelon") {
				Debug.Log ("SPIKE HIT WATERMELON");
				activeFruit.Split ();
			}
		}
	}
}
