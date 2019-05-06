using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSpawnPoint : MonoBehaviour {

	public GameObject player;
	List<GameObject> spawnPoints;
	private int lastUpdatedPoint;

    public float DistanceChecker = 10.0f;

	// Use this for initialization
	void Start () {
		
		spawnPoints = new List<GameObject>();

		foreach(Transform child in transform){
			spawnPoints.Add(child.gameObject);
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		for(int i = spawnPoints.Count - 1; i>=0; i--){
            if (player.transform.localPosition.x > spawnPoints[i].transform.localPosition.x)
            {
                float d = Vector3.Distance(player.transform.localPosition, spawnPoints[i].transform.localPosition);

                if (d < DistanceChecker)
                {
                    player.GetComponent<PlayerController>().SetNewRespawnPoint(spawnPoints[i].transform.localPosition);
                    break;
                }
            }
		}
		
	}
}
