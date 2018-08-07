using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MoveManager : MonoBehaviour {

	[SerializeField]
	private GameObject _tap;
	private PlayerManager playerManager;

	// Use this for initialization
	void Start () {
		playerManager = gameObject.GetComponent<PlayerManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 touchedPos = Input.mousePosition; // - new Vector3 (0.0f, 100.0f, 0.0f);
//			Debug.Log ("touched at : " + touchedPos);
			Ray ray = Camera.main.ScreenPointToRay (touchedPos);
			RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);

			if (hit) {
				if (hit.collider.gameObject.tag == "Fruit") {
					Fruit tappedFruit = hit.collider.gameObject.GetComponent<Fruit> ();
					if (!tappedFruit.isSelected ()) {
						playerManager.SetActive (tappedFruit);
					}
				} else if (hit.collider.tag == "ActionObject") {
					if (hit.collider.name == "Button_1" || hit.collider.name == "Button_2") {
						if (playerManager.activeCharacter.name == "Cherry") {
							playerManager.activeCharacter.Fling (hit.collider.name);
						}
					}
				}
			} else {
				Vector3 checkAgainPos = touchedPos - new Vector3 (0.0f, 100.0f, 0.0f);
				Ray checkAgainRay = Camera.main.ScreenPointToRay (checkAgainPos);
				RaycastHit2D checkAgainHit = Physics2D.GetRayIntersection (checkAgainRay, Mathf.Infinity);

				if (checkAgainHit) {
					if (checkAgainHit.collider.tag == "Tile") {
						if (checkAgainHit.collider.transform.name == "Surface") {
							Debug.Log ("HIT!");
							Tilemap tilemap = checkAgainHit.collider.transform.GetComponent<Tilemap> ();
							Vector3Int cellPosition = tilemap.WorldToCell ((Vector3)checkAgainHit.point);

							// get active fruit from player manager and set goal
							Fruit active = playerManager.activeCharacter;

							// position goal correctly
							Vector3 goal = tilemap.GetCellCenterWorld (cellPosition);
							goal.z += active.gameObject.transform.position.z;

							if (isValidDestination (active, goal)) {
								// record position for time reversal
								TimeManager timeManager = gameObject.GetComponent<TimeManager> ();
								timeManager.MarkKeyPosition ();
								//							Debug.Log ("KEY POSITION RECORDED AT : " + active.transform.position);

								// set goal
								active.SetDest (goal);
							} else {
								// invalid move – character shrugs
								Animator activeAnim = active.gameObject.GetComponent<Animator> ();
								activeAnim.SetTrigger ("shrug");
							}

							// instantiate and then destroy tap animation
							Vector3 tap_pos = goal + new Vector3 (0, 0.5f, 1.0f);
							GameObject tap = (GameObject)Instantiate (_tap, tap_pos, Quaternion.Euler (50, 0, 0));
							StartCoroutine (destroyTap (tap));
						}
					}
				}
			}
		}
	}

	private bool isValidDestination(Fruit active, Vector3 goal) {
		// check if object in path
		Vector3 origin = active.transform.position;
		goal.y -= active.transform.position.y;	// make distance parallel to ground
		Vector3 dir = (goal - origin).normalized;
		Vector3 dist = goal - origin;	// check only up to distance between goal and character

		LayerMask layerMask = 1 << 11; // only objects in layer 11 – obstacles
		RaycastHit2D hit = Physics2D.Raycast ((Vector2) origin, (Vector2) dir, dist.x, layerMask);
		if (hit) {
			return false;
		}

		return true;
	}

	public IEnumerator destroyTap(GameObject tap) {
		yield return new WaitForSeconds (tap.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);
		Destroy (tap);
	}


}
