using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_L1 : ActionObject {

	[SerializeField]
	private GameObject column;

	[SerializeField]
	private float col_speed = 0.5f;

	private Animator buttonAnim;

	private AudioSource audioSource;

	private Camera camera;

	private bool pressed;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		buttonAnim = GetComponent<Animator> ();
		camera = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		if (pressed) {
			column.transform.position += Vector3.up * col_speed;
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		
		if (!pressed) {
			if (other.collider.tag == "Fruit") {

				// trigger button press animation
				buttonAnim.SetTrigger ("press");

				// play sound
				audioSource.Play ();
				StartCoroutine (TriggerColumnRoutine ());
			}
		}
	}

	public IEnumerator TriggerColumnRoutine() {
		yield return new WaitForSeconds (buttonAnim.GetCurrentAnimatorStateInfo (0).length);
		column.GetComponent<AudioSource> ().Play();

		// shake camera
		camera.GetComponent<CameraShake>().ShakeCamera(2.0f, 11.0f);

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
