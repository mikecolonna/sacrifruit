using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_L4_2 : ActionObject {

	private PlayerManager playerManager;

	[SerializeField]
	private GameObject spike;

	[SerializeField]
	private float spike_speed;

	private Animator buttonAnim;

	private bool pressed;

	// Use this for initialization
	void Start () {
		playerManager = GameObject.Find("MoveDirector").GetComponent<PlayerManager> ();
		buttonAnim = GetComponent<Animator> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (pressed) {
			spike.transform.position += Vector3.up * spike_speed;
		}
		
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (!pressed) {
			if (other.collider.tag == "Fruit") {

				if (other.collider.name == "Cherry") {
					Destroy (other.collider.GetComponent<SpriteRenderer>());
					Destroy (other.collider.GetComponent<BoxCollider2D>());
					Destroy (other.collider.GetComponent<CapsuleCollider2D>());

				}

				// trigger button press animation
				buttonAnim.SetTrigger ("press");

				StartCoroutine (TriggerColumnRoutine ());
			}
		}
	}

	public IEnumerator TriggerColumnRoutine() {
		yield return new WaitForSeconds (buttonAnim.GetCurrentAnimatorStateInfo (0).length);

		pressed = true;
	}

	public override void ResetState() {
		pressed = false;
		buttonAnim = GetComponent<Animator> ();
		buttonAnim.Play ("Button");
	}

	public override bool isActivated() {
		return pressed;
	}
}
