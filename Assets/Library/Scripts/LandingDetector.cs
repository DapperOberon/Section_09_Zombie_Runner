using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class LandingDetector : MonoBehaviour {

	public float timeSinceLastTrigger = 0f;
	private bool bSentMessage = false;

	private void Update()
	{
		timeSinceLastTrigger += Time.deltaTime;

		if(timeSinceLastTrigger > 1f && Time.realtimeSinceStartup > 20f && !bSentMessage)
		{
			SendMessageUpwards("OnFindClearArea");
			bSentMessage = true;
		}
		else
		{
			Debug.Log("Colliding...");
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if(other.tag != "Player")
		{
			timeSinceLastTrigger = 0f;
		}
	}
}
