using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpike : MonoBehaviour {

	public GameObject water;
	public float fillAmount = 1.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.CompareTag ("Player")) {
			Destroy (other.gameObject);
			//Vector3 pheight = new Vector3(0, 1, 0) * other.gameObj
			water.transform.localScale += new Vector3 (0, fillAmount, 0);
		}
	}
}
