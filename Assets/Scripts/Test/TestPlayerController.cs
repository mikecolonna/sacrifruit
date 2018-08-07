﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = true;

	public float moveForce = 365f;
	public float maxSpeed = 5f;	// cap speed
	public bool active = false;

	//public float jumpForce = 1000f;
	//public Transform groundCheck; // check if player is on the ground

	//private bool grounded = false;
	private Rigidbody2D rb2d;

	// Use this for initialization
	void Awake () {
		rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
		/*if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
		}*/
	}

	void Flip () 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	// physics
	void FixedUpdate ()
	{
		if (active) {
			float h = Input.GetAxis ("Horizontal");

			if (h * rb2d.velocity.x < maxSpeed) {
				rb2d.AddForce (Vector2.right * h * moveForce);
			}

			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed) {
				// figuring out if velocity is > 0 or < 0
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
			}

			if (h > 0 && !facingRight) {
				Flip ();
			} else if (h < 0 && facingRight) {
				Flip ();
			}

			/*if (jump) {
			rb2d.AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		}*/
		}
	}
}
