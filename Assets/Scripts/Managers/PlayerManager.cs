using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private GameObject[] characters;
	private GameObject marker;

	[SerializeField]
	public Fruit activeCharacter;

	// Use this for initialization
	void Start () {
		characters = GameObject.FindGameObjectsWithTag ("Fruit");
		foreach (GameObject character in characters) {
			Debug.Log(character.name);
			Fruit fruit = character.GetComponent<Fruit> ();
			fruit.name = character.name;
			if (fruit.isSelected ()) {
				activeCharacter = fruit;
			} else {
				fruit.deselectCharacter ();
			}
//			character.GetComponent<Fruit>().deselectCharacter();
		}
//		activeCharacter = characters[0].GetComponent<Fruit>(); // placeholder
//		activeCharacter.selectCharacter();

		Debug.Log ("ACTIVE CHARACTER IS " + activeCharacter);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetActive(Fruit newFruit) {
		newFruit.selectCharacter ();
		activeCharacter.deselectCharacter ();
		activeCharacter = newFruit;
	}

	public Fruit GetActive() {
		return activeCharacter;
	}



}
