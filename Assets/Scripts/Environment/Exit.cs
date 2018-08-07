﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Seed") {
			Debug.Log ("LEVEL FINISHED! CONGRATS!");
			// finish level 

			// instantiate UI saying congrats!

//			Application.LoadLevel (0);

			// go to next level
			SceneManager.LoadScene("Level_1_Complete", LoadSceneMode.Single);
		}
	}
}
