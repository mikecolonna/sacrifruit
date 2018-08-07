using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.ImageEffects;

public class Keyframe
{
	public Vector3 position;
	public Quaternion rotation;
	public bool isActivated;

	public Keyframe(Vector3 position, Quaternion rotation)
	{
		this.position = position;
		this.rotation = rotation;
	}

	public Keyframe(Vector3 position, Quaternion rotation, bool isActivated) {
		this.position = position;
		this.rotation = rotation;
		this.isActivated = isActivated;
	}
}

public class TimeManager : MonoBehaviour {

	[SerializeField]
	private Dictionary<Reversible,Stack<Keyframe>> allPositions;
	private List<Reversible> reversibleObjects;

//	private Camera camera;


	// Use this for initialization
	void Start () {
		// initialization
//		camera = Camera.main;
		reversibleObjects = new List<Reversible> ();
		allPositions = new Dictionary<Reversible, Stack<Keyframe>> ();
//
		// get reversible objects and record initial position of every one
		GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
		foreach (GameObject go in allObjects) {
			if (go.GetComponent<Reversible>()) {
				Reversible obj = go.GetComponent<Reversible> ();
				reversibleObjects.Add (obj);

				Stack<Keyframe> positions = new Stack<Keyframe> ();
				if (obj is ActionObject) {
					positions.Push (new Keyframe (obj.transform.position, Quaternion.identity, (obj as ActionObject).isActivated ()));
				} else {
					positions.Push (new Keyframe (obj.transform.position, Quaternion.identity));
				}
				allPositions.Add (obj, positions);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Reverse() {
		Keyframe prevPos;
		foreach (Reversible obj in reversibleObjects) {
			// if there is at least 1 position
			if (allPositions [obj].Count > 1) {
				prevPos = allPositions [obj].Pop ();
				obj.transform.position = prevPos.position;

				if (obj is ActionObject) {
					// if in next position object is deactivated, reset object to be deactivated
					if ((obj as ActionObject).isActivated () && !prevPos.isActivated) {
						(obj as ActionObject).ResetState ();
//						Debug.Log ("STATE RESET");
					}
				} else if (obj is Fruit) {
					if ((obj as Fruit).isSelected ()) {
						(obj as Fruit).ResetMovement ();
					}
				}
			}
		}
	}

	public void ReverseAllTheWay() {
		Keyframe prevPos;
		foreach (Reversible obj in reversibleObjects) {
			prevPos = allPositions [obj].ToArray() [allPositions[obj].Count - 1];	// first position
			obj.transform.position = prevPos.position;

			if (obj is ActionObject) {
				// if in next position, object is deactivated, reset object
				if ((obj as ActionObject).isActivated() && !prevPos.isActivated) {
					(obj as ActionObject).ResetState ();
//					Debug.Log ("STATE RESET");
				}
			} else if (obj is Fruit) {
				if ((obj as Fruit).isSelected()) {
					(obj as Fruit).ResetMovement();
				}
			}

			// reset position stacks
			allPositions [obj] = new Stack<Keyframe> ();
			allPositions [obj].Push (prevPos);
		}
	}

	public void MarkKeyPosition() {
		foreach (Reversible obj in reversibleObjects) {
			if (obj is ActionObject) {
				allPositions [obj].Push (new Keyframe (obj.transform.position, obj.transform.rotation, (obj as ActionObject).isActivated ()));
			} else {
				allPositions [obj].Push (new Keyframe (obj.transform.position, obj.transform.rotation));
			}
		}
	}
}
