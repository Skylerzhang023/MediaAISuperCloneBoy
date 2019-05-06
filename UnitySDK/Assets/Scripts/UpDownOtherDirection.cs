﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownOtherDirection : MonoBehaviour {

	public float offset = 0.0f;
	public float range = 2.0f;
	public float speed = 10.0f;
	private float origin;

	// Use this for initialization
	void Start () {
		origin = transform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () {

		transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, origin + range * Mathf.Sin((Time.time + offset) * speed * Mathf.Deg2Rad));
		
	}
}
