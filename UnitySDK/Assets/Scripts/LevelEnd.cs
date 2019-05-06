using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEnd : MonoBehaviour {

    private float d;
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        d = Vector3.Distance(player.transform.localPosition, this.transform.localPosition);
        Debug.Log("Distance to player: " + d);
	}

    public bool IsPlayerClose()
    {
        if (d < 15.0f)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
