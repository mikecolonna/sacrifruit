using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

	private bool aTileIsTapped = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void selectTile (GroundTile tile) {
		tile.FadeStart (Time.time);
		tile.tapped = true;
		aTileIsTapped = true;

		Debug.Break ();
		StartCoroutine (TapPowerDown (tile));
	}

	public IEnumerator TapPowerDown(GroundTile tile) {
		yield return new WaitForSeconds (0.5f);
		tile.tapped = false;
		aTileIsTapped = false;
	}
}
