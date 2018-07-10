using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool respawn;

	public AudioSource audioSource;
	public AudioClip[] audioClips;

	public Transform spawnPointParent;
	private Transform[] spawnPoints;
	private Helicopter helicopter;

	private void Start()
	{
		spawnPoints = spawnPointParent.GetComponentsInChildren<Transform>();
		helicopter = FindObjectOfType<Helicopter>();
		audioSource.clip = audioClips[0];
		audioSource.Play();
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
		Debug.Log("Found clear area...");
		helicopter.Call();
	}
}
