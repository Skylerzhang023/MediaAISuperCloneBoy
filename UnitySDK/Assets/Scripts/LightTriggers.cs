using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTriggers : MonoBehaviour {

	private List<GameObject> cl;
	private bool state = false;

	public GameObject player;
	public float MinimumDistance;

	// Use this for initialization
	void Start () {

		cl = new List<GameObject>();
		foreach (Transform child in transform){
			cl.Add(child.gameObject);
		}

		for(int i = 0; i<cl.Count; i++){
			cl[i].SetActive(false);
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if(IsPlayerClose(MinimumDistance)){
			if(!state){
			for(int i = 0; i < cl.Count; i++){
				cl[i].SetActive(true);
			}
			state = true;
			}
		}

		if(!IsPlayerClose(MinimumDistance) && state){
			for(int i = 0; i < cl.Count; i++){
				cl[i].SetActive(false);
			}
			state = false;
		}
		
	}

	bool IsPlayerClose(float minDistance){
		float d = Vector3.Distance(player.transform.position, this.transform.position);
		
		if(d < minDistance){
			return true;
		} else {
			return false;
		}
	}

}
