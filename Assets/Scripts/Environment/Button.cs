using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : ActionObject {

	private Animator buttonAnim;
	protected bool pressed;

//	[SerializeField]
//	private GameObject moveDirector;
	private TimeManager timeManager;

	// Use this for initialization
	void Start () {
		timeManager = GameObject.FindObjectOfType (typeof(TimeManager)) as TimeManager;
		Debug.Log (timeManager);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter2D(Collision2D other) {
		Debug.Log ("ON COLLISION ENTER");
		if (!pressed) {
			if (other.collider.tag == "Fruit") {
				pressed = true;

				// trigger button press animation
				buttonAnim = GetComponent<Animator> ();
				buttonAnim.SetTrigger ("press");

				// trigger fruit jump animation
//				Animator fruitAnim = other.collider.GetComponent<Animator> ();
//				fruitAnim.SetTrigger ("jump");

			}
		}
	}

	public override void ResetState() {
//		Debug.Log ("BUTTON HAS BEEN RESET");
		pressed = false;
		buttonAnim = GetComponent<Animator> ();
		buttonAnim.Play ("Button");
	}

	public override bool isActivated() {
		return pressed;
	}
		
}
