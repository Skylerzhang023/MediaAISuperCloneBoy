using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

	public string MainScene;
	public string StartScene;

	// Use this for initialization
	void Start () {
        StopAllCoroutines();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.JoystickButton7) || Input.GetKeyUp(KeyCode.JoystickButton9)){
			SceneManager.LoadScene(MainScene);
		}

		if(Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.JoystickButton6) || Input.GetKeyUp(KeyCode.JoystickButton10)){
			SceneManager.LoadScene(StartScene);
		}


	}
}
