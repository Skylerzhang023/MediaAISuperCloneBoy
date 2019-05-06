using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horizontal_move : MonoBehaviour {

    public float offset = 0.0f;
    public float range = 2.0f;
    public float speed = 10.0f;
    private float origin;

    // Use this for initialization
    void Start()
    {
        origin = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {

        transform.localPosition = new Vector3(origin + range * Mathf.Sin((Time.time + offset) * speed * Mathf.Deg2Rad), transform.localPosition.y, transform.localPosition.z);

    }
}
