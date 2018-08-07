using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TransitionToNextScene : MonoBehaviour {

	private VideoPlayer vp;
//	private SceneManager sceneManager;
	private double time;

	// Use this for initialization
	void Start () {
		vp = GetComponent<VideoPlayer> ();
		time = vp.clip.length;
	}
	
	// Update is called once per frame
	void Update () {
		double currTime = vp.time;
		if (currTime >= time) {
			SceneManager.LoadScene ("Level_1");
		}
	}
}
