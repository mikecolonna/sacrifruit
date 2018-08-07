using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column_L1 : ActionObject {

	private Vector3 originalState;

	// Use this for initialization
	void Start () {
		originalState = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y > 9.0f) {
			transform.position = new Vector3(transform.position.x, 9.0f, transform.position.z);
			GetComponent<AudioSource> ().Stop ();
		}
	}

	public override void ResetState() {
		transform.position = originalState;
	}

	public override bool isActivated() {
		return !transform.position.Equals (originalState);
	}
}
