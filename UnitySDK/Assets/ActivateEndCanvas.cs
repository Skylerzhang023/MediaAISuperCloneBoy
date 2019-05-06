using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivateEndCanvas : MonoBehaviour {

	public GameObject player;
	public Canvas UI;
	bool display = false;
    public float time = 3.0f;

	// Use this for initialization
	void Start () {
		UI.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (display)
        {
            UI.enabled = true;
            StartCoroutine(ResetGame());
        }
		
	}

	void OnCollisionEnter(Collision c){
		display = true;
	}

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("Start_Scene");
        yield return null;
    }
}
