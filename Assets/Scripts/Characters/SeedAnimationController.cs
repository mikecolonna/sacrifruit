using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeedAnimationController : MonoBehaviour {

	private Animator anim;
	private Scene currentScene;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		currentScene = SceneManager.GetActiveScene ();

		if (!(currentScene.name == "Level_1")) {
			anim.Play ("seed_idle");
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
