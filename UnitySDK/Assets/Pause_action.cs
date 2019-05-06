using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause_action : MonoBehaviour {
    private bool ifvisible;
	// Use this for initialization
	void Start () {
        ifvisible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ifvisible = !ifvisible;
            if (ifvisible)
            {
                
            }
        }
           
	}
}
