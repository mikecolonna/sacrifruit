﻿// LocomotionSimpleAgent.cs
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (Animator))]
public class LocomotionSimpleAgent : MonoBehaviour {
	Animator anim;
	NavMeshAgent agent;
	Vector2 smoothDeltaPosition = Vector2.zero;
	Vector2 velocity = Vector2.zero;
	bool facingRight = true;

	void Start ()
	{
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
//		 Don’t update position automatically
		agent.updatePosition = false;
	}

	void Update ()
	{
		Vector3 worldDeltaPosition = agent.nextPosition - transform.position;

		// Map 'worldDeltaPosition' to local space
		float dx = Vector3.Dot (transform.right, worldDeltaPosition);
		float dy = Vector3.Dot (transform.forward, worldDeltaPosition);
		Vector2 deltaPosition = new Vector2 (dx, dy);

		// Low-pass filter the deltaMove
		float smooth = Mathf.Min(1.0f, Time.deltaTime/0.15f);
		smoothDeltaPosition = Vector2.Lerp (smoothDeltaPosition, deltaPosition, smooth);

		// Update velocity if time advances
		if (Time.deltaTime > 1e-5f)
			velocity = smoothDeltaPosition / Time.deltaTime;

		// Flip
		if (velocity.x > 0.1f && !facingRight) {
			Flip ();
		} else if (velocity.x < -0.1f && facingRight) {
			Flip ();
		}
//		Debug.Log (velocity.x);

		bool shouldMove = velocity.magnitude > 0; // && agent.remainingDistance > agent.radius;

		// Update animation parameters
		anim.SetBool("move", shouldMove);
		anim.SetFloat ("speed", velocity.x);
//		Debug.Log (velocity.x);
//		anim.SetFloat ("vely", velocity.y);

//		GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;
	}

	void Flip () 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void OnAnimatorMove ()
	{
		// Update position to agent position
		transform.position = agent.nextPosition;
	}
}