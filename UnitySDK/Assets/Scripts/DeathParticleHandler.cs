using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticleHandler : MonoBehaviour {

	public GameObject player;
	 ParticleSystem ps;


	void Awake(){
		ps = this.GetComponent<ParticleSystem>();
		//ps.Stop();
	}
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		ps.transform.localPosition = player.transform.localPosition;

		if(player.GetComponent<PlayerController>().IsDying()){
			if(ps.isStopped){
				ps.Play();
			}
		} else {
			ps.Clear();
			ps.Stop();
		}

		// if(ps.isStopped && ps.IsAlive()){
		// 	ps.Clear();
		// }
		
	}
}
