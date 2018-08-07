using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAnimationController : MonoBehaviour {

	private Animator anim;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.collider.tag == "Tile") {
			anim.SetTrigger ("landed");
			audioSource.Play ();
		}
	}
}
