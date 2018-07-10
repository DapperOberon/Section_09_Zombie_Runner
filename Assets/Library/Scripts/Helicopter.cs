using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Helicopter : MonoBehaviour {

	public AudioSource callSource;
	public AudioSource heliSource;
	public AudioClip[] callAudio;
	public AudioClip[] heliAudio;

	private bool bCalled = false;
	private Rigidbody rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void Call()
	{
		if (!bCalled)
		{
			callSource.clip = callAudio[0];
			callSource.Play();
			heliSource.clip = heliAudio[0];
			heliSource.Play();
			rb.velocity = new Vector3(0, 0, 55.55f); // roughly three minutes to come
			bCalled = true;
		}
		
	}
}
