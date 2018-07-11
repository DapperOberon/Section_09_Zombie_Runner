using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private bool respawn;

	public GameObject landingAreaPrefab;
	public Transform spawnPointParent;
	private Transform[] spawnPoints;

	private void Start()
	{
		spawnPoints = spawnPointParent.GetComponentsInChildren<Transform>();
	}

	// Update is called once per frame
	private void Update () {
		if (respawn)
		{
			Respawn();
		}
	}

	private void Respawn()
	{
		transform.position = spawnPoints[Random.Range(1, 4)].position;
		respawn = false;
	}

	private void OnFindClearArea()
	{
		Invoke("DropFlare", 3f);
	}

	void DropFlare()
	{
		// Drop flare
		Debug.Log("Found clear area...dropping flare.");
		Instantiate(landingAreaPrefab, transform.position, transform.rotation);
	}
}
