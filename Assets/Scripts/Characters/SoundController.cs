using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public AudioClip leftFootstep;
	public AudioClip rightFootstep;
	private AudioSource source;

	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayFootstep(string step) {
		if (step == "right") {
			source.PlayOneShot (rightFootstep);
		} else if (step == "left") {
			source.PlayOneShot (leftFootstep);
		}
	}
}
