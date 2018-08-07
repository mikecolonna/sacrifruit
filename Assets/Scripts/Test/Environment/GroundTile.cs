using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : UnityEngine.Tilemaps.Tile {

	public bool tapped = false;
	private float startTime;
	private float fadeSpeed = 3.0f;

	Animator anim;

	// Use this for initialization
	void Start () {
//		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (tapped) {
			float t = (Time.time - startTime) * fadeSpeed;
//			GetComponent<Renderer> ().material.color = Color.Lerp (Color.blue, Color.white, t);
		}
	}

	public void FadeStart(float time) {
		startTime = time;
	}

	public void TriggerTapAnim() {
		anim.SetBool ("tapped", true);
//		anim.
	}


}
