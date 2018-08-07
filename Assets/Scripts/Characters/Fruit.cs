using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fruit : Character {

	private Animator anim;
	private string button_name;

	private PlayerManager playerManager;

	public string name;
	private bool fling = false;
	private bool kill_watermelon = false;

	private int count = 0;

	// MOVEMENT
	[SerializeField]
	private float speed = 1.5f;

	[SerializeField]
	private Vector3 goal;

	private Vector2 velocity = Vector2.zero;
	private bool facingRight = true;
	private Vector2 smoothDeltaPosition = Vector2.zero;

	[SerializeField]
	private Vector3 nextPosition;

	// STATE BOOLS
	[SerializeField]
	private bool active = false;
	private bool alive = true;
	private bool movementDisabled = false;

	// Use this for initialization
	void Start () {
		goal = transform.position;
		anim = GetComponent<Animator> ();
		nextPosition = transform.position;
		playerManager = GameObject.Find("MoveDirector").GetComponent<PlayerManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (alive) {
			
			if (fling) {
				deselectCharacter ();
				var seed = GameObject.Find("Seed").GetComponent<Fruit> ();
				seed.selectCharacter ();
				playerManager.activeCharacter = seed;
				transform.Rotate (0,0,69);
				//find rise and run
				var run = GameObject.Find (button_name).transform.position.x - transform.position.x;
				var rise = GameObject.Find (button_name).transform.position.y - transform.position.y;
				transform.position += Vector3.up * (rise * 0.1f);
				transform.position += Vector3.right * (run * 0.1f);
					//GameObject.Find (button_name).transform.position;
				this.count++;
				Debug.Log (this.count);
				if (this.count >= 25) {
					transform.position =  GameObject.Find (button_name).transform.position;
					this.fling = false;
				}
			}


			if (kill_watermelon) {
				Debug.Log ("KILLED WATERMELON");
				var spike = GameObject.Find("spike").GetComponent<Spike>();
				spike.ResetState ();
			}




			if (active && !movementDisabled) {
				Move ();
			} else {
				ResetMovement ();
			}



		}
	}

	public override void Move() {
		if (!goal.Equals (transform.position)) {
			float step = speed * Time.deltaTime;
			transform.rotation = Quaternion.identity; // don't rotate

			Vector3 offset = goal - transform.position;

			nextPosition = Vector3.MoveTowards (transform.position, goal, step);

			Vector3 worldDeltaPosition = nextPosition - transform.position;

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

			bool shouldMove = velocity.magnitude > 0.2f; // && remaining > 0.2f;

			// Update animation parameters
			anim.SetBool("move", shouldMove);
			anim.SetFloat ("speed", Mathf.Abs(velocity.x));
		}
	}
		
	public void SetDest(Vector3 destination) {
		goal = destination;
	}

	private void OnAnimatorMove ()
	{
		// Update position to agent position
		if (active) {
			Vector3 nextPosition = transform.position;
			nextPosition.x += velocity.x * Time.deltaTime;
			transform.position = nextPosition;
		}
	}

	private void Flip () 
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public bool isSelected() {
		return active;
	}

	public void selectCharacter() {
		// make marker appear 
		transform.GetChild(0).gameObject.SetActive(true);

		// make dance
		anim.SetTrigger("celebrate");

		// set active
		this.active = true;
	}

	public void deselectCharacter() {
		// make marker disappear
		transform.GetChild(0).gameObject.SetActive(false);

		// set active
		this.active = false;
	}

	public void killCharacter() {
		Debug.Log ("KILL");
		alive = false;
		//Destroy (GetComponent<Fruit>());
		Destroy (GetComponent<SpriteRenderer>());
		Destroy (GetComponent<BoxCollider2D>());
		Destroy (GetComponent<CapsuleCollider2D>());
	}

	public void disableMovement() {
		movementDisabled = true;
	}

	public void enableMovement() {
		movementDisabled = false;
	}

	public void ResetMovement() {
		goal = transform.position;

		// zero velocity
		velocity = Vector2.zero;

		// stop walking animation
		anim.SetBool ("move", false);
		anim.SetFloat ("speed", 0.0f);
	}

	public void Fling(string button) {
		this.button_name = button;
		anim.SetTrigger ("fling");
		StartCoroutine(TriggerFling (anim.GetCurrentAnimatorStateInfo(0).length));
		Debug.Log (anim.GetCurrentAnimatorStateInfo(0).length);

	}


	IEnumerator TriggerFling(float time) {
		yield return new WaitForSeconds (time);
		this.fling = true;
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.collider.tag == "Obstacle") {
			anim.SetTrigger ("death");
			//StartCoroutine(TriggerCherryDeath (anim.GetCurrentAnimatorStateInfo(0).length));
		}
	}

	public void Split() {
		anim.SetTrigger ("split");
		StartCoroutine(TriggerSplit (anim.GetCurrentAnimatorStateInfo(0).length));


	}

	IEnumerator TriggerCherryDeath(float time) {
		yield return new WaitForSeconds (time);
		killCharacter ();
	}


	IEnumerator TriggerSplit(float time) {
		yield return new WaitForSeconds (time);
		deselectCharacter ();
		var seed = GameObject.Find("Seed").GetComponent<Fruit> ();
		seed.selectCharacter ();
		playerManager.activeCharacter = seed;

		//switch to half watermelon sprite
		transform.GetComponent<SpriteRenderer>().sprite =  Resources.Load<Sprite>("Sprites/Characters/Watermelom/Split/Asset50");

		//move spike down
		this.kill_watermelon = true;
	}

}
