using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightAtTheEndOfTheTunnel : MonoBehaviour {

    public float MaxIntensity = 50.0f;
    public GameObject player;
    private float DistanceToPlayer;
    public AreaLight light;
    bool lightsOn = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        DistanceToPlayer = Vector3.Distance(player.transform.position, this.transform.position);

        if (DistanceToPlayer < 15.0f)
        {
            if (!lightsOn)
            {
                StartCoroutine(IncreaseLight());
                lightsOn = true;
            }
        }
		
	}

    IEnumerator IncreaseLight()
    {
        while (light.m_Intensity < 50.0f)
        {
            light.m_Intensity += .4f;
            yield return new WaitForSeconds(.01f);
        }

        yield return null;
    
    }
}
