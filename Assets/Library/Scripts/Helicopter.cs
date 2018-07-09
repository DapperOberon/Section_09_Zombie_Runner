using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Helicopter : MonoBehaviour {

	public AudioSource callSource;
	public AudioSource heliSource;
	public AudioClip[] callAudio;
	public AudioClip[] heliAudio; 

	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown("CallHeli"))
		{
			callSource.clip = callAudio[0];
			callSource.Play();
			heliSource.clip = heliAudio[0];
			heliSource.Play();
		}
	}
}
