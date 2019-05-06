using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenTrigger : MonoBehaviour {

	public GameObject player;
	private bool open = false;
	private float PlayerDistance;
	private Animator anim;

	// Use this for initialization
	void Start () {
		anim = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		PlayerDistance = Vector3.Distance(player.transform.position, this.transform.position);

		if((PlayerDistance<15.0f) && (!open)){
			anim.SetTrigger("Player");
			open = true;
		}
		
	}
}
