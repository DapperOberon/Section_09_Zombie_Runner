using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerVoice : MonoBehaviour {

	public AudioClip[] audioClips;
	private AudioSource audioSource;


	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource>();
		audioSource.clip = audioClips[0];
		audioSource.Play();
	}
	
	void OnFindClearArea()
	{
		print(name + " OnFindClearArea");
		audioSource.clip = audioClips[1];
		audioSource.Play();

		Invoke("CallHeli", audioSource.clip.length + 1f);
	}

	void CallHeli()
	{
		SendMessageUpwards("OnCallHeli");
	}
}
