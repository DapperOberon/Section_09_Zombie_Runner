using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioSystem : MonoBehaviour {

	public AudioClip[] audioClips;
	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}
	IEnumerator OnCallHeli()
	{
		audioSource.clip = audioClips[0];
		audioSource.Play();
		yield return new WaitForSeconds(audioSource.clip.length);
		audioSource.clip = audioClips[1];
		audioSource.Play();
		BroadcastMessage("OnHelicopterDispatch");
	}
}
