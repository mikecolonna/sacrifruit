using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour {

//	private SceneMa

	public void SceneSwitcher (string sceneName) {
		SceneManager.LoadScene (sceneName);
	}

	public void SceneSwap(int level) {
		Application.LoadLevel (level);
	}
}
