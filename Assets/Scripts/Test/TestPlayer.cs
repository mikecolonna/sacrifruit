using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestPlayer : MonoBehaviour {
//
//	[SerializeField]
//	private float _speed = 1.0f;
//
//	[SerializeField]
//	private bool moving = false;
//
//	[SerializeField]
//	protected Vector3 _target;

	public Transform goal;

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
//		_target = transform.position;
		agent = GetComponent<NavMeshAgent>();
	}
	
//	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;

			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100)) {
				
//				agent.destination = hit.point;
				agent.destination = hit.transform.TransformPoint (new Vector3 (0.0f, 0.5f, 0.0f));
			}
		}
			
	}
//
//	public void SetTarget(Transform pos) {
//		agent.destination = pos.position;
////		_target = pos + new Vector3(0.0f, 1.33f, 0.0f);
////		Debug.Log (_target);
//////		moving = true;
////		Debug.Log ("TARGET SET");
////		Vector3.MoveTowards (transform.position, target, _speed * Time.deltaTime);
//	}
}
