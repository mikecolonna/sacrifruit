using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTileManager : MonoBehaviour {

	private GameObject[] tiles;
	private bool aTileIsTapped = false;

	[SerializeField]
	private GameObject player;
//	private bool moving = false;

	private Transform currDestination;

	// Use this for initialization
	void Start () {
		tiles = GameObject.FindGameObjectsWithTag ("Tile");
	}
	
	// Update is called once per frame
	void Update () {
		if (!aTileIsTapped) {
//			Debug.Log ("hey");
			CheckIfTileTapped ();
		}
			
//		MovePlayer (currDestination);
	}

	void CheckIfTileTapped() {
		if (Input.GetMouseButtonDown(0)) {
			Vector3 touch = Input.mousePosition;
			Ray ray = Camera.main.ScreenPointToRay(touch);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000.0f)) {
				foreach (GameObject obj in tiles) {
					GroundTile tile = obj.GetComponent<GroundTile> ();
					if (hit.collider.gameObject == tile.gameObject) {
						tile.FadeStart (Time.time);
						tile.tapped = true;
						aTileIsTapped = true;

//						currDestination = tile.transform;
//						MovePlayer ();

						StartCoroutine (TapPowerDown (tile));
						break;
					}
				}
			}
		}	
	}

//	void MovePlayer() {
////		Debug.Log ("HERE");
//		TestPlayer playerController = player.GetComponent<TestPlayer> ();
//		playerController.SetTarget (currDestination);
////		Vector3 offset = currDestination - player.transform.position;
////		Vector3.MoveTowards(player.transform.position, currDestination, 
////		playerController._dest = currDestination;
//	}
					
	public IEnumerator TapPowerDown(GroundTile tile) {
		yield return new WaitForSeconds (0.5f);
		tile.tapped = false;
		aTileIsTapped = false;
	}
}
