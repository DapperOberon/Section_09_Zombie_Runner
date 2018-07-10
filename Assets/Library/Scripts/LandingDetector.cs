using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LandingDetector : MonoBehaviour {

	private AudioSource audioSource;
	public float timeSinceLastTrigger = 0f;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		timeSinceLastTrigger += Time.deltaTime;

		if(timeSinceLastTrigger > 1f && Time.realtimeSinceStartup > 20f)
		{
			SendMessageUpwards("OnFindClearArea");
		}
		else
		{
			Debug.Log("Colliding...");
		}
	}

	private void OnTriggerStay(Collider other)
	{
		timeSinceLastTrigger = 0f;
	}
}
